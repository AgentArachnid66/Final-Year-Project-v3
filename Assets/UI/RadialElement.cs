using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RadialElement : MonoBehaviour
{
    public Image icon;

    [Space(10)]
    public Color normal;
    public Color highlight;
    public Color pressed;

    public UnityEvent OnPress = new UnityEvent();

    void Awake()
    {
    }

    void Start()
    {
        icon = GetComponent<Image>();
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
