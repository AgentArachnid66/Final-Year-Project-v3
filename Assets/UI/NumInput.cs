using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NumInput : MonoBehaviour
{
    public TMP_Text display;
    public UnityEventInt SubmitInput = new UnityEventInt();
    public UnityEventString SubmitInputString = new UnityEventString();
    public UnityEventInt AppendInputEvent = new UnityEventInt();
    public UnityEventInt RemoveInputEvent = new UnityEventInt();


    public int _deltaInput;
    private string _currentString= "";
    public string _deltaInputStr;


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

    public void AppendInput(string str)
    {
        _currentString += str;
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
        SubmitInputString.Invoke(_currentString);
        SubmitInput.Invoke(_currInput);
    }

    public void ClearInput()
    {
        _currentString = "";
        display.text = "";
    }

    public void CheckThenSubmit()
    {
        Debug.Log($"This is {gameObject.name} and we are {(gameObject.activeInHierarchy ? "Active" : "Inactive")}");
        if (gameObject.activeInHierarchy)
        {
            EnterInput();
        }

    }

    public void CheckThenDelete()
    {
        if (gameObject.activeInHierarchy)
        {
            RemoveFromInput();
        }
    }
}
