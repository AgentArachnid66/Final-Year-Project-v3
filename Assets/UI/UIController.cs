using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UIController : MonoBehaviour
{
    public UnityEventColour TempColour= new UnityEventColour();
    public UnityEventColour PressColour= new UnityEventColour();
    public UnityEventFloat PlayerScore = new UnityEventFloat();

    public ColourLerping TempLerp;
    public ColourLerping PressLerp;

    public static UIController SharedInstance;
    public Pickup[] pickups = new Pickup[2];
    private void Start()
    {
        GetPlayerScore();
    }

    private void Awake()
    {
        if (ReferenceEquals(SharedInstance, null))
        {
            SharedInstance = this;
            Debug.LogError($"The name of the object with UI Controller: {this.gameObject.name}");
        }
        else
        {
            Debug.LogError("Additional UI Controllers in Scene");
            Debug.LogError($"The name of the object with UI Controller: {this.gameObject.name}");
        }
        if (!ReferenceEquals(null, TempLerp))
        {
            TempLerp.UpdateColour.AddListener(GetTempColour);
        }
        if (!ReferenceEquals(null, PressLerp))
        {
            PressLerp.UpdateColour.AddListener(GetPressColour);
        }
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

    public void GetPlayerScore()
    {
        PlayerScore.Invoke(PlayerPrefs.GetFloat("score", 150f));
    }

    public void UseTimePickup()
    {
        int id = -1;
        for (int i = 0; i < pickups.Length; i++)
        {
            if (pickups[i] != null)
            {
                id = i;
            }
        }
        if (id >= 0)
        {
            StartCoroutine(GlobalController.SharedInstance.StartCountDown(30f));
            DataController.sharedInstance.sessionData.pickupData.Add(new PickupData(id, "TimeLeft", System.DateTime.Now.ToString("yyyyMMddHHmmss"), InteractionType.Used));
            pickups[id] = null;
        }
    }
}
