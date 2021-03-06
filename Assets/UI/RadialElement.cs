using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RadialElement : MonoBehaviour
{
    public Image icon;

    [Space(10)]
    [Header("Colour Settings")]
    public Color normal;
    public Color highlight;
    public Color pressed;
    public float fillAmount=1f;

    [Space(10)]
    [Header("Alpha")]
    [SerializeField] private bool _overrideAlpha;
    public float alpha;


    [Space(10)]
    [Header("Radial Variables")]
    // Based on the fill amount, the min and max angle will be calculated and store in order to optimise the menu
    public float minAngle;
    public float maxAngle;
    public float offset;
    // Total angle between min and max angles
    public float arcLength;
    public float localAngle;

    [Space(10)]
    [Header("Element Information")]
    public string title;
    public string description;
    public string lockedDescription;

    public UnityEvent OnPress = new UnityEvent();

    void Awake()
    {
    }

    void Start()
    {
        if (_overrideAlpha)
        {
            normal.a = alpha;
            highlight.a = alpha;
            pressed.a = alpha;
        }

        
        icon.color = normal;

    }

    public void Select()
    {
        icon.color = highlight;
    }

    public void Deselect()
    {
        icon.color = normal;
    }

    public void Press()
    {
        OnPress.Invoke();
    }
}
