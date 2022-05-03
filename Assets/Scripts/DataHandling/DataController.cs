using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;
using System.Collections.Generic;

public class DataController: MonoBehaviour
{
    public static DataController sharedInstance;


    private string url = "https://still-springs-04765.herokuapp.com/";

    private ScoreOutputData score;

    public ScoreInputData scoreInput;

    public float scoreBackup;

    // Set when the session starts to Time.time. This will give a more accurate
    // measurement of the length of the session
    public float startTime = 0f;

    public UnityEvent SaveSessionAction;
    public UnityEvent SaveMasksAction;

    [SerializeField]
    public UserData data = new UserData();

    [SerializeField]
    public SessionData sessionData = new SessionData();

    [SerializeField]
    public ParticipantData participantData = new ParticipantData();

    /// <summary>
    /// Callback events, giving the success of the request and the error code if applicable
    /// </summary>

    [SerializeField]
    public UnityEventBoolString LoginAttemptCallback = new UnityEventBoolString();

    [SerializeField]
    public UnityEventBoolString RegisterAttemptCallback = new UnityEventBoolString();

    public Dictionary<int, Texture2D> wallMasks = new Dictionary<int, Texture2D>();
    
    private bool[] savedMasks;

    public Shader Combine;
    public Material combine;
    public RenderTexture source;

    public UnityEventFloat GotScore = new UnityEventFloat();
    public UnityEventBool SaveComplete = new UnityEventBool();
    public UnityEvent SaveAndQuit = new UnityEvent();
    
    private bool _saved;
    [SerializeField]private int _numSessions;
    
    public int numSessions
    {
        get { return _numSessions; }
    }

    private void Awake()
    {
        if (ReferenceEquals(sharedInstance, null))
        {
            sharedInstance = this;
        }
        else
        {
            Debug.Log("Multiple Data Controllers");
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        combine = new Material(Combine);
        
        int numOfWalls = GameObject.FindGameObjectsWithTag("Wall").Length;
        sessionData.masks = new WallData[numOfWalls];
        savedMasks = new bool[numOfWalls];

        Application.quitting += LogOut;
        

        if (PlayerPrefs.GetInt("prevLoggedIn") > 0) sessionData.PlayerID = PlayerPrefs.GetInt("playerID");
        
        Debug.Log($"Player Identity is {sessionData.PlayerID}");
    }

    private void LogOut()
    {
        PlayerPrefs.SetInt("playerID", -1);
        PlayerPrefs.SetInt("prevLoggedIn", 0);
        PlayerPrefs.SetFloat("score", 0);
        Debug.Log("Player logged out");

    }
    
    
    private string UploadPhoto()
    {
        return "";
    }

    public void UpdateMaskCheck(int index, bool input)
    {
        savedMasks[index] = input;
    }

    public void SetLoginID(int id)
    {
        participantData.ID = id;
    }
    

    public void SetLoginPassword(string password)
    {
        participantData.Password = password;
    }
    
    #region Coroutine Tests

    [ContextMenu("Calculate Score")]
    public void GetScore()
    {
        SaveMasksAction.Invoke();
        StartCoroutine(CalculateScore());
    }


    [ContextMenu("Save Current Session")]
    public void Save()
    {
        Debug.Log("Called Save Session");
        if (SaveSessionAction != null)
        {
            SaveSessionAction.Invoke();
        }

        // Can now convert the timestamp from this format into a number that can be 
        // used to analyse dates and order by chronological order
        sessionData.TimeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");

        // Can be used to analyse the length of the session;
        sessionData.Length = Time.time - startTime;

        StartCoroutine(SaveSession());
    }


    [ContextMenu("Create Participant Account")]
    public void AccountCreation()
    {
        StartCoroutine(CreateAccount());
    }

    [ContextMenu("Login Participant Account")]
    public void AccountLogin()
    {
        StartCoroutine(LoginAccount());
    }

#endregion

#region Coroutines

    IEnumerator CalculateScore()
    {        
       
        yield return new WaitUntil(CheckMasks);
        scoreInput.paths = new string[sessionData.masks.Length];
        
        for (int i = 0; i < sessionData.masks.Length; i++)
        {
            scoreInput.paths[i] = sessionData.masks[i].combinedMasks;
        }
        
        Debug.Log(JsonUtility.ToJson(scoreInput));


        string localURL = url + "Image/Updated";
        UnityWebRequest scoreCal = new UnityWebRequest(localURL);
        scoreCal.method = UnityWebRequest.kHttpVerbPOST;
        scoreCal.downloadHandler = new DownloadHandlerBuffer();
        scoreCal.SetRequestHeader("Content-Type", "application/json");

        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(JsonConvert.SerializeObject(scoreInput));
        scoreCal.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);

        yield return scoreCal.SendWebRequest();

        if(scoreCal.isNetworkError || scoreCal.isHttpError)
        {
            Debug.Log("Error Occured: " + scoreCal.error);
            sessionData.Score = Mathf.RoundToInt(scoreBackup);
        }
        else
        { 
            Debug.Log(scoreCal.downloadHandler.text);
            ScoreOutputData respond = JsonConvert.DeserializeObject<ScoreOutputData>(scoreCal.downloadHandler.text);

            if (respond.success)
            {
                sessionData.Score = Mathf.RoundToInt(respond.score);
            }

        }


        PlayerPrefs.SetFloat("score", sessionData.Score);
        GotScore.Invoke(sessionData.Score);
    }
    
    IEnumerator SaveSession()
    {
        yield return new WaitUntil(CheckMasks);
        CustomUtility.SaveSessionToJSON(sessionData);
        string localURL = url + "Session";
        
        Debug.Log(JsonConvert.SerializeObject(sessionData));
        UnityWebRequest saveSession = new UnityWebRequest(localURL, "POST");
        saveSession.downloadHandler = new DownloadHandlerBuffer();
        saveSession.SetRequestHeader("Content-Type", "application/json");
         
        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(JsonConvert.SerializeObject(sessionData));
        saveSession.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);

        yield return saveSession.SendWebRequest();

        if (saveSession.isNetworkError || saveSession.isHttpError)
        {                                  
            Debug.Log("Error Occured: " + saveSession.error);
        }
        
        SaveComplete.Invoke(true);
        SaveAndQuit.Invoke();
    }

    private bool CheckMasks()
    {
        for (int i = 0; i < savedMasks.Length; i++)
        {
            if (!savedMasks[i]) return false;
        }

        return true;
    }

    IEnumerator CreateAccount()
    {
        string localURL = url + "Participant/Create";
        string serial = JsonConvert.SerializeObject(participantData);
        Debug.Log(serial);
        UnityWebRequest saveSession = new UnityWebRequest(localURL, "POST");
        saveSession.downloadHandler = new DownloadHandlerBuffer();
        saveSession.SetRequestHeader("Content-Type", "application/json");

        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(serial);
        saveSession.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);

        yield return saveSession.SendWebRequest();

        if (saveSession.isNetworkError || saveSession.isHttpError)
        {

            ErrorOutputData respond = JsonConvert.DeserializeObject<ErrorOutputData>(saveSession.downloadHandler.text);
            Debug.Log("Error Occured: " + saveSession.error);
            if (ReferenceEquals(respond, null)){
                RegisterAttemptCallback.Invoke(false, saveSession.error);
            }
            else {
                RegisterAttemptCallback.Invoke(false, respond.msg); }
        }
        else
        {
            Debug.Log(saveSession.downloadHandler.text);
            LoginOutputData respond = JsonConvert.DeserializeObject<LoginOutputData>(saveSession.downloadHandler.text);
            if (respond.success)
            {
                // If the response is successful, then the PlayerID is set to the 
                // ObjectID of the participant
                sessionData.PlayerID = participantData.ID;
            }

            RegisterAttemptCallback.Invoke(respond.success, "");
        }
    }

    IEnumerator LoginAccount()
    {
        string localURL = url + "Participant/Login";
        string serial = JsonConvert.SerializeObject(participantData);
        Debug.Log(serial);
        UnityWebRequest saveSession = new UnityWebRequest(localURL, "POST");
        saveSession.downloadHandler = new DownloadHandlerBuffer();
        saveSession.SetRequestHeader("Content-Type", "application/json");

        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(serial);
        saveSession.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);

        yield return saveSession.SendWebRequest();

        if (saveSession.isNetworkError || saveSession.isHttpError)
        {
            Debug.Log("Error Occured: " + saveSession.error);
            // Login Failed
            ErrorOutputData respond = JsonConvert.DeserializeObject<ErrorOutputData>(saveSession.downloadHandler.text);
            Debug.Log("Server Responded with: " + respond.msg);
            LoginAttemptCallback.Invoke(false, respond.msg);
        }
        else
        {
            // Check if the participant login was successful before moving forward
            Debug.Log(saveSession.downloadHandler.text);
            LoginOutputData respond = JsonConvert.DeserializeObject<LoginOutputData>(saveSession.downloadHandler.text);
            if (respond.success)
            {
                // If the response is successful, then the PlayerID is set to the 
                // ObjectID of the participant
                sessionData.PlayerID = participantData.ID;
                PlayerPrefs.SetInt("playerID", sessionData.PlayerID);
                PlayerPrefs.SetInt("PrevLoggedIn", 1);
            }

            LoginAttemptCallback.Invoke(respond.success, "");
            Debug.Log(url + "Session/Count");
            UnityWebRequest retrieveNumSessions = new UnityWebRequest(url + "Session/Count");
            retrieveNumSessions.downloadHandler = new DownloadHandlerBuffer();
            retrieveNumSessions.SetRequestHeader("Content-Type", "application/json");
            retrieveNumSessions.uploadHandler = (UploadHandler) new UploadHandlerRaw(bytes);

            yield return retrieveNumSessions.SendWebRequest();
            if (retrieveNumSessions.isNetworkError || retrieveNumSessions.isHttpError)
            {
                Debug.Log(retrieveNumSessions.error);
                _numSessions = (int)System.DateTimeOffset.Now.ToUnixTimeSeconds();
            }

            else
            {
                _numSessions = JsonConvert.DeserializeObject<int>(retrieveNumSessions.downloadHandler.text);
                Debug.Log($"Number of Sessions played by this participant is {_numSessions}");
                _numSessions += 1;
            }
        }
    }
    
    

#endregion

}
