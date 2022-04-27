using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public struct FeedbackStruct
{
    public FeedbackObject feedBackObject;
    public float weighting;
    public int cont_id;
    public FeedbackStruct(FeedbackObject object_Lighting, float weight, int id)
    {
        this.feedBackObject = object_Lighting;
        this.weighting = weight;
        this.cont_id = id;
    }
}


public class FeedbackController_Lighting : FeedbackController
{
    public ColourLerping colourLerping;

    public override void Start()
    {
        base.Start();

        Debug.LogWarning("Lighting Controller Started");

        foreach (FeedbackObject_Lighting item in feedbackObjects)
        {
            Debug.Log($"Lighting {item.name}  Colour Lerp Set: {item.GetType().ToString()}");
            item.colourLerping = colourLerping;
        }
    }

    private void Update()
    {
       // AdjustFeedback(temperatureCurve.Evaluate(testTemp),pressureCurve.Evaluate(testPress));

        //UpdateColour.Invoke(colourLerping.SetLerpValue(Mathf.InverseLerp(0, 2,(testPress + testTemp))));
    }

    public override void AdjustFeedback(float temp, float press)
    {
        Debug.LogWarning($"Controller Lerping with value");
        float ctx = (temp + press) / 2;
        foreach (FeedbackStruct feedback in nodes)
        {
            feedback.feedBackObject.AdjustFeedback(ctx * (feedback.weighting / feedback.feedBackObject.totalWeighting),feedback.cont_id);
        }

    }

    private void PreventRemoval(float test, int index)
    {
        Debug.LogWarning($"The Lerp Value produced is {colourLerping.SetLerpValue(test)}");
    }
    private void OnDrawGizmos()
    {

        Gizmos.DrawSphere(transform.position+ offset, radius);
    }
}
