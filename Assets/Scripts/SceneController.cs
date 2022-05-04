using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]private bool _canChange;

    [SerializeField]
    public UnityEventBool SceneCallback = new UnityEventBool();

    public GameObject registerPopup;
    public GameObject loginPopup;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This class will be in charge of switching the scenes

    public void ChangeScene(string sceneName)
    {
        if (_canChange)
        {
            Debug.Log("Loading: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }

    public void RefreshScene()
    {
        Scene active = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(active.buildIndex);
        SceneManager.LoadSceneAsync(active.buildIndex);
    }

    public void TestButton(string testString)
    {
        Debug.Log(testString);
    }

    public void UpdateCanChange(bool can)
    {
        _canChange = can;
    }

    public void UpdateSceneFromCallback(bool can, string sceneName)
    {
        _canChange = can;

        if (_canChange)
        {
            sceneName = sceneName == "" ? "MainLevel" : sceneName;
            Debug.Log("Loading: " + sceneName);
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void UpdateRegister(bool success, string message)
    {
        registerPopup.SetActive(true);
        TMPro.TMP_Text text = registerPopup.GetComponent<TMPro.TMP_Text>();

        text.text = success ? "Registeration Successful. Please Log In" : "Error Occured: " + message;

        Debug.LogError(text.text);
    }
    public void UpdateLogin(bool success, string message)
    {
        if (!success)
        {
            loginPopup.SetActive(true);
            TMPro.TMP_Text text = loginPopup.GetComponent<TMPro.TMP_Text>();

            text.text = message;

            Debug.Log(text.text);
        }
        else
        {
            Debug.Log("Load Scene");
            UpdateSceneFromCallback(success, "MainLevel");
        }
    }

    

    public void QuitGame()
    {
        Application.Quit();
    }

}
