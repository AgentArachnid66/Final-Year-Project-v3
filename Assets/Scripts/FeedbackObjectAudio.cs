using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FeedbackObjectAudio : FeedbackObject
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public override void AdjustFeedback(float pressure, float temp)
    {
        base.AdjustFeedback(pressure, temp);
        Debug.Log($"Adjusted Audio Feedback: Pressure: {pressure} and Temperature: {temp}");

        audioSource.volume = Mathf.Clamp01(pressure + temp);
    }
}

