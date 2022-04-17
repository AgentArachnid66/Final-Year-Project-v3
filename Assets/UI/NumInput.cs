using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NumInput : MonoBehaviour
{
    public TMP_Text display;
    public UnityEventInt SubmitInput = new UnityEventInt();
    public UnityEventInt AppendInputEvent = new UnityEventInt();
    public UnityEventInt RemoveInputEvent = new UnityEventInt();


    public int _deltaInput;
    private string _currentString= "";


    [ContextMenu("Test Appending")]
    public void AppendTest()
    {
        AppendInput(_deltaInput);
    }

    public void testInput(string test)
    {
        Debug.Log($"The Select Return is {test}");
    }

    private void UpdateDisplay()
    {
        display.text = _currentString;
    }
    public void AppendInput(int num)
    {
        _currentString += num.ToString();
        UpdateDisplay();
    }

    [ContextMenu("Remove From Input")]
    public void RemoveFromInput()
    {
        if (_currentString.Length > 0)
        {
            _currentString = _currentString.Substring(0, _currentString.Length - 1);
            UpdateDisplay();
        }
    }


    public void EnterInput()
    {
        int _currInput = 0;
        int.TryParse(_currentString, out _currInput);
        Debug.Log($"Entered the Input {_currInput}");
        SubmitInput.Invoke(_currInput);
    }

    public void ClearInput()
    {
        _currentString = "";
        display.text = "";
    }
}
