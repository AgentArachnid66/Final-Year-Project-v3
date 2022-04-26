using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Events;



public class NoiseGenerator : MonoBehaviour
{

    public RenderTexture sourceTexture;
    //public RenderTexture no_First;
    //public RenderTexture second_Third;
    // public RenderTexture globalMask;
    public DataController dataController;

    public int width = 1024;
    public int height = 1024;
    public float scale = 20f;
    public float offset_X;
    public float offset_Y;
    public float numOfBands;
    public float falloff;
    public float initialFloat;

    //[SerializeField] private RenderTexture waterMask;
    //[SerializeField] private RenderTexture tempMask;
    //[SerializeField] private RenderTexture pressMask;

    [SerializeField] private RenderTexture masterMask;
    private MaterialPropertyBlock _block;

    public int materialIndex;

    public int wallID;

    public UnityEventFloatFloat WallHitPressTemp = new UnityEventFloatFloat();
    private Texture2D _genNoise;

    public Texture2D generatedNoiseTexture
    {
        get => _genNoise;
    }


    private void Awake()
    {

    }

    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {


        _renderer = GetComponent<Renderer>();
        // _Global - R
        // _Pressure - G
        // _Temperature - B
        // _Water - A
        masterMask = new RenderTexture(sourceTexture);

        // waterMask = new RenderTexture(sourceTexture);
        // tempMask = new RenderTexture(sourceTexture);
        // pressMask = new RenderTexture(sourceTexture);
        // globalMask = new RenderTexture(sourceTexture);

        UpdateRenderTextures();

        _block = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_block, materialIndex);
        //_block.SetTexture("Texture2D_1C6CB6F8", waterMask);
        //_block.SetTexture("Texture2D_F6700227", tempMask);
        //_block.SetTexture("Texture2D_28C91406", pressMask);
        _block.SetTexture("Texture2D_CA25BCB1", masterMask);

        //_renderer.material.SetTexture("Texture2D_CA25BCB1", globalMask);
        _renderer.SetPropertyBlock(_block, materialIndex);

        Debug.LogError(_renderer.materials[materialIndex].name);
        DataController.sharedInstance.SaveSessionAction += SaveAllMasks;
        DataController.sharedInstance.SaveMasksAction += SaveAllMasks;

        _genNoise = RenderTextureToTexture2D(masterMask);

        DataController.sharedInstance.wallMasks.Add(wallID, generatedNoiseTexture);

        dataController = DataController.sharedInstance;
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
        RenderTexture.active = masterMask;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }

    public void SetMaterialTextureArray(string propertyName, Texture2DArray textArray)
    {
        _block = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_block, materialIndex);
        _block.SetTexture(propertyName, textArray);
        _renderer.SetPropertyBlock(_block, materialIndex);

        Debug.Log("Material Set");
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
        UpdateTexture(masterMask);
        Graphics.Blit(sourceTexture, masterMask);
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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

        float sample = (float)(UnityEngine.Random.Range(0, 12) % 4) / 4f;

        // Step(Mathf.PerlinNoise(xCoord, yCoord) + initialFloat);
        initialFloat = sample;

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
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;

        // Reads the pixels of the render texture into the local Texture2D variable
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

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

    public string CombineRenderTextures()
    {

        // DataController.sharedInstance.combine.SetTexture(Shader.PropertyToID("_Global"), globalMask);
        // DataController.sharedInstance.combine.SetTexture(Shader.PropertyToID("_Temperature"), tempMask);
        // DataController.sharedInstance.combine.SetTexture(Shader.PropertyToID("_Pressure"), pressMask);
        // DataController.sharedInstance.combine.SetTexture(Shader.PropertyToID("_Water"), waterMask);

        // RenderTexture save = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, sourceTexture.depth, RenderTextureFormat.ARGB32);
        // RenderTexture temp = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, sourceTexture.depth, RenderTextureFormat.ARGB32);
        // Graphics.Blit(save, temp);
        // Graphics.Blit(temp, save, DataController.sharedInstance.combine);

        string path = SaveRenderTexture(masterMask, DataController.sharedInstance.participantData.ID + "_" +
                                              DataController.sharedInstance.numSessions.ToString() +
                                              "_CombinedMasks_" + wallID.ToString());

        //  RenderTexture.ReleaseTemporary(save);
        //  RenderTexture.ReleaseTemporary(temp);

        return path;

    }

    Texture2D RenderTextureToTexture2D(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.R16, false);
        var old_rt = RenderTexture.active;
        RenderTexture.active = rt;

        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();

        RenderTexture.active = old_rt;
        return tex;

        // https://stackoverflow.com/questions/44264468/convert-rendertexture-to-texture2d
    }

    [ContextMenu("Save All Masks")]
    public void SaveAllMasks()
    {
        string id = "id_" + DataController.sharedInstance.sessionData.PlayerID.ToString();

        DataController.sharedInstance.sessionData.masks[wallID] = new WallData(
            CombineRenderTextures(),
            wallID);

        DataController.sharedInstance.UpdateMaskCheck(wallID, true);
    }

    public RenderTexture RetrieveRenderTextures()
    {
        return masterMask;
    }
}