using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FeedbackObject_Lighting : FeedbackObject
{
    private Light _light;
    
    void Start()
    {
        _light = GetComponent<Light>();
    }

    public override void AdjustFeedback(Color value)
    {
        Debug.Log($" Adjusting the Lighting to: {value}");
        _light.color = value;
    }

}
