using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackObject : MonoBehaviour
{


    public virtual void AdjustFeedback(float pressure, float temp)
    {

    }


    public virtual void AdjustFeedback(float value)
    {

    }


    public virtual void AdjustFeedback(Color value)
    {
        
    }
}
