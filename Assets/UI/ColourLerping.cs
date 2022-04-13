﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColourLerp
{
    public Color targetColour;
    public float targetValue;
}

public struct AudioLerp
{
    public AudioClip targetClip;
    public float targetValue;
}

[CreateAssetMenu(fileName = "Colour Lerping", menuName = "ScriptableObjects/Colour Lerper", order = 1)]
public class ColourLerping : ScriptableObject
{
    public List<ColourLerp> values = new List<ColourLerp>();
    private float _currLerpValue = 0f;

    public UnityEventColour UpdateColour = new UnityEventColour();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sorting values");
        values.Sort((s1, s2) => s1.targetValue.CompareTo(s2.targetValue));
    }


    public void AdjustLerpValue(float delta)
    {
        Debug.Log($"New Current Lerp Value is {_currLerpValue}");
        _currLerpValue += delta;
        UpdateColour.Invoke(GetCurrentColour());
    }

    public void SetLerpValue(float absolute)
    {
        Debug.Log($"New Current Lerp Value is {_currLerpValue}");
        _currLerpValue = absolute;
        UpdateColour.Invoke(GetCurrentColour());
    }

    private Color GetCurrentColour()
    {
        int minIndex = -1;
        for(int i =0; i < values.Count; i++)
        {
            if(values[i].targetValue < _currLerpValue)
            {
                minIndex = i;
            }
        }

        if(minIndex >= 0)
        {
            return Color.Lerp(values[minIndex].targetColour, values[minIndex + 1].targetColour, (_currLerpValue - values[minIndex].targetValue) / (values[minIndex + 1].targetValue - values[minIndex].targetValue));
        }

        return values[0].targetColour;
    }
}


[CreateAssetMenu(fileName =" Audio Lerping", menuName = "ScriptableObjects/Audio Lerper", order =1)]
public class AudioLerping: ScriptableObject
{
    public List<AudioLerp> values = new List<AudioLerp>();
    private float _currLerpValue = 0f;

    public UnityEventAudio UpdateAudio = new UnityEventAudio();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sorting values");
        values.Sort((s1, s2) => s1.targetValue.CompareTo(s2.targetValue));
    }

}