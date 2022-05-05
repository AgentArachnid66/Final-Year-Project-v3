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
            customEvents = GameObject.FindObjectOfType<CustomEvents>();

            return customEvents;
        }
    }



    [SerializeField]private bool controlsActive = true;
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
    [SerializeField] private float _mousePressure, _mouseAdjustPressure = 0f;

    private bool _adjustingTemp;
    private float _tempAdjustValue;
    private bool _canAdjustTemp = true;
    private bool _adjustingPress;
    private bool _canAdjustPress= true;
    [SerializeField]private bool _switchedToTemp = true;

    private bool _radialMenuOpen=false;
    private bool _radialMenuMouse=false;
    private bool _radialMenuMouseSet = false;

    public AnimationCurve mouseRotateCamera;

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
    public UnityEventVector2 NavigateMenu = new UnityEventVector2();
    public UnityEventFloat AdjustAngle = new UnityEventFloat();

    public UnityEventString ChangeScene = new UnityEventString();

    public UnityEventInt ChangeLiquid = new UnityEventInt();
    public UnityEventFloat AdjustTemp = new UnityEventFloat();
    public UnityEventFloat AdjustPress = new UnityEventFloat();
    public UnityEventInt ChangeMode = new UnityEventInt();

    public UnityEventBoolFloat ToggleRadialMenu = new UnityEventBoolFloat();
    public UnityEvent Select = new UnityEvent();

    public UnityEventFloatFloat HitWallPressureTemp = new UnityEventFloatFloat();
    public UnityEventBool SetControlsActive = new UnityEventBool();
    
    private void OnEnable()
    {
        _playerInput.Drone.MoveDrone.Enable();
        _playerInput.Drone.VerticalMoveUp.Enable();
        _playerInput.Drone.VerticalMoveDown.Enable();
        _playerInput.Drone.Shoot.Enable();
        _playerInput.Drone.RotateCamera.Enable();
        _playerInput.Drone.Liquid.Enable();
        _playerInput.Drone.Temp.Enable();
        _playerInput.Drone.OpenModes.Enable();
        _playerInput.Drone.Navigate.Enable();
        _playerInput.Drone.Switch.Enable();
        _playerInput.Drone.Select.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Drone.MoveDrone.Disable();
        _playerInput.Drone.VerticalMoveUp.Disable();
        _playerInput.Drone.VerticalMoveDown.Disable();
        _playerInput.Drone.Shoot.Disable();
        _playerInput.Drone.RotateCamera.Disable();
        _playerInput.Drone.Liquid.Disable();
        _playerInput.Drone.Temp.Disable();
        _playerInput.Drone.OpenModes.Disable();
        _playerInput.Drone.Navigate.Disable();
        _playerInput.Drone.Switch.Disable();
        _playerInput.Drone.Select.Disable();
    }

    public void SetControlsActiveFunc(bool active)
    {
        Debug.Log($"{(active ? "Controls are now active" : "Controls are now Inactive")}");
    }

    private void Awake()
    {
        _playerInput = new InputActions();
    
        _playerInput.Drone.MoveDrone.performed += ctx =>
        {
            _2dMove = ctx.ReadValue<Vector2>();
            if (!_radialMenuOpen)
            {
                DroneHorizontal.Invoke(_2dMove.x);
                DroneDiagonal.Invoke(_2dMove.y);
                LeftAnalog.Invoke(_2dMove);
            }

        };

        _playerInput.Drone.Navigate.performed += ctx =>
        {
            if (_radialMenuOpen)
            {
                Vector2 directionVector = ctx.ReadValue<Vector2>();
                float angle = (directionVector.x > 0) ? 
                    Vector3.Angle(Vector3.up, Vector3.Normalize(directionVector)) : 
                    360.0f - Vector3.Angle(Vector3.up, Vector3.Normalize(directionVector));

                AdjustAngle.Invoke(angle);
            }
        };

        _playerInput.Drone.Switch.started += ctx =>
        {
            _switchedToTemp = !_switchedToTemp;
        };
        
        _playerInput.Drone.VerticalMoveUp.performed += ctx =>
        {
            _verticalMovement = true && !_radialMenuOpen;
            _verticalMove = ctx.ReadValue<float>();
        };

        _playerInput.Drone.VerticalMoveDown.performed += ctx =>
        {
            _verticalMovement = true && !_radialMenuOpen;
            _verticalMove = ctx.ReadValue<float>() * -1f;
        };

        _playerInput.Drone.Shoot.performed += ctx =>
        {
            _shooting = true;
            _shootingValue =ctx.ReadValue<float>();
            
        };

        _playerInput.Drone.RotateCamera.performed += ctx =>
        {
            if (!_radialMenuOpen)
            {
                _2dOrientate = ctx.control == Pointer.current.delta
                    ? new Vector2(0f,0f)
                    : _2dOrientate;
                _2dOrientate = ctx.ReadValue<Vector2>().normalized;

                OrientDrone.Invoke(_2dOrientate);
            }
        };

        _playerInput.Drone.Liquid.performed += ctx =>
        {
            ChangeLiquid.Invoke((int)ctx.ReadValue<float>());
        };

        _playerInput.Drone.Temp.performed += ctx =>{
            if (_switchedToTemp)
            {
                _adjustingTemp = true;    
                _tempAdjustValue = ctx.ReadValue<float>();
            }
            else
            {
                _adjustingPress = true;
                _mouseAdjustPressure = ctx.ReadValue<float>();
            }
        };

        _playerInput.Drone.OpenModes.performed += ctx =>
        {
            _radialMenuMouse = ctx.control == Mouse.current.rightButton;
            _radialMenuOpen = true;
            ToggleRadialMenu.Invoke(true, ctx.ReadValue<float>());
        };

        _playerInput.Drone.Select.performed += ctx =>
        {
            Debug.LogError("Selected");
            Select.Invoke();
        };

        // When Player no longer inputs

        _playerInput.Drone.MoveDrone.canceled += ctx =>
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
            _radialMenuOpen = false;
            ToggleRadialMenu.Invoke(false, 0f);
        };

        _playerInput.Drone.Temp.canceled += ctx =>
        {
            _adjustingTemp = false;
            _adjustingPress = false;
        };

    }

    private void Update()
    {
        OrientDrone.Invoke(_2dOrientate);

        if (Input.GetKey(KeyCode.W))
        {
            Debug.LogError("W Key is being pressed");
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSimulation.Invoke();
        }

        if (_verticalMovement) DroneVertical.Invoke(_verticalMove);


        if (_adjustingTemp && _canAdjustTemp)
        {
            Debug.Log(_tempAdjustValue);
            AdjustTemp.Invoke(_tempAdjustValue);

            StartCoroutine(ResetTemp(0.1f));
        }


        
        

        if (_shooting && _canShoot)
        {
            Shoot.Invoke(_shootingValue);
            StartCoroutine(ResetShot(0.1f));
        }

        if (_radialMenuOpen && _radialMenuMouse)
        {
            if (!_radialMenuMouseSet)
            {
                _radialMenuMouseSet = true;
                
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            
            //CAN BE MADE A CACHED AT THE TOP
            Camera droneCamera = FindObjectOfType<Camera>();


            Vector3 viewportPoint = droneCamera.ScreenToViewportPoint(Input.mousePosition);
            Vector3 directionVector = viewportPoint - new Vector3(0.5f, 0.5f, 0.0f);

            float angle = (directionVector.x > 0) ? 
                Vector3.Angle(Vector3.up, Vector3.Normalize(directionVector)) : 
                360.0f - Vector3.Angle(Vector3.up, Vector3.Normalize(directionVector));

            AdjustAngle.Invoke(angle);
          
        }
        else if (_radialMenuMouseSet)
        {
            _radialMenuMouseSet = false;
                
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private IEnumerator ResetShot(float rate)
    {
        _canShoot = false;
        yield return new WaitForSeconds(rate);
        _canShoot = true;
    }

    private IEnumerator ResetTemp(float rate)
    {
        _canAdjustTemp = false;
        yield return new WaitForSeconds(rate);
        _canAdjustTemp = true;
    }

    private IEnumerator ResetMousePressure(float rate)
    {
        _canAdjustPress = false;
        yield return new WaitForSeconds(rate);
        _canAdjustPress = true;
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

[Serializable]
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
public class UnityEventStringText2DArray : UnityEvent<String, Texture2DArray>
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
        string fileName ="/_id_Session_" + sessionData.PlayerID.ToString()+"_" +Time.time.ToString();
        Debug.Log(Application.persistentDataPath + fileName);
        System.IO.File.WriteAllText(Application.persistentDataPath +fileName +".json", data);
    }

    public static void SaveSpatialToJSON(SpatialData spatialData)
    {
        string data = JsonUtility.ToJson(spatialData);
        string fileName ="/_id_Spatial_" + spatialData.playerID.ToString()+"_" +Time.time.ToString();
        Debug.Log(Application.persistentDataPath + fileName);
        System.IO.File.WriteAllText(Application.persistentDataPath +fileName +".json", data);
        
    }
    
    public static int CompareByAngleValue(RadialElement x, RadialElement y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (y == null)
            {
                return 1;
            }
            else
            {
                float xAngle = x.localAngle;
                float yAngle = y.localAngle;
                
                if (Mathf.Approximately(xAngle,yAngle))
                {
                    return 0;
                }
                else if (xAngle > yAngle)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}

