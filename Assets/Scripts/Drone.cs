using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float theta;
    public Vector3 origin;
    public float radius;
    public float height;
   
    public Vector3 scaleOffset;
    public float minTheta;
    public float maxTheta;
    public float thetaOffset;
    public float heightBounds;

    private Texture2D displacement;



    void Update()
    {
        transform.position = GetPosition() + origin;
        transform.LookAt(target.transform);
    }


    // The object that the turret is facing
    public GameObject target;

    //
    public bool on = false;
    public float pressure;
    public Transform waterSpout;
    public float testTheta;



    // Start is called before the first frame update
    void Start()
    {
        CustomEvents.CustomEventsInstance.DroneHorizontal.AddListener(UpdateThetaDelta);
        CustomEvents.CustomEventsInstance.DroneVertical.AddListener(UpdateHeightDelta);
        CustomEvents.CustomEventsInstance.Shoot.AddListener(ShootGlobule);


        origin = transform.position;
    }




    // 
    // For the curved screen
    // Use parametric equation to get the position
    // To get the position on the texture to grab the displacement value
    // map the parametric equation values of X and Y to UV space and sample the 
    // texture

    #region Movement


    float MapToUV()
    {
        if (displacement == null)
        {
            displacement = CustomUtility.LoadPNG(CustomUtility.maskPath);
            return 0f;

        }
        else
        {

            // Get the bounds of the target

            // Normalise the current values between these bounds to get 
            // a value between 0-1 to get the UV coordinate
            testTheta = Mathf.Clamp(testTheta + thetaOffset, minTheta + thetaOffset, maxTheta + thetaOffset);


            float u = Mathf.InverseLerp(minTheta + thetaOffset, maxTheta + thetaOffset, testTheta);
            float v = Mathf.InverseLerp(-heightBounds, heightBounds, height);

            float displaceValue = displacement.GetPixelBilinear(1-u, v).r;

            Debug.Log($"U: {1-u}, V:{v}, Displacement: {displaceValue}");

            return displaceValue;
        }
    }

    void UpdateThetaDelta(float deltaTheta)
    {
        theta += deltaTheta;
    }

    void UpdateThetaAbs(float newTheta)
    {
        theta = newTheta;
    }

    void UpdateHeightDelta(float deltaHeight)
    {
        height += deltaHeight;
    }

    void UpdateHeightAbs(float newHeight)
    {
        height = newHeight;
    }

    Vector3 GetPosition()
    {
        Vector3 returnValue = Vector3.zero;

        returnValue.x = scaleOffset.x * (radius * Mathf.Cos(testTheta + thetaOffset));
        returnValue.z = scaleOffset.z * (radius * Mathf.Sin(testTheta + thetaOffset));

        returnValue.y = height;


        return returnValue;
    }


    #endregion

    #region Shooting

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

    #endregion

    public void LateUpdate()
    {

    }


}
