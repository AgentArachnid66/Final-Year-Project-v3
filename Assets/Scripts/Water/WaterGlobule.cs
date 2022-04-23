using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGlobule : MonoBehaviour
{
    public DataController dataController;
    public float pressure;
    public Vector3 launchVelocity;

    [SerializeField]private Rigidbody _rigidBody;
    public Shader _drawShader;
    private Material  _drawMaterial, _waterMaterial;
    

    [SerializeField] private LayerMask _mask;
    private Vector3 _globuleOffset;
    private bool _reseting;
    private float _score;
    private float _temp;
    [SerializeField]private float[] offsets = new float[3];

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + launchVelocity *-1, transform.position + launchVelocity * 500f);
    }

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Colour", Color.red);
        _waterMaterial = GetComponent<Renderer>().material;
        _globuleOffset = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), Random.Range(-500, 500));
    }

    private void OnTriggerEnter(Collider other)
    {
        Ray ray = new Ray(transform.position + _rigidBody.velocity * -1, transform.position + _rigidBody.velocity * 500f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, _mask))
        {
            Debug.Log("Hit: " + hit.textureCoord.ToString());
            NoiseGenerator noise = hit.transform.GetComponent<NoiseGenerator>();
            if (noise != null)
            {
                PaintToNoiseTarget(noise, hit);
                Debug.LogError("Hit Wall");
                // If the raycast hits, then apply the brush to the render target at the specific UV locationlaunc
                //dataController.sessionData.HitLocations.Add(new Hit(hit.textureCoord, _score, _temp ,noise.wallID));

                //_wallMaterial = hit.transform.GetComponent<MeshRenderer>().material;

                
            }
            else
            {
                Debug.Log("<color=#FF0000>The Noise Was Null</color>");
            }
        }

    }


    private void FixedUpdate()
    {
        _waterMaterial.SetVector("Vector3_B9E3988F", transform.position + _globuleOffset);
        launchVelocity = _rigidBody.velocity.normalized;
    }

    private void PaintToNoiseTarget(NoiseGenerator noise, RaycastHit hit)
    {
        DataController.sharedInstance.sessionData.HitLocations.Add(new Hit(hit.textureCoord, _score, _temp, noise.wallID));
        RenderTexture[] renders = noise.RetrieveRenderTextures();
        _drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));

        DataController.sharedInstance.scoreBackup += 
            (noise.generatedNoiseTexture.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y).r * DataController.sharedInstance.scoreInput.weights[1]) +
            _score * DataController.sharedInstance.scoreInput.weights[2] + _temp * DataController.sharedInstance.scoreInput.weights[3];

        FeedbackController feedback = noise.GetComponent<FeedbackController>();

        if (!ReferenceEquals(null, feedback))
        {
            noise.GetComponent<FeedbackController_Lighting>().AdjustFeedback(_score, _temp);
        }
        for ( int i = 0; i< renders.Length; i++)
        {
            RenderTexture render = renders[i];
            _drawMaterial.SetFloat("_ColourMultiplier", offsets[i]);
            RenderTexture temp = RenderTexture.GetTemporary(render.width, render.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(render, temp);
            Graphics.Blit(temp, render, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
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

    public void SetVars(float newScore, float newTemp)
    {
        _score = newScore;
        _temp = newTemp;
        offsets[0] = 1f;
        offsets[1] = _temp;
        offsets[2] = _score;
    }


}
