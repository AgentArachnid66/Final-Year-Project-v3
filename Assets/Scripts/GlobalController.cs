using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalController : MonoBehaviour
{

    public CustomEvents customEvents;
    public Renderer wallMat;
    public float startingRadius;
    public float endRadius;
    public float totalTime;

    private bool activate = false;
    private int direction = 1;
    private float elapsedTime;

    private void Awake()
    {
        customEvents.StartSimulation.AddListener(ActivateSimulation);
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {

            wallMat.material.SetFloat(Shader.PropertyToID("_Radius"), Mathf.SmoothStep(startingRadius, endRadius, Mathf.Clamp01(elapsedTime / totalTime)));
            elapsedTime += Time.deltaTime * direction;
            elapsedTime = Mathf.Clamp(elapsedTime, 0.0f, totalTime);
        }


    }

    [ContextMenu("Activate Simulation")]
    public void ActivateSimulation()
    {
        Debug.Log("Activate");
        Debug.Log(Shader.PropertyToID("Radius"));
        activate = true;
        direction *= -1;
    }


}
