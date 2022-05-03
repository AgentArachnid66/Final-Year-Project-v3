using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupController : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void ToggleMouse(bool active)
    {
        Cursor.lockState = active? CursorLockMode.Confined: CursorLockMode.Locked;
        Cursor.visible = active;
    }
}
