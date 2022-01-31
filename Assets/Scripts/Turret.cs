using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    // The object that the turret is facing
    public GameObject target;
    public Vector2 minPos;
    public Vector2 maxPos;
    public float distanceHint;
    public Vector3 origin;
    public Vector3 scaleOffset;
    public float distanceMulitplier;
    public float height;

    public bool on = false;
    public float pressure;
    public Transform waterSpout;
    public float timestep;
    public float testTheta;
    public float theta;

    

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }


    // 
    // For the curved screen
    // Use parametric equation to get the position
    // To get the position on the texture to grab the displacement value
    // map the parametric equation values of X and Y to UV space and sample the 
    // texture


    private void Update()
    {
        transform.position = GetPosition(distanceMulitplier);
        transform.LookAt(target.transform);
    }

    Vector3 GetPosition(float displacement)
    {
        Vector3 returnValue = Vector3.zero;

        returnValue.x = scaleOffset.x* (distanceHint * Mathf.Cos(testTheta));
        returnValue.z = scaleOffset.z * (distanceHint * Mathf.Sin(testTheta));

        returnValue.y = height;


        return returnValue + origin;
    }



    public void ToggleActive(bool active)
    {
        on = active;
    }

    [ContextMenu("Shoot Test")]
    void ShootGlobule()
    {
        Debug.Log("Receivied");
        GameObject globule = GlobuleObjectPool.sharedInstance.GetPooledObject();
        if (globule != null)
        {
            globule.SetActive(true);
            Debug.Log(waterSpout.transform.position);
            globule.transform.position = waterSpout.transform.position;
            globule.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * pressure);
            globule.GetComponent<WaterGlobule>().pressure = pressure;
            Debug.Log("Shooting Globule");
        }
    }

    public void LateUpdate()
    {
        
    }


}
