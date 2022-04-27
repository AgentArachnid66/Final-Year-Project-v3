using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FeedbackObjectAudio : FeedbackObject
{
    public AudioSource pos_audioSource;
    public AudioSource neg_audioSource;
    public AudioLerping audioLerping;
    public float volume;
    public float volume_1;
    public float volume_2;

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

        lerpValue = sum;

        DetermineFeedback(audioLerping.GetVolume(lerpValue));
    }

    void DetermineFeedback(float volume_n)
    {



        Debug.Log($"Audio Object Feedback");
        volume_1 = volume_n * volume;
        volume_2 = (1f - volume_n) * volume;

        pos_audioSource.volume = volume_n * volume;
        neg_audioSource.volume = (1f - volume_n) * volume;

        

    }
}

