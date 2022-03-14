using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleMenu : MonoBehaviour
{
    public UnityEventBool Toggle = new UnityEventBool();

    public void ConduitEvent(bool toggle)
    {
        Toggle.Invoke(toggle);
    }
    public void ConduitEvent(bool toggle, float input)
    {
        Toggle.Invoke(toggle);
    }

}
