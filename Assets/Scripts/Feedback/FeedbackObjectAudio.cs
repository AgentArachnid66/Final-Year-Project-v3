using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FeedbackObjectAudio : FeedbackObject
{
    public AudioSource pos_audioSource;
    public AudioSource neg_audioSource;
    public AudioLerping audioLerping;
    private void Start()
    {
    }

    public override void AdjustFeedback(float value, int index)
    {
        evaluatedControllers[index] = value;

        float sum = 0f;
        for (int i = 0; i < evaluatedControllers.Count; i++)
        {
            sum += evaluatedControllers[i];
        }

        DetermineFeedback(audioLerping.SetLerpValue(sum));
    }

    void DetermineFeedback(float volume)
    {
        pos_audioSource.volume = volume;
        neg_audioSource.volume = 1f - volume;
    }
}

