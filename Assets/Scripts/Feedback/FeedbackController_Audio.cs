using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController_Audio : FeedbackController
{
    public AudioLerping audioLerping;

    public void Start()
    {
        foreach (FeedbackObjectAudio item in feedbackObjects)
        {
            item.audioLerping = audioLerping;
        }
    }
}
