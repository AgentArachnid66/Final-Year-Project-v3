using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct FeedbackStruct
{
    public FeedbackObject_Lighting feedBackObject;
    public float weighting;

    public FeedbackStruct(FeedbackObject_Lighting object_Lighting, float weight)
    {
        this.feedBackObject = object_Lighting;
        this.weighting = weight;
    }
}


public class FeedbackController_Lighting : FeedbackController
{
    public ColourLerping colourLerping;

    public float radius = Mathf.Infinity;
    UnityAction<Color> UpdateColour;


    [SerializeField]
    private List<FeedbackStruct> nodes = new List<FeedbackStruct>();
    public Vector3 offset;

    void Start() {

        FeedbackObject_Lighting[] arrayFeedback = FindObjectsOfType<FeedbackObject_Lighting>();
        List<FeedbackObject_Lighting> value = new List<FeedbackObject_Lighting>();

        foreach (FeedbackObject_Lighting item in arrayFeedback)
        {
            if(Vector3.Distance(item.transform.position, this.transform.position) < radius)
            {
                value.Add(item);
                nodes.Add(new FeedbackStruct(item, Vector3.Distance(item.transform.position, this.transform.position)/radius));
                item.numBranches++;
            }
        }

        feedbackObjects = value.ToArray();

        UpdateColour += (ctx =>
            {
                Debug.Log($"Recieved Event to Update the Colour to: {ctx}");

                for (int i = 0; i < nodes.Count; i++) {
                    if (!ReferenceEquals(nodes[i], null))
                    {
                        nodes[i].feedBackObject.AdjustFeedback(ctx*nodes[i].weighting);
                    }
                    }
            });

        CustomEvents.CustomEventsInstance.HitWallPressureTemp.AddListener(GetCurrentColour);

        
    }

    private void Update()
    {
        UpdateColour.Invoke( colourLerping.SetLerpValue(Mathf.InverseLerp(0, 2,(testPress + testTemp))));
    }

    void GetCurrentColour(float temp, float press)
    {
        UpdateColour.Invoke(colourLerping.SetLerpValue((temp + press) / 2));
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawSphere(transform.position+ offset, radius);
    }
}
