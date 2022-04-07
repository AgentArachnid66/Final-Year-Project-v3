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


    [SerializeField] private RenderTexture waterMask;
    [SerializeField] private RenderTexture tempMask;
    [SerializeField] private RenderTexture pressMask;
    [SerializeField] private MaterialPropertyBlock _block;

    public int wallID;

    


    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        waterMask = new RenderTexture(sourceTexture);
        tempMask = new RenderTexture(sourceTexture);
        pressMask = new RenderTexture(sourceTexture);
        globalMask = new RenderTexture(sourceTexture);

        UpdateRenderTextures();

        _block = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_block);
        _block.SetTexture("Texture2D_1C6CB6F8", waterMask);
        _block.SetTexture("Texture2D_F6700227", tempMask);
        _block.SetTexture("Texture2D_28C91406", pressMask);
        _block.SetTexture("Texture2D_CA25BCB1", globalMask);

        //_renderer.material.SetTexture("Texture2D_CA25BCB1", globalMask);
        _renderer.SetPropertyBlock(_block);


        dataController.SaveSessionAction += SaveAllMasks;
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
        UpdateTexture(globalMask);
        Graphics.Blit(sourceTexture, waterMask);
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

        string dir = Application.dataPath + "/../SavedImages/Masks/";

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
        dataController.sessionData.masks[wallID].waterMask = SaveRenderTexture(waterMask, "renderTarget_" +wallID.ToString());
    }

    [ContextMenu("Save All Masks")]
    public void SaveAllMasks()
    {
        string id ="id_" +dataController.sessionData.PlayerID.ToString();

        DataController.sharedInstance.sessionData.masks[wallID] = new WallData(
            SaveRenderTexture(globalMask, id + "_global_" + wallID.ToString()),
            SaveRenderTexture(waterMask, id + "_water_" + wallID.ToString()),
            SaveRenderTexture(pressMask, id + "_pressure_" + wallID.ToString()),
            SaveRenderTexture(tempMask, id + "_temperature_" + wallID.ToString()),
            wallID);

        DataController.sharedInstance.UpdateMaskCheck(wallID, true);
        /*
        SaveRenderTexture(globalMask, "global_" + wallID.ToString());
        Debug.Log($"Saving Mask to {CustomUtility.maskPath}");


        dataController.sessionData.masks[wallID].pressureMask = SaveRenderTexture(pressMask, "pressure_" + wallID.ToString());
        dataController.sessionData.masks[wallID].temperatureMask = SaveRenderTexture(tempMask, "temperature_" + wallID.ToString());
        
        dataController.sessionData.masks[wallID].id = wallID;
        */
    }

    public RenderTexture[] RetrieveRenderTextures()
    {
        return new RenderTexture[] {waterMask, tempMask, pressMask};
    }
}
