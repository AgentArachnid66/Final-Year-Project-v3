using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CustomEvents : MonoBehaviour
{
     private static CustomEvents customEvents;
    
    public static CustomEvents CustomEventsInstance
    {
        get
        {
            if (ReferenceEquals(customEvents, null))
                customEvents = GameObject.FindObjectOfType<CustomEvents>();

            return customEvents;
        }
    }


    private InputActions _playerInput;
    // Input from the left analog stick
    private Vector2 _2dMove;
   
    private float _verticalMove;
    // Bool to determine if the player is moving up/down this frame 
    private bool _verticalMovement;

    public UnityEvent StartSimulation = new UnityEvent();

    public UnityEvent Shoot = new UnityEvent();

    public UnityEventFloat DroneHorizontal = new UnityEventFloat();
    public UnityEventFloat DroneVertical = new UnityEventFloat();
    public UnityEventFloat DroneDiagonal = new UnityEventFloat();

    public UnityEvent ResetDroneVertical = new UnityEvent();
    public UnityEvent ResetDroneHorizontal = new UnityEvent();
    public UnityEvent ResetDroneDiagonal= new UnityEvent();




    public UnityEventString ChangeScene = new UnityEventString();





    private void Awake()
    {
        _playerInput = new InputActions();
        _playerInput.Drone.Move.Enable();
        _playerInput.Drone.VerticalMoveUp.Enable();
        _playerInput.Drone.VerticalMoveDown.Enable();
        _playerInput.Drone.Shoot.Enable();

        // Player Inputs

        _playerInput.Drone.Move.performed += ctx => {
            _2dMove = ctx.ReadValue<Vector2>();
            DroneHorizontal.Invoke(_2dMove.x);
            DroneDiagonal.Invoke(_2dMove.y);
            };

        _playerInput.Drone.VerticalMoveUp.performed += ctx =>
        {
            _verticalMovement = true;
            _verticalMove = ctx.ReadValue<float>();
        };

        _playerInput.Drone.VerticalMoveDown.performed += ctx =>
        {
            _verticalMovement = true;
            _verticalMove = ctx.ReadValue<float>() * -1f;
        };

        _playerInput.Drone.Shoot.performed += ctx =>
        {
            Shoot.Invoke();
        };


        // When Player no longer inputs

        _playerInput.Drone.Move.canceled += ctx =>
        {
            _2dMove = Vector2.zero;
            ResetDroneHorizontal.Invoke();
            ResetDroneDiagonal.Invoke();
        };

        _playerInput.Drone.VerticalMoveUp.canceled += ctx =>
        {
            _verticalMovement = false;
            _verticalMove = 0f;
            ResetDroneVertical.Invoke();
        };


        _playerInput.Drone.VerticalMoveDown.canceled += ctx =>
        {
            _verticalMovement = false;
            _verticalMove = 0f;
            ResetDroneVertical.Invoke();
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSimulation.Invoke();
        }

        if (_verticalMovement) DroneVertical.Invoke(_verticalMove);

        /*
        // Vertical Movement
        if (Input.GetKey(KeyCode.Keypad8))
        {
            DroneVertical.Invoke(0.5f);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            DroneVertical.Invoke(-0.5f);
        }

        if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            ResetDroneVertical.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            ResetDroneVertical.Invoke();
        }


        // Horizontal Movement
        if (Input.GetKey(KeyCode.Keypad6))
        {
            DroneHorizontal.Invoke(0.5f);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            DroneHorizontal.Invoke(-0.5f);
        }

        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            ResetDroneHorizontal.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            ResetDroneHorizontal.Invoke();
        }        
        
        
        // Diagonal Movement
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            DroneDiagonal.Invoke(0.5f);
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            DroneDiagonal.Invoke(-0.5f);
        }

        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            ResetDroneDiagonal.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            ResetDroneDiagonal.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot.Invoke();
        }
    
        */
    }
}

public class UnityEventFloat : UnityEvent<float>
{
}

public class UnityEventString : UnityEvent<string>
{
}


public enum Liquid
{
    None,
    Water
}


public static class CustomUtility
{
    public static string maskPath;


    // Code Snippet from https://gist.github.com/openroomxyz/bb22a79fcae656e257d6153b867ad437
    public static Texture2D LoadPNG(string filePath)
    {
        Debug.Log("Attempting to Load PNG");

        Texture2D tex = null;
        byte[] fileData;

        if (System.IO.File.Exists(filePath))
        {
            Debug.Log("File Path Exists");

            fileData = System.IO.File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        else
        {
            Debug.Log($"File path {maskPath} does not exist");
        }
        return tex;
    }
}