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

    public static void StartCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
