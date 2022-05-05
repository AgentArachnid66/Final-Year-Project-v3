using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Camera droneCamera;
    [Space(10)]
    [Header("Tooltip")]
    public TMPro.TMP_Text title;
    public TMPro.TMP_Text description;
    public TMPro.TMP_Text locked;

    void Start()
    {
        foreach (RadialElement element in menuItems)
        {
            Vector3 directionVector = Vector3.Normalize(element.transform.localPosition);

            element.localAngle = (directionVector.x > 0) ? 
                Vector3.Angle(Vector3.up, Vector3.Normalize(directionVector)) : 
                360.0f - Vector3.Angle(Vector3.up, Vector3.Normalize(directionVector));
            
            Debug.Log($"Angle: {element.localAngle}");
        }

        List<RadialElement> sortedList = menuItems.ToList();
        sortedList.Sort(CustomUtility.CompareByAngleValue);

        menuItems = sortedList.ToArray();

        int index = 0;
        foreach (RadialElement element in menuItems)
        {
            Debug.Log($"[{index}] - Angle: {element.localAngle}");
            index++;
        }
        
        
        /*
        float sum = 0f;
        for(int i = 0; i < menuItems.Length; i++)
        {
            sum += menuItems[i].fillAmount;
        }

        float start = 0f;

        for (int i = 0; i < menuItems.Length; i++)
        {
            // Gets fill amount in terms of degrees
            //menuItems[i].arcLength = (menuItems[i].fillAmount / sum) * 360f;
            
            //Vector3 viewportPoint = droneCamera.ScreenToViewportPoint(Input.mousePosition);
            //Vector3 directionVector = menuItems[i].transform.localPosition; // - new Vector3(0.5f, 0.5f, 0.0f);
            
            // Gets the angle local to the parent, 
            menuItems[i].localAngle = Mathf.Atan2(menuItems[i].transform.localPosition.y, menuItems[i].transform.localPosition.x) * Mathf.Rad2Deg;


            // Min angle = if first element then local angle - (arc length /2) otherwise it is the previous max angle
            menuItems[i].minAngle = menuItems[i].localAngle - (menuItems[i].arcLength/2f);
            menuItems[i].minAngle += 360;
            menuItems[i].minAngle %= 360;

            // Max angle = local angle + arc length /2
           menuItems[i].maxAngle = menuItems[i].localAngle + (menuItems[i].arcLength / 2f);
            menuItems[i].maxAngle += 360;
            menuItems[i].maxAngle %= 360;

            menuItems[i].offset = menuItems[i].minAngle > menuItems[i].maxAngle ? 360f - menuItems[i].minAngle : 0f;
        }
        */

        CustomEvents.CustomEventsInstance.Select.AddListener(PressSelection);
        CustomEvents.CustomEventsInstance.ToggleRadialMenu.AddListener(ToggleMenu);


        CustomEvents.CustomEventsInstance.NavigateMenu.AddListener(ctx =>{
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

        CustomEvents.CustomEventsInstance.AdjustAngle.AddListener(mouseAngle =>{
            if (_active)
            {
                int angleIndex = -1;
                bool hasFoundValidAngle = false;
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (mouseAngle < menuItems[i].localAngle)
                    {
                        angleIndex = i;
                        hasFoundValidAngle = true;
                        break;
                    }
                }

                float firstAngle = 0, secondAngle = 0;
                int selectedIndex = 0;
                if (hasFoundValidAngle)
                {
                    if (angleIndex == 0)
                    {
                        firstAngle = menuItems[angleIndex].localAngle;
                        secondAngle = menuItems[menuItems.Length - 1].localAngle - 360;
                        
                        selectedIndex = (Mathf.Abs(mouseAngle - firstAngle) < Mathf.Abs(secondAngle - mouseAngle)) ? 0 : menuItems.Length - 1;
                    }
                    else
                    {
                        firstAngle = menuItems[angleIndex].localAngle;
                        secondAngle = menuItems[angleIndex - 1].localAngle;
                        
                        selectedIndex = (Mathf.Abs(mouseAngle - firstAngle) < Mathf.Abs(secondAngle - mouseAngle)) ? angleIndex : angleIndex - 1;
                    }
                }
                else
                {
                    firstAngle = menuItems[menuItems.Length - 1].localAngle;
                    secondAngle = menuItems[0].localAngle + 360;
                    
                    selectedIndex = (Mathf.Abs(mouseAngle - firstAngle) < Mathf.Abs(secondAngle - mouseAngle)) ? menuItems.Length - 1 : 0;
                }

                selection = selectedIndex;
                Debug.Log($"Index: {selectedIndex}");
                
                if (_prevSelection != selectedIndex && selectedIndex>=0)  
                {
                    Debug.Log($"Deselecting: {_prevSelection}");
                    menuItems[_prevSelection].Deselect();
                    menuItems[selectedIndex].Select();
                    
                    title.text = menuItems[selectedIndex].title;
                    description.text = menuItems[selectedIndex].description;
                    locked.text = menuItems[selectedIndex].lockedDescription;
                }
                
                _prevSelection = selectedIndex;
            }
        });


        gameObject.SetActive(false);
    }

    void Update()
    {
    }

    private int GetCurrentIndex()
    {
        /*
        int index = selection;
        
        bool hasfound = false;
        int selectedIndex = 0;
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (angle < menuItems[i].angle)
        }

        return index;
        */
        return 0;
    }

    void PressSelection()
    {
        Debug.Log("Selection Pressed");
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