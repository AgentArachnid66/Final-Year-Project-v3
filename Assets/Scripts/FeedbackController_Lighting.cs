using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController_Lighting : FeedbackController
{
    public ColourLerping colourLerping;


    void Start() {
        colourLerping.UpdateColour.AddListener(ctx =>
            {
                Debug.Log($"Recieved Event to Update the Colour to: {ctx}");

                for (int i = 0; i < feedbackObjects.Length; i++) {
                    feedbackObjects[i].AdjustFeedback(ctx);
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
