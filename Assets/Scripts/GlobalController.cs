using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class GlobalController : MonoBehaviour
{

    public static GlobalController SharedInstance;

    public Renderer wallMat;
    public float startingRadius;
    public float endRadius;
    public float totalTime;

    private bool activate = false;
    private int direction = -1;
    private float elapsedTime;

    private float timeLeft = 0f;

    public float countdown_Test;
    
    private void Awake()
    {
        CustomEvents.CustomEventsInstance.StartSimulation.AddListener(ActivateSimulation);

        if (ReferenceEquals(SharedInstance, null))
        {
            SharedInstance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    [ContextMenu("Test Countdown")]
    public void TestCountdown()
    {
        StartCoroutine(StartCountDown(countdown_Test));
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

    public IEnumerator StartCountDown(float countdown)
    {
        Debug.LogError($"Started Countdown with {timeLeft+countdown} left");
        timeLeft += countdown;
        yield return new WaitWhile(CheckTime);
        Debug.LogError($"Time over");
    }

    private bool CheckTime()
    {
        timeLeft -= Time.deltaTime;
        Debug.Log($"The Time left in simulation is {timeLeft}");
        return timeLeft > 0;
    }
}
