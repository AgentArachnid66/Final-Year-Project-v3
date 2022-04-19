using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController_Lighting : FeedbackController
{
    public ColourLerping colourLerping;

    public float radius = Mathf.Infinity;


    void Start() {

        FeedbackObject_Lighting[] arrayFeedback = FindObjectsOfType<FeedbackObject_Lighting>();
        List<FeedbackObject_Lighting> value = new List<FeedbackObject_Lighting>();

        foreach (FeedbackObject_Lighting item in arrayFeedback)
        {
            if(Vector3.Distance(item.transform.position, this.transform.position) < radius)
            {
                value.Add(item);
            }
        }

        feedbackObjects = value.ToArray();

        colourLerping.UpdateColour.AddListener(ctx =>
            {
                Debug.Log($"Recieved Event to Update the Colour to: {ctx}");

                for (int i = 0; i < feedbackObjects.Length; i++) {
                    if (!ReferenceEquals(feedbackObjects[i], null))
                    {
                        feedbackObjects[i].AdjustFeedback(ctx);
                    }
                    }
            });

        CustomEvents.CustomEventsInstance.HitWallPressureTemp.AddListener(GetCurrentColour);

        
    }

    private void Update()
    {
        colourLerping.SetLerpValue(Mathf.InverseLerp(0, 2,(testPress + testTemp)));
    }

    void GetCurrentColour(float temp, float press)
    {
        colourLerping.SetLerpValue((temp + press) / 2);
    }
}
