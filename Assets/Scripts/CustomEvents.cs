using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    public UnityEvent StartSimulation = new UnityEvent();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSimulation.Invoke();
        }
    }
}
