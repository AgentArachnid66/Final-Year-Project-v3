using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController_Audio : FeedbackController
{
    public AudioLerping audioLerping;

    public override void Start()
    {
        base.Start();

        foreach (FeedbackObjectAudio item in feedbackObjects)
        {
            item.audioLerping = audioLerping;
        }
    }
    public override void AdjustFeedback(float temp, float press)
    {
    
        testPress =pressureCurve.Evaluate(Mathf.Clamp(press, -1f, 1f));
        testTemp = temperatureCurve.Evaluate(Mathf.Clamp(temp, -1f, 1f));

        Debug.LogWarning($"Controller Lerping with value");
        float ctx = (temperatureCurve.Evaluate(temp) + pressureCurve.Evaluate(press)) / 2;
        foreach (FeedbackStruct feedback in nodes)
        {
            feedback.feedBackObject.AdjustFeedback(ctx * (CalculateWeight(feedback) / feedback.feedBackObject.totalWeighting), feedback.cont_id);
        }

    }

    private float CalculateWeight(FeedbackStruct item)
    {
       return 1f - (Vector3.Distance(item.feedBackObject.transform.position, this.transform.position) / radius);
       
    }


    private void Update()
    {
        //AdjustFeedback(temperatureCurve.Evaluate(testTemp),pressureCurve.Evaluate(testPress));
    }
}
