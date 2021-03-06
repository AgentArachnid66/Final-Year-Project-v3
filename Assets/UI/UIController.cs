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

    private InputActions _playerInput;
    public UnityEvent AlteredSubmit = new UnityEvent();
    public UnityEvent Delete = new UnityEvent();

    

    private void Start()
    {
        GetPlayerScore();


        _playerInput = new InputActions();
        _playerInput.UI.Submit_Altered.Enable();
        _playerInput.UI.Delete.Enable();
        _playerInput.UI.Navigate.Enable();

        _playerInput.UI.Submit_Altered.performed += ctx =>
        {
            //Delete.Invoke();
            //AlteredSubmit.Invoke();
        };

        _playerInput.UI.Delete.performed += ctx =>
        {
            Delete.Invoke();
        };

        _playerInput.UI.Navigate.performed += ctx =>
        {
           // Debug.LogError("Navigation Event");
        };

    }

    private void Awake()
    {
        if (ReferenceEquals(SharedInstance, null))
        {
            SharedInstance = this;
            //Debug.LogError($"The name of the object with UI Controller: {this.gameObject.name}");
        }
        else
        {

            if (GameObject.FindObjectsOfType<DataController>().Length > 1)
            {
                Debug.Log("Multiple Data Controllers");
                foreach (var item in GameObject.FindObjectsOfType<GlobalController>())
                {
                    Debug.LogWarning(item.gameObject.name);
                }
            }
            else
            {
                Debug.Log("Reference is pointing to previous scene");
                SharedInstance = this;
            }

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
        //Debug.LogWarning($"Temperature image colour is: {colour}");
        TempColour.Invoke(colour);
    }

    public void GetPressColour(Color colour)
    {
       // Debug.LogWarning($"Pressure Image Colour is: {colour}");
        PressColour.Invoke(colour);
    }

    public void GetPlayerScore()
    {
        PlayerScore.Invoke(PlayerPrefs.GetFloat("score", 0f));
    }

    public void UseTimePickup()
    {
        Debug.LogWarning("Used Time Pickup");
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
