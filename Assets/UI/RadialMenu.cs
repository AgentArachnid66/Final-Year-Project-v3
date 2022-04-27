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

    [Space(10)]
    [Header("Tooltip")]
    public TMPro.TMP_Text title;
    public TMPro.TMP_Text description;
    public TMPro.TMP_Text locked;

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
            // Gets fill amount in terms of degrees
            menuItems[i].arcLength = (menuItems[i].fillAmount / sum) * 360f;

            // Gets the angle local to the parent, 
            menuItems[i].localAngle = (Mathf.Atan2(menuItems[i].transform.localPosition.y, menuItems[i].transform.localPosition.x) * Mathf.Rad2Deg);


            // Min angle = if first element then local angle - (arc length /2) otherwise it is the previous max angle
            menuItems[i].minAngle = i == 0 ? menuItems[i].localAngle - (menuItems[i].arcLength/2f) : start;
            menuItems[i].minAngle += 360;
            menuItems[i].minAngle %= 360;

            // Max angle = local angle + arc length /2
            menuItems[i].maxAngle = menuItems[i].localAngle + (menuItems[i].arcLength / 2f);
            menuItems[i].maxAngle += 360;
            menuItems[i].maxAngle %= 360;

            menuItems[i].offset = menuItems[i].minAngle > menuItems[i].maxAngle ? 360f - menuItems[i].minAngle : 0f;

            //

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
                    title.text = menuItems[selection].title;
                    description.text = menuItems[selection].description;
                    locked.text = menuItems[selection].lockedDescription;
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

    public void ToggleMenu(bool toggle)
    {
        _active = toggle;
        gameObject.SetActive(_active);
    }

}
