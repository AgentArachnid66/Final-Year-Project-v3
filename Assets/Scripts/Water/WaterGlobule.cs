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
        RenderTexture render = noise.RetrieveRenderTextures();
        _drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));


        Debug.LogError($"Hit Wall at UV: " +hit.textureCoord);

        DataController.sharedInstance.scoreBackup += 
            (noise.generatedNoiseTexture.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y).r * DataController.sharedInstance.scoreInput.weights[1]) +
            _score * DataController.sharedInstance.scoreInput.weights[2] + _temp * DataController.sharedInstance.scoreInput.weights[3];

        FeedbackController feedback = noise.GetComponent<FeedbackController>();
        Color[] colours = new Color[] { new Color(0, 0, 0, 1), new Color(0,1,0,0), new Color(0,0,1,0)};

        if (!ReferenceEquals(null, feedback))
        {
            noise.GetComponent<FeedbackController_Lighting>().AdjustFeedback(_score, _temp);
        }
        for ( int i = 0; i< colours.Length; i++)
        {
            _drawMaterial.SetFloat("_ColourMultiplier", offsets[i]);
            _drawMaterial.SetColor("_Colour", colours[i]);
            RenderTexture temp = RenderTexture.GetTemporary(render.width, render.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(render, temp);
            Graphics.Blit(temp, render, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }

        noise.WallHitPressTemp.Invoke(_score, _temp);

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
