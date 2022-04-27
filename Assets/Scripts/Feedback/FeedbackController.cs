﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum FeedbackType
{
    Lighting,
    Audio
}

[RequireComponent(typeof(NoiseGenerator))]
public class FeedbackController : MonoBehaviour
{
    public FeedbackObject[] feedbackObjects;

    public AnimationCurve pressureCurve;
    public AnimationCurve temperatureCurve;

    [SerializeField]
    protected float testPress;
    [SerializeField]
    protected float testTemp;

    [SerializeField]
    protected List<FeedbackStruct> nodes = new List<FeedbackStruct>();
    public Vector3 offset;
    public float radius = Mathf.Infinity;

    protected NoiseGenerator noiseGenerator;

    public FeedbackType feedbackType;
    private void OnEnable()
    {
        
    }

    public virtual void Start()
    {
        noiseGenerator = GetComponent<NoiseGenerator>();

        FeedbackObject[] arrayFeedback;



        switch (feedbackType)
        {
            case FeedbackType.Lighting:
                arrayFeedback = FindObjectsOfType<FeedbackObject_Lighting>();
                break;
            case FeedbackType.Audio:
                arrayFeedback = FindObjectsOfType<FeedbackObjectAudio>();
                break;
            default:
                arrayFeedback = FindObjectsOfType<FeedbackObject>();
                break;
        }


        List<FeedbackObject> value = new List<FeedbackObject>();

        foreach (FeedbackObject item in arrayFeedback)
        {
            if (Vector3.Distance(item.transform.position, this.transform.position) < radius)
            {
                value.Add(item);
                // 1 - (distance to origin/radius) so that the closer the object to origin, the more weighting it has
                nodes.Add(new FeedbackStruct(item, 1f - (Vector3.Distance(item.transform.position, this.transform.position) / radius), item.evaluatedControllers.Count));

                Debug.LogWarning(item.evaluatedControllers.Count);

                // Useful for finding the proportional weight for each branch connected to this controller
                item.totalWeighting += 1f - (Vector3.Distance(item.transform.position, this.transform.position) / radius);



                item.evaluatedControllers.Add(0f);
                Debug.LogWarning(item.evaluatedControllers.Count);
            }
        }

        feedbackObjects = value.ToArray();

        if (noiseGenerator != null)
        {
            noiseGenerator.WallHitPressTemp.AddListener(AdjustFeedback);
        }
        Debug.LogWarning("Base Start has been called");

    }
    private void Update()
    {
    }

    public virtual void AdjustFeedback(float pressure, float temperature)
    {

    }


    public void TestAdjustment()
    {
    }

}
