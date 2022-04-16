using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]private bool _canChange;

    [SerializeField]
    public UnityEventBool SceneCallback = new UnityEventBool();

    public GameObject popup;

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
            SceneManager.LoadSceneAsync(sceneName);

        }
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
            sceneName = sceneName == "" ? "SampleScene" : sceneName;
            Debug.Log("Loading: " + sceneName);
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void UpdateRegister(bool success, string message)
    {
        popup.SetActive(true);
        TMPro.TMP_Text text = popup.GetComponent<TMPro.TMP_Text>();

        text.text = success ? "Registeration Successful. Please Log In" : "Error Occured: " + message;

        Debug.Log(text.text);
    }

}
