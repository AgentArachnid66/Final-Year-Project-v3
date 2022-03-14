using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NumInput : MonoBehaviour
{
    public TMP_Text display;
    public UnityEventInt SubmitInput = new UnityEventInt();


    public int _deltaInput;
    private string _currentString= "";


    [ContextMenu("Test Appending")]
    public void AppendTest()
    {
        AppendInput(_deltaInput);
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
        _currentString = _currentString.Substring(0, _currentString.Length - 1);
        UpdateDisplay();
    }


    public void EnterInput()
    {
        int[] values = _currentString.ToIntArray();
        int _currInput=0;

        for(int i = 0; i < values.Length; i++)
        {
            _currInput *= 10;
            _currInput += values[i];

        }
        Debug.Log($"Entered the Input {_currInput}");
        SubmitInput.Invoke(_currInput);
    }

    public void ClearInput()
    {
        display.text = "";
    }
}
