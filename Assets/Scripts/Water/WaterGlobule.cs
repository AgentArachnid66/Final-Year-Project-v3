using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGlobule : MonoBehaviour
{
    public DataController dataController;
    public float pressure;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {


            Ray ray = new Ray(gameObject.transform.position, other.gameObject.transform.position - gameObject.transform.position);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {

                Debug.Log("Hit: " + hit.textureCoord.ToString());
                // If the raycast hits, then apply the brush to the render target at the specific UV location
                dataController.sessionData.HitLocations.Add(new Hit(hit.textureCoord, pressure));
            }


        }
    }

}
