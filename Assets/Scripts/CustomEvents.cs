using System;
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
    [SerializeField]private Vector2 _2dMove;
    [SerializeField]private Vector2 _2dOrientate;
   
    private float _verticalMove;
    // Bool to determine if the player is moving up/down this frame 
    private bool _verticalMovement;

    private bool _shooting;
    private float _shootingValue;
    private bool _canShoot = true;

    public UnityEvent StartSimulation = new UnityEvent();

    public UnityEventFloat Shoot = new UnityEventFloat();

    public UnityEventFloat DroneHorizontal = new UnityEventFloat();
    public UnityEventFloat DroneVertical = new UnityEventFloat();
    public UnityEventFloat DroneDiagonal = new UnityEventFloat();

    public UnityEvent ResetDroneVertical = new UnityEvent();
    public UnityEvent ResetDroneHorizontal = new UnityEvent();
    public UnityEvent ResetDroneDiagonal= new UnityEvent();

    public UnityEventVector2 OrientDrone = new UnityEventVector2();
    public UnityEventVector2 LeftAnalog = new UnityEventVector2();

    public UnityEventString ChangeScene = new UnityEventString();

    public UnityEventInt ChangeLiquid = new UnityEventInt();
    public UnityEventFloat AdjustTemp = new UnityEventFloat();
    public UnityEventInt ChangeMode = new UnityEventInt();

    public UnityEventBoolFloat ToggleRadialMenu = new UnityEventBoolFloat();
    public UnityEvent Select = new UnityEvent();

    public UnityEventFloatFloat HitWallPressureTemp = new UnityEventFloatFloat();

    private void OnEnable()
    {
        _playerInput.Drone.Move.Enable();
        _playerInput.Drone.VerticalMoveUp.Enable();
        _playerInput.Drone.VerticalMoveDown.Enable();
        _playerInput.Drone.Shoot.Enable();
        _playerInput.Drone.RotateCamera.Enable();
        _playerInput.Drone.Liquid.Enable();
        _playerInput.Drone.Temp.Enable();


        _playerInput.Drone.OpenModes.Enable();
        _playerInput.Drone.Select.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Drone.Move.Disable();
        _playerInput.Drone.VerticalMoveUp.Disable();
        _playerInput.Drone.VerticalMoveDown.Disable();
        _playerInput.Drone.Shoot.Disable();
        _playerInput.Drone.RotateCamera.Disable();
        _playerInput.Drone.Liquid.Disable();
        _playerInput.Drone.Temp.Disable();
        _playerInput.Drone.OpenModes.Disable();
       _playerInput.Drone.Select.Disable();

    }


    private void Awake()
    {
        _playerInput = new InputActions();

        // Player Inputs

        _playerInput.Drone.Move.performed += ctx => {
            _2dMove = ctx.ReadValue<Vector2>();
            DroneHorizontal.Invoke(_2dMove.x);
            DroneDiagonal.Invoke(_2dMove.y);
            LeftAnalog.Invoke(_2dMove);
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
            _shooting = true;
            _shootingValue =ctx.ReadValue<float>();
        };

        _playerInput.Drone.RotateCamera.performed += ctx =>
        {
            _2dOrientate = ctx.ReadValue<Vector2>();
            OrientDrone.Invoke(_2dOrientate);
        };

        _playerInput.Drone.Liquid.performed += ctx =>
        {
            ChangeLiquid.Invoke((int)ctx.ReadValue<float>());
        };

        _playerInput.Drone.Temp.performed += ctx =>{
            AdjustTemp.Invoke(ctx.ReadValue<float>());
        };

        _playerInput.Drone.OpenModes.performed += ctx =>
        {
            ToggleRadialMenu.Invoke(true, ctx.ReadValue<float>());
        };

        _playerInput.Drone.Select.performed += ctx =>
        {
            Select.Invoke();
        };

        // When Player no longer inputs

        _playerInput.Drone.Move.canceled += ctx =>
        {
            _2dMove = Vector2.zero;
            ResetDroneHorizontal.Invoke();
            ResetDroneDiagonal.Invoke();
            LeftAnalog.Invoke(Vector2.zero);
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

        _playerInput.Drone.Shoot.canceled += ctx =>
        {
            _shooting = false;
            _shootingValue = 0f;
        };

        _playerInput.Drone.RotateCamera.canceled += ctx =>
        {
            _2dOrientate = Vector2.zero;
        };

        _playerInput.Drone.OpenModes.canceled += ctx =>
        {
            ToggleRadialMenu.Invoke(false, 0f);
        };

    }

    private void Update()
    {
        OrientDrone.Invoke(_2dOrientate);


        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSimulation.Invoke();
        }

        if (_verticalMovement) DroneVertical.Invoke(_verticalMove);

        if (_shooting && _canShoot)
        {

            Shoot.Invoke(_shootingValue);
            StartCoroutine(ResetShot(0.1f));
        }
    }

    private IEnumerator ResetShot(float rate)
    {
        _canShoot = false;
        yield return new WaitForSeconds(rate);
        _canShoot = true;
    }

    public void UpdateMode(int index)
    {
        Debug.Log(index);
        ChangeMode.Invoke(index);
    }
}

[Serializable]
public class UnityEventFloat : UnityEvent<float>
{
}

public class UnityEventString : UnityEvent<string>
{
}

public class UnityEventVector2 : UnityEvent<Vector2>
{
}

[Serializable]
public class UnityEventInt : UnityEvent<int>{

}

[Serializable]
public class UnityEventBool : UnityEvent<bool>
{

}

[Serializable]
public class UnityEventColour : UnityEvent<Color>
{

}

[Serializable]
public class UnityEventAudio : UnityEvent<AudioClip>
{

}

[Serializable]
public class UnityEventBoolFloat : UnityEvent<bool, float>
{

}

[Serializable]
public class UnityEventBoolString : UnityEvent<bool, string>
{

}

[Serializable]
public class UnityEventFloatFloat: UnityEvent<float, float>
{

}

public enum Liquid
{
    None,
    Water
}

public enum Mode
{
    Water,
    Laser
}

public struct RenderSettings
{
    RenderTexture texture;
    Color drawColour;
    float drawSize;

    public RenderSettings(RenderTexture render, Color color, float draw)
    {
        this.texture = render;
        this.drawColour = color;
        this.drawSize = draw;
    }
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

    public static void SaveSessionToJSON(SessionData sessionData)
    {
        string data = JsonUtility.ToJson(sessionData);
        string fileName ="/_id" + sessionData.PlayerID.ToString()+"_" +Time.time.ToString();
        Debug.Log(Application.persistentDataPath + fileName);
        System.IO.File.WriteAllText(Application.persistentDataPath +fileName +".json", data);
    }

}

