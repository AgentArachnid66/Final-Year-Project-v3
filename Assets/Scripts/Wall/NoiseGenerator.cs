using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class NoiseGenerator : MonoBehaviour
{

    public RenderTexture sourceTexture;
    //public RenderTexture no_First;
    //public RenderTexture second_Third;
    public RenderTexture globalMask;
    public DataController dataController;

    public int width = 1024;
    public int height = 1024;
    public float scale = 20f;
    public float offset_X;
    public float offset_Y;
    public float numOfBands;
    public float falloff;


    public RenderTexture waterMask;
    public int wallID;

    


    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        waterMask = new RenderTexture(sourceTexture);
        globalMask = new RenderTexture(sourceTexture);

        UpdateTexture(globalMask);
        SaveRenderTexture(globalMask, "mask" + gameObject.name);
        Debug.Log($"Saving Mask to {CustomUtility.maskPath}");

        _renderer.material.SetTexture("Texture2D_1C6CB6F8", waterMask);
        _renderer.material.SetTexture("Texture2D_CA25BCB1", globalMask);
        
    }

    [ContextMenu("Update Texture")]
    void UpdateTexture(RenderTexture target) {

        if (_renderer != null)
        {
            _renderer.material.mainTexture = GenerateTexture();
        }
        Debug.Log("Update Texture");
        RandomiseValues();
        Graphics.Blit(GenerateTexture(), target);
    }

    [ContextMenu("Clear Water Mask")]
    public void ClearWaterMask()
    {
        // https://forum.unity.com/threads/how-to-clear-a-render-texture-to-transparent-color-all-bytes-at-0.147431/
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = waterMask;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;

    }

    [ContextMenu("Randomise Texture")]
    void RandomiseValues()
    {
        scale = UnityEngine.Random.Range(5, 10);
        offset_X = UnityEngine.Random.Range(-999f, 999f); // transform.position.x;
        offset_Y = transform.position.y;

    }

    [ContextMenu("Update All Render Textures")]
    void UpdateRenderTextures()
    {
        /*
        RandomiseValues();
        UpdateTexture(no_First);
        dataController.sessionData.Masks[1] = SaveRenderTexture(no_First, "No_First");

        RandomiseValues();
        UpdateTexture(second_Third);
        dataController.sessionData.Masks[2] = SaveRenderTexture(second_Third, "Second_Third");

        */
        UpdateTexture(globalMask);
        //dataController.sessionData.Masks[3] = SaveRenderTexture(globalMask, "GlobalMask");

    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        
        for (int i=0; i<width; i++)
        {
            for(int j=0; j<height; j++)
            {
                Color colour = CalculateColour(i, j);
                texture.SetPixel(i, j, colour);
            }

        }
        texture.Apply();
        return texture;

    }

    Texture2D GenerateCheckerTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color colour = CalculateChecker(i, j);
                texture.SetPixel(i, j, colour);
            }

        }
        texture.Apply();
        return texture;



    }

    Color CalculateChecker(int x, int y)
    {
        return new Color(0f, 0f, 0f);
    }

    Color CalculateColour(int x, int y)
    {
        float xCoord = (float)x / width * scale + offset_X;
        float yCoord = (float)y / height * scale + offset_X;

        float sample = Step(Mathf.PerlinNoise(xCoord, yCoord));
        

        return new Color(sample, 0f, 0f);
    }

    float Step(float sample)
    {
        /*
         * Take a threshold value of 0.03 and 3 bands
         * 0.35 * 3 = 1.05 = 1 / 3 = 0.33
         * if 0.35 <= 0.33 +- 0.03 then return 0.35
         */
        float stepAndBlur = sample * numOfBands;
        stepAndBlur = Mathf.Floor(stepAndBlur) / numOfBands;
        float returnVal = sample >= stepAndBlur + falloff || sample <= stepAndBlur - falloff ? sample : stepAndBlur;
        return returnVal;
    }
    string SaveRenderTexture(RenderTexture renderTexture, string fileName)
    {
        // Stores the active Render Texture
        RenderTexture oldRT = RenderTexture.active;

        // Sets up a Texture2D object the same dimensions as the input render texture
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
        RenderTexture.active = renderTexture;

        // Reads the pixels of the render texture into the local Texture2D variable
        tex.ReadPixels(new Rect(0,0,renderTexture.width, renderTexture.height), 0, 0);
        
        // Applies those pixels to the texture
        tex.Apply();

        string dir = Application.dataPath + "/../SavedImages/";

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string fileSaveName = dir + fileName + ".png";


        // Encodes the PNG file
        File.WriteAllBytes(fileSaveName, tex.EncodeToPNG());
        CustomUtility.maskPath = fileSaveName;

        RenderTexture.active = oldRT;

        return fileSaveName;
    }

    [ContextMenu("Save User Mask")]
    public void SaveWaterMask()
    {
        dataController.sessionData.mask = SaveRenderTexture(waterMask, "renderTarget_" +wallID.ToString());
    }


    public void AddToUserMask(Hit uv, float radius)
    {

    }

}
