using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetupController : MonoBehaviour
{
    bool _active = false;



    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void ToggleMouse(bool active)
    {
        _active = active;
        Cursor.lockState = active? CursorLockMode.Confined: CursorLockMode.Locked;
        Cursor.visible = active;
    }

}
