using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobuleHandle : MonoBehaviour
{
    // This class will be used to transfer data to the shader as to where each globule is
    // It will also be used to keep track of the globule objects and put them back into the pool when necessary

    public DataController dataController;
    public Renderer raymarcher;

    public float _globRadius;

    private bool[] activeGlobules;


    private void Start()
    {
        activeGlobules = new bool[GlobuleObjectPool.sharedInstance.amountToPool];
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Globule")
        {

            UpdateShader(other.gameObject);

            // Gets the index of the globule within the object pool
            // This corresponds with the index on the float4 array on the shader (Not yet implemented)
            int index = GlobuleObjectPool.sharedInstance.GetObjectIndex(other.gameObject);
            if (index >= 0)
            {
                activeGlobules[index] = true;
            }

            // Need to make a way to make sure that the capsules are connected appropriately, 
            // without needing the globules to be taken out of the pool sequentially

        }
    }

    #region Deprecated 

    // Requires the Ray Marcher Plugin which might not be used

    private void LateUpdate()
    {
        for(int i = 0; i < activeGlobules.Length; i++)
        {
            if (activeGlobules[i])
            {
                UpdateShader(GlobuleObjectPool.sharedInstance.pooledObjects[i]);
                UpdateSDF(GlobuleObjectPool.sharedInstance.pooledObjects[i], i >= 1);
            }
        }
    }

    private void UpdateShader(GameObject other)
    {
        // Gets the index of the object within the pool
        int index = GlobuleObjectPool.sharedInstance.GetObjectIndex(other);

        // Attachs the appropriate property name to the index
        string prop = "_Glob" + index.ToString();

        Debug.Log(prop);

        // Updates the Shader
        raymarcher.material.SetVector(Shader.PropertyToID(prop), other.transform.position - raymarcher.transform.position);
    }

    private void UpdateSDF(GameObject gameObject, bool glob1)
    {
        Vector4 update = new Vector4(gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z,
                _globRadius);        
    }

    #endregion


    [ContextMenu("Test")]
    public void DrawFromPool()
    {
        GameObject globule = GlobuleObjectPool.sharedInstance.GetPooledObject();
        if (globule != null)
        {
            globule.SetActive(true);
            globule.GetComponent<WaterGlobule>().dataController = dataController;
        }
    }
}
