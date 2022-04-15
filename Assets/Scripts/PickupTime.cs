using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTime : Pickup
{
    public float additionalTime = 30f;
    
    public override void Activate()
    {
        Debug.Log("Activate");
        StartCoroutine(GlobalController.SharedInstance.StartCountDown(additionalTime));
    }
}