using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class CoroutineManager : MonoBehaviour
{
    public UnityAction testAction;

    private void Start()
    {
        testAction += Test1;
        DataController.sharedInstance.SaveSessionAction += Test1;
        DataController.sharedInstance.SaveMasksAction += Test1;

        testAction += DataController.sharedInstance.SaveSessionAction.Invoke;
    }
    
    [ContextMenu("Test Action")]
    public void Test()
    {
        testAction.Invoke();
    }

    private void Test1()
    {
        Debug.Log("test Action Triggered");
    }
}
