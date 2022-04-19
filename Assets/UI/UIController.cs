using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UIController : MonoBehaviour
{
    public UnityEventColour TempColour= new UnityEventColour();
    public UnityEventColour PressColour= new UnityEventColour();

    public ColourLerping TempLerp;
    public ColourLerping PressLerp;

    public static UIController SharedInstance;


    private void Awake()
    {
        if (ReferenceEquals(SharedInstance, null))
        {
            SharedInstance = this;

        }
        else
        {
            Debug.LogError("Additional UI Controllers in Scene");
        }

        TempLerp.UpdateColour.AddListener(GetTempColour);
        PressLerp.UpdateColour.AddListener(GetPressColour);
    }

    public void GetTempColour(Color colour)
    {
        Debug.LogWarning($"Temperature image colour is: {colour}");
        TempColour.Invoke(colour);
    }

    public void GetPressColour(Color colour)
    {
        Debug.LogWarning($"Pressure Image Colour is: {colour}");
        PressColour.Invoke(colour);
    }
}
