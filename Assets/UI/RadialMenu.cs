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
    private float _currMinAngle;
    private float _currMaxAngle;
    void Start()
    {
        float sum = 0f;
        for(int i = 0; i < menuItems.Length; i++)
        {
            sum += menuItems[i].fillAmount;
        }

        float start = 0f;

        for (int i = 0; i < menuItems.Length; i++)
        {
            menuItems[i].minAngle = (Mathf.Atan2(menuItems[i].transform.localPosition.y, menuItems[i].transform.localPosition.x) * Mathf.Rad2Deg) - ((menuItems[i].fillAmount / sum) * 360f) / 2f;

            menuItems[i].minAngle += 360;
            menuItems[i].minAngle %= 360;

            menuItems[i].maxAngle = (Mathf.Atan2(menuItems[i].transform.localPosition.y, menuItems[i].transform.localPosition.x) * Mathf.Rad2Deg) + ((menuItems[i].fillAmount / sum) * 360f) / 2f;

            menuItems[i].maxAngle += 360;
            menuItems[i].maxAngle %= 360;

            menuItems[i].offset = menuItems[i].minAngle > menuItems[i].maxAngle ? 360f - menuItems[i].minAngle : 0f;

            start = menuItems[i].maxAngle;

        }
        

        CustomEvents.CustomEventsInstance.Select.AddListener(PressSelection);


        CustomEvents.CustomEventsInstance.ToggleRadialMenu.AddListener(ToggleMenu);


        CustomEvents.CustomEventsInstance.LeftAnalog.AddListener(ctx =>{
            if (_active)
            {
                _position = ctx;

                currentAngle = Mathf.Atan2(_position.y, _position.x) * Mathf.Rad2Deg;
                currentAngle += 360;
                currentAngle %= 360;

                //(int)Mathf.Clamp((currentAngle / (360 / menuItems.Length)), 0, menuItems.Length)
                selection = GetCurrentIndex();
                if (_prevSelection != selection && selection>=0)  
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

    private int GetCurrentIndex()
    {
        int index = selection;

        for (int i = 0; i < menuItems.Length; i++)
        {
            float min = menuItems[i].minAngle + menuItems[i].offset >= 360 ? 0 : menuItems[i].minAngle + menuItems[i].offset;
            if ((min < currentAngle + menuItems[i].offset) && (menuItems[i].maxAngle + menuItems[i].offset > currentAngle + menuItems[i].offset))
            {
                index = i;
            }

        }

        return index;
    }

    void PressSelection()
    {
        menuItems[selection].Press();
    }

    public void TestRadialMenuPress(string name)
    {
        Debug.Log("The Button Name is: " +name);
    }

    public void ToggleMenu(bool toggle, float input)
    {
        _active = toggle;
        gameObject.SetActive(_active);
    }

}
