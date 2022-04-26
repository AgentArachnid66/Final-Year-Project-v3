using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackObject : MonoBehaviour
{
    public float totalWeighting;
    public List<float> evaluatedControllers = new List<float>();
    public float lerpValue;
    
    public virtual void AdjustFeedback(float pressure, float temp)
    {

    }


    public virtual void AdjustFeedback(float value)
    {

    }

    public virtual void AdjustFeedback(float value, int index)
    {

    }
    public virtual void AdjustFeedback(Color value)
    {
        
    }

}
