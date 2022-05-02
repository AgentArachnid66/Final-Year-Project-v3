using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTime : Pickup
{
    public float additionalTime = 30f;
    
    public override void Activate()
    {
        DataController.sharedInstance.sessionData.pickupData.Add(new PickupData(id, "TimeLeft", additionalTime, System.DateTime.Now.ToString("yyyyMMddHHmmss"), InteractionType.PickedUp));
        UIController.SharedInstance.pickups[id] = this;
    }

    public override void Despawn()
    {
        gameObject.SetActive(false);
    }
}