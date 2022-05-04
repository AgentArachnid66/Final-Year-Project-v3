using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Events;

public class GlobalController : MonoBehaviour
{

    public static GlobalController SharedInstance;

    public UnityEventFloat UpdateTimeLeft = new UnityEventFloat();
    public UnityEvent CountdownComplete = new UnityEvent();


    public float totalTime;

    private float timeLeft = 0f;

    public float countdown_Test;
    
    [SerializeField] private UnityEvent GetTextureArray = new UnityEvent();
    [SerializeField] public UnityEventStringText2DArray PropagateTextureArray = new UnityEventStringText2DArray();

    private void Awake()
    {
        if (ReferenceEquals(SharedInstance, null))
        {
            SharedInstance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountDown(totalTime));
    }

    // Update is called once per frame
    void Update()
    {


    }

    public IEnumerator StartCountDown(float countdown)
    {
        Debug.LogError($"Started Countdown with {timeLeft+countdown} left");
        timeLeft += countdown;
        yield return new WaitWhile(CheckTime);
        Debug.LogError($"Time over");
        CountdownComplete.Invoke();
    }

    public IEnumerator InvokeAfterDuration(UnityEvent Event, float duration)
    {
        yield return new WaitForSeconds(duration);

        Event.Invoke();
    }


    private bool CheckTime()
    {
        timeLeft -= Time.deltaTime * Time.timeScale;
        //Debug.Log($"The Time left in simulation is {timeLeft}");
        UpdateTimeLeft.Invoke(timeLeft);
        countdown_Test = timeLeft;
        return timeLeft > 0;
    }

    public void CancelCountDown()
    {
        StopAllCoroutines();
    }

    public void Test()
    {
    }

    public void Quit()
    {
        Application.Quit();
        //Debug.LogError($"Quit Game");
    }
}
