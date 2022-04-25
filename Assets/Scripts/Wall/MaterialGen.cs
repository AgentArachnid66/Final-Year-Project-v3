using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Texture Array Generator", menuName = "ScriptableObjects/Texture Array Generator", order = 1)]
public class MaterialGen : ScriptableObject
{
    public Texture2D[] ordinaryTextures;
    public string property;
    public Renderer renderer;
    public int materialIndex;
    
    
    [ContextMenu("Create Array")]
    public void CreateTextureArray()
    {
        // Create Texture2DArray
        Texture2DArray texture2DArray = new
            Texture2DArray(ordinaryTextures[0].width,
            ordinaryTextures[0].height, ordinaryTextures.Length,
            TextureFormat.RGBA32, true, false);
        // Apply settings
        texture2DArray.filterMode = FilterMode.Bilinear;
        texture2DArray.wrapMode = TextureWrapMode.Repeat;
        // Loop through ordinary textures and copy pixels to the
        // Texture2DArray
        for (int i = 0; i < ordinaryTextures.Length; i++)
        {
            texture2DArray.SetPixels(ordinaryTextures[i].GetPixels(0),
                i, 0);
        }
        // Apply our changes
        texture2DArray.Apply();
        
        GlobalController.SharedInstance.PropagateTextureArray.Invoke(property, texture2DArray);
        
    }
}
