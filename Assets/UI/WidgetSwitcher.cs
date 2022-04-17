using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[System.Serializable]
public struct WidgetSwitcherChild
{
    public GameObject root;
    public GameObject initialSelect;
}

[ExecuteInEditMode]
public class WidgetSwitcher : MonoBehaviour
{

    [SerializeField]
    public WidgetSwitcherChild[] children;
    public int currentIndex;

    private void Start()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (i != currentIndex)
            {
                children[i].root.SetActive(false);
            }
            else
            {
                children[i].root.SetActive(true);
            }
        }
    }

    [ContextMenu("Refresh")]
    public void RefreshSwitch()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (i != currentIndex)
            {
                children[i].root.SetActive(false);
            }
            else
            {
                children[i].root.SetActive(true);
            }
        }
    }

    public void SetActiveIndex(int index)
    {
        if (index != currentIndex)
        {
            children[currentIndex].root.SetActive(false);
            children[index].root.SetActive(true);
            EventSystem.current.SetSelectedGameObject(children[index].initialSelect);
            currentIndex = index;
        }
    }

    public void SetActiveChild(GameObject child)
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (child != children[i].root)
            {
                children[i].root.SetActive(false);
            }
            else
            {
                children[i].root.SetActive(true);
                EventSystem.current.SetSelectedGameObject(children[i].initialSelect);
                Debug.Log($"Currently Selected Game Object is {EventSystem.current.currentSelectedGameObject.name}");
            }
        }

    }
}
