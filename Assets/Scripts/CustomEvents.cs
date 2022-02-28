using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
     private static CustomEvents customEvents;
    
    public static CustomEvents CustomEventsInstance
    {
        get
        {
            if (ReferenceEquals(customEvents, null))
                customEvents = GameObject.FindObjectOfType<CustomEvents>();

            return customEvents;
        }
    }


    public UnityEvent StartSimulation = new UnityEvent();

    public UnityEvent Shoot = new UnityEvent();

    public UnityEventFloat DroneHorizontal = new UnityEventFloat();
    public UnityEventFloat DroneVertical = new UnityEventFloat();
    public UnityEventFloat DroneDiagonal = new UnityEventFloat();

    public UnityEvent ResetDroneVertical = new UnityEvent();
    public UnityEvent ResetDroneHorizontal = new UnityEvent();
    public UnityEvent ResetDroneDiagonal= new UnityEvent();




    public UnityEventString ChangeScene = new UnityEventString();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSimulation.Invoke();
        }



        // Vertical Movement
        if (Input.GetKey(KeyCode.Keypad2))
        {
            DroneVertical.Invoke(0.5f);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            DroneVertical.Invoke(-0.5f);
        }

        if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            ResetDroneVertical.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            ResetDroneVertical.Invoke();
        }


        // Horizontal Movement
        if (Input.GetKey(KeyCode.Keypad6))
        {
            DroneHorizontal.Invoke(0.5f);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            DroneHorizontal.Invoke(-0.5f);
        }

        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            ResetDroneHorizontal.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            ResetDroneHorizontal.Invoke();
        }        
        
        
        // Diagonal Movement
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            DroneDiagonal.Invoke(0.5f);
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            DroneDiagonal.Invoke(-0.5f);
        }

        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            ResetDroneDiagonal.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            ResetDroneDiagonal.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot.Invoke();
        }
    }
}

public class UnityEventFloat : UnityEvent<float>
{
}

public class UnityEventString : UnityEvent<string>
{
}


public enum Liquid
{
    None,
    Water
}


public static class CustomUtility
{
    public static string maskPath;


    // Code Snippet from https://gist.github.com/openroomxyz/bb22a79fcae656e257d6153b867ad437
    public static Texture2D LoadPNG(string filePath)
    {
        Debug.Log("Attempting to Load PNG");

        Texture2D tex = null;
        byte[] fileData;

        if (System.IO.File.Exists(filePath))
        {
            Debug.Log("File Path Exists");

            fileData = System.IO.File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        else
        {
            Debug.Log($"File path {maskPath} does not exist");
        }
        return tex;
    }
}