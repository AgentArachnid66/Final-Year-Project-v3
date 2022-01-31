using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;



public class DataController : MonoBehaviour
{
    
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

    // Start is called before the first frame update
    void Start()
    {
        //database = client.GetDatabase("GameData");
        //userCollection = database.GetCollection<BsonDocument>("Player Data");
        //sessionCollection = database.GetCollection<BsonDocument>("Session Data");
    }


    /*
    [ContextMenu("Save Session")]
    public async void SaveSessionDataToDatabase()
    {
        BsonDocument document = sessionData.ToBsonDocument<SessionData>();
        await sessionCollection.InsertOneAsync(document);
        
    }

    [ContextMenu ("Save User")]
    public async void SaveUserDataToDatabase()
    {
        BsonDocument document = data.ToBsonDocument<UserData>();
        await userCollection.InsertOneAsync(document);

    }
    /*
    [ContextMenu ("Get Users")]
    public async void GetUsers()
    { 

    } 
    */




#region Coroutine Tests

    [ContextMenu("Calculate Score")]
    public void GetScore()
    {
        scoreInput.images.renderTarget = sessionData.mask;
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
        }
        else
        {
            // Check if the participant creation was successful before moving forward

            // Go to the login screen
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
        }
    }

#endregion

}
