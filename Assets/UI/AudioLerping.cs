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



    // Start is called before the first frame update
    public float GetVolume(float abs)
    {
        return volumeCurve.Evaluate(abs);

    }

}