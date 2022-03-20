using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;



public class DataController : MonoBehaviour
{
    public static DataController sharedInstance;


    private string url = "http://localhost:5000/";

    private ScoreOutputData score;

    public ScoreInputData scoreInput;

    public NoiseGenerator noiseGenerator;

    // Set when the session starts to Time.time. This will give a more accurate
    // measurement of the length of the session
    public float startTime = 0f;


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

    private void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        int numOfWalls = GameObject.FindGameObjectsWithTag("Wall").Length;
        sessionData.masks = new WallData[numOfWalls];
    }

    public void SetLoginID(int id)
    {
        participantData.ID = id;
    }

    public void SetLoginPIN(int pin)
    {
        participantData.PIN = pin;
    }

    #region Coroutine Tests

    [ContextMenu("Calculate Score")]
    public void GetScore()
    {
        scoreInput.images.renderTarget = sessionData.masks[0].waterMask;
        StartCoroutine(CalculateScore());
    }

    [ContextMenu("Save Current Session")]
    public void Save()
    {
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

    }

    IEnumerator SaveSession()
    {
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
            Debug.Log("Error Occured: " + saveSession.error);
            RegisterAttemptCallback.Invoke(false, saveSession.error);
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
            LoginAttemptCallback.Invoke(false, saveSession.error);
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
            }
            LoginAttemptCallback.Invoke(respond.success, "");
        }
    }

#endregion

}
