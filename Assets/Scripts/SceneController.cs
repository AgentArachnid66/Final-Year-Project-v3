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


    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

}
