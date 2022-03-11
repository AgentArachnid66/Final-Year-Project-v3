using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RadialMenu : MonoBehaviour
{

    [SerializeField]private Vector2 _position;
    public float currentAngle;
    public int selection;

    public RadialElement[] menuItems;

    private int _prevSelection;
    private bool _active;
    void Start()
    {
        CustomEvents.CustomEventsInstance.Select.AddListener(PressSelection);


        CustomEvents.CustomEventsInstance.ToggleRadialMenu.AddListener(ctx =>
        {
            // If false, then need to send the data to the relevant scripts before disabling the game object
            _active = ctx;
            gameObject.SetActive(ctx);
        });


        CustomEvents.CustomEventsInstance.LeftAnalog.AddListener(ctx =>{
            if (_active)
            {
                _position = ctx;

                currentAngle = Mathf.Atan2(_position.y, _position.x) * Mathf.Rad2Deg;
                currentAngle += 360;
                currentAngle %= 360;

                selection = (int)Mathf.Clamp((currentAngle / (360 / menuItems.Length)), 0, menuItems.Length);
                if (_prevSelection != selection)
                {
                    menuItems[_prevSelection].Deselect();
                    menuItems[selection].Select();
                }
                _prevSelection = selection;
            }

        });

        gameObject.SetActive(false);
    }

    void Update()
    {
    }

    void PressSelection()
    {
        menuItems[selection].Press();
    }

    public void TestRadialMenuPress(string name)
    {
        Debug.Log("The Button Name is: " +name);
    }

}
