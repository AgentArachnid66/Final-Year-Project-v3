using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    public UnityEvent StartSimulation = new UnityEvent();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSimulation.Invoke();

        }
    }
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