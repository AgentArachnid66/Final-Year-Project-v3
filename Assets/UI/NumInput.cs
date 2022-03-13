using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NumInput : MonoBehaviour
{
    public TMP_Text display;
    public UnityEventInt SubmitInput = new UnityEventInt();


    [SerializeField]private int _currentInput;
    public int _deltaInput;



    [ContextMenu("Test Appending")]
    public void AppendTest()
    {
        AppendInput(_deltaInput);
    }

    private void UpdateDisplay()
    {
        display.text = _currentInput.ToString();
    }
    public void AppendInput(int num)
    {
        _currentInput *= 10;
        _currentInput += num;
        UpdateDisplay();
    }

    [ContextMenu("Remove From Input")]
    public void RemoveFromInput()
    {
        _currentInput -= _currentInput % 10;
        _currentInput /= 10;
        UpdateDisplay();
    }


    public void EnterInput()
    {
        Debug.Log("Entered the Input");
        SubmitInput.Invoke(_currentInput);
    }

    public void ClearInput()
    {
        _currentInput = 0;
        display.text = "";
    }
}
