using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
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
        Debug.Log("Loading: " + sceneName);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void TestButton(string testString)
    {
        Debug.Log(testString);
    }

}
