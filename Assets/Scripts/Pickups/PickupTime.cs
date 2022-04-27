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

        DataController.sharedInstance.sessionData.pickupData.Add(new PickupData(id, "TimeLeft", additionalTime, System.DateTime.Now.ToString("yyyyMMddHHmmss")));
    }

    public override void Despawn()
    {
        gameObject.SetActive(false);
    }
}