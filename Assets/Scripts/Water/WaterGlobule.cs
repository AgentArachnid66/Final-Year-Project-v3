using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGlobule : MonoBehaviour
{
    public DataController dataController;
    public float pressure;
    public Vector3 launchVelocity;

    private Rigidbody _rigidBody;
    public Shader _drawShader;
    private RenderTexture _targetRender;
    private Material _wallMaterial, _drawMaterial;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + launchVelocity * 500f);
    }

    private void Start()
    {
        
        _rigidBody = GetComponent<Rigidbody>();
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Colour", Color.red);

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
                dataController.sessionData.HitLocations.Add(new Hit(hit.textureCoord, pressure, 0));

                _wallMaterial = hit.transform.GetComponent<MeshRenderer>().material;
                _targetRender = hit.transform.GetComponent<NoiseGenerator>().waterMask;

                _drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                RenderTexture temp = RenderTexture.GetTemporary(_targetRender.width, _targetRender.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_targetRender, temp);
                Graphics.Blit(temp, _targetRender, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);


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
