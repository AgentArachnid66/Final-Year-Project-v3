using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    public FeedbackObject[] feedbackObjects;

    public AnimationCurve pressureCurve;
    public AnimationCurve temperatureCurve;

    [SerializeField]
    protected float testPress;
    [SerializeField]
    protected float testTemp;

    private void OnEnable()
    {
        CustomEvents.CustomEventsInstance.HitWallPressureTemp.AddListener(AdjustFeedback);
        
    }

    private void Update()
    {
        TestAdjustment();
    }

    public virtual void AdjustFeedback(float pressure, float temperature)
    {
        for (int i = 0; i < feedbackObjects.Length; i++)
        {
            feedbackObjects[i].AdjustFeedback(pressureCurve.Evaluate(pressure), temperatureCurve.Evaluate(temperature));
        }
    }


    public void TestAdjustment()
    {
        for (int i = 0; i < feedbackObjects.Length; i++)
        {
            feedbackObjects[i].AdjustFeedback(testPress, testTemp);
        }
    }

}
