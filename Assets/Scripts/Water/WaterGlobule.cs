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

    [SerializeField] private LayerMask _mask;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + launchVelocity * -1, transform.position + launchVelocity * 500f);
    }

    private void Start()
    {
        
        _rigidBody = GetComponent<Rigidbody>();
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Colour", Color.red);

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.LogError("Collided with " + other.name);

        Ray ray = new Ray(transform.position + launchVelocity * -1, transform.position + launchVelocity * 500f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _mask))
        {

            Debug.Log("Hit: " + hit.textureCoord.ToString());
            NoiseGenerator noise = hit.transform.GetComponent<NoiseGenerator>();
            if (noise != null)
            {
                _targetRender = noise.waterMask;
                // If the raycast hits, then apply the brush to the render target at the specific UV location
                dataController.sessionData.HitLocations.Add(new Hit(hit.textureCoord, pressure, noise.wallID));

                _wallMaterial = hit.transform.GetComponent<MeshRenderer>().material;

                _drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                RenderTexture temp = RenderTexture.GetTemporary(_targetRender.width, _targetRender.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_targetRender, temp);
                Graphics.Blit(temp, _targetRender, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);


                StartCoroutine(ResetGlobule(0.5f));
            }
            else
            {
                Debug.Log("Noise Null, hit " + hit.rigidbody.name);
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
