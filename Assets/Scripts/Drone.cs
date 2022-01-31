using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float theta;
    public Vector3 origin;
    public float radius;
    public Vector3 scaleoffset;

    public void UpdateParametricPosition()
    {

        float x = origin.x + (radius * Mathf.Cos(theta));
        float z = origin.y + (radius * Mathf.Sin(theta));

        transform.position = new Vector3(x, transform.position.y, z);


    }

    void Update()
    {
        UpdateParametricPosition();
    }
}
