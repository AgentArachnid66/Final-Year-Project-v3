using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGlobule : MonoBehaviour
{
    public DataController dataController;
    public float pressure;
    public Vector3 launchVelocity;

    private Rigidbody _rigidBody;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + launchVelocity * 500f);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Ray ray = new Ray(transform.position, transform.position + launchVelocity * 500f);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                Debug.Log("Hit: " + hit.textureCoord.ToString());
                // If the raycast hits, then apply the brush to the render target at the specific UV location
                dataController.sessionData.HitLocations.Add(new Hit(hit.textureCoord, pressure));

                StartCoroutine(ResetGlobule(0.5f));
                
            }


        }
    }


    public IEnumerator ResetGlobule(float lifetime)
    {
        Debug.LogWarning("Reseting Globule");

        yield return new WaitForSeconds(lifetime);

        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;

        this.gameObject.SetActive(false);

        Debug.LogWarning("Globule Reset");

    }

}
