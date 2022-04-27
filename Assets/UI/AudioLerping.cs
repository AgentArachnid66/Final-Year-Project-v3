using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]

[CreateAssetMenu(fileName = " Audio Lerping", menuName = "ScriptableObjects/Audio Lerper", order = 1)]
public class AudioLerping : ScriptableObject
{
    public AnimationCurve volumeCurve;
    public AnimationCurve clipCurve;
    public AnimationCurve freqCurve;

    public AudioClip clip1;
    public AudioClip clip2;



    // Start is called before the first frame update
    public float GetVolume(float abs)
    {
        return volumeCurve.Evaluate(abs);
    }

    public AudioClip GetClip(float x)
    {
        float y = clipCurve.Evaluate(x);

        return y >= 0.5f ? clip1 : clip2;        
    }

    public float GetFrequency(float x)
    {
        return freqCurve.Evaluate(x);
    }

}