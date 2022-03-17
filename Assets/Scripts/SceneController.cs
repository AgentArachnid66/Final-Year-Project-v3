﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]private bool _canChange;

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

}
