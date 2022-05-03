using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody _rigidbody;
    private float _verticalForce;
    public AnimationCurve verticalCurve;

    [SerializeField]private float _horizontalForce;
    public AnimationCurve horizontalCurve;

    [SerializeField]private float _diagonalForce;
    public AnimationCurve diagonalCurve;

    private bool _shouldAddSpatialData = false;
    private Vector3 _velocity= Vector3.zero;
    public float speed;

    [Header("Rotation and Orientation")]
    private Vector3 _currentRotation;
    public Vector3 _targetRotation;
    public float banking;
    private Vector3 _rotationVelocity;
    private Vector2 _orientation;
    public float orientSpeed;
    public AnimationCurve orientCurve;

    [Header("Shooting")]
    public float minPressure = 0f;
    public float maxPressure = 1000f;
    public bool on = false;
    private float _pressure;
    public Transform waterSpout;

    [Header("Pressure and Temperature")]
    public AnimationCurve pressureCurve;
    public AnimationCurve tempCurve;

    public Liquid _currentLiquid;
    private float _currentTempLerp = 25f;
    private float _currentTemp;
    [SerializeField]private Mode _currentMode = Mode.Water;
    [SerializeField] private LayerMask _mask;

    [Header("UI")]
    public AnimationCurve timeScaleCurve;
    public UnityEventFloat UpdateTemperatureValue = new UnityEventFloat();
    public UnityEventFloat UpdateEvaluatedTemperatureValue = new UnityEventFloat();
    public UnityEventFloat UpdateTemperatureLerpValue = new UnityEventFloat();

    public UnityEventFloat UpdatePressureValue = new UnityEventFloat();
    public UnityEventFloat UpdatePressureLerpValue = new UnityEventFloat();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

 
    
    private void Start()
    {
        CustomEvents.CustomEventsInstance.DroneVertical.AddListener(VerticalMovement);
        CustomEvents.CustomEventsInstance.ResetDroneVertical.AddListener(ResetVertical);
        CustomEvents.CustomEventsInstance.ResetDroneVertical.AddListener(ResetRotation);


        CustomEvents.CustomEventsInstance.DroneHorizontal.AddListener(HorizontalMovement);
        CustomEvents.CustomEventsInstance.ResetDroneHorizontal.AddListener(ResetHorizontal);
        CustomEvents.CustomEventsInstance.ResetDroneHorizontal.AddListener(ResetRotation);


        CustomEvents.CustomEventsInstance.DroneDiagonal.AddListener(DiagonalMovement);
        CustomEvents.CustomEventsInstance.ResetDroneDiagonal.AddListener(ResetDiagonal);
        CustomEvents.CustomEventsInstance.ResetDroneDiagonal.AddListener(ResetRotation);

        CustomEvents.CustomEventsInstance.Shoot.AddListener(ShootWater);
        CustomEvents.CustomEventsInstance.OrientDrone.AddListener(ChangeOrientation);

        CustomEvents.CustomEventsInstance.ChangeLiquid.AddListener(ChangeLiquid);
        CustomEvents.CustomEventsInstance.AdjustTemp.AddListener(AdjustTemp);
        CustomEvents.CustomEventsInstance.ChangeMode.AddListener(ChangeMode);

        CustomEvents.CustomEventsInstance.ToggleRadialMenu.AddListener(AdjustTimeScale);



        _currentTemp = tempCurve.Evaluate(_currentTempLerp);
    }

    private void FixedUpdate()
    {
        // Movement
        _rigidbody.MovePosition(Vector3.SmoothDamp(transform.position, transform.position +
            (transform.up * _verticalForce) +
            (transform.right * _horizontalForce) +
            (transform.forward * _diagonalForce),
            ref _velocity, speed * Time.deltaTime));


        if(_shouldAddSpatialData) DataController.sharedInstance.sessionData.spatialData.Add(new SpatialData(_rigidbody.position, _rigidbody.velocity, System.DateTime.Now.ToString("yyyyMMddHHmmss")));

        _currentRotation.x = Mathf.SmoothDamp(_currentRotation.x, _targetRotation.x + (-1f * _orientation.y), ref _rotationVelocity.x, banking);
        _currentRotation.y = Mathf.SmoothDamp(_currentRotation.y, _targetRotation.y + _orientation.x, ref _rotationVelocity.y, banking);
        _currentRotation.z = Mathf.SmoothDamp(_currentRotation.z, _targetRotation.z, ref _rotationVelocity.z, banking);

        _rigidbody.MoveRotation(Quaternion.Euler(_currentRotation));

    }

    #region Movement

    /// <summary>
    /// This smoothly transitions from controller analog stick to movement in a given axis. [Deprecated]
    /// </summary>
    /// <param name="force"></param>
    /// <param name="currentLerpValue"></param>
    /// <param name="input"></param>
    [Obsolete("Move using float lerp value is deprecated, please use Move with the Animation Curve instead.")]
    private void Move(ref float force, ref float currentLerpValue, float input)
    {
        currentLerpValue += Time.deltaTime;
        currentLerpValue = Mathf.Clamp01(currentLerpValue);
        force = Mathf.SmoothStep(force, input, currentLerpValue);
    }

    
    /// <summary>
    /// Uses a curve that is evaluated by the controller analog input to give smooth translation
    /// </summary>
    /// <param name="force"></param>
    /// <param name="curve"></param>
    /// <param name="input"></param>
    private void Move(ref float force, AnimationCurve curve, float input)
    {
        force = curve.Evaluate(input);
        _shouldAddSpatialData = true;
    }

    private void VerticalMovement(float input)
    {
        Move(ref _verticalForce, verticalCurve, input);
        AddRotation(input, ref _targetRotation.x);
    }

    private void HorizontalMovement(float input)
    {
        Move(ref _horizontalForce, horizontalCurve, input);
        AddRotation(input, ref _targetRotation.z);
        
    }

    private void DiagonalMovement(float input)
    {
        Move(ref _diagonalForce, diagonalCurve, input);
        AddRotation(input, ref _targetRotation.y);
    }
    #endregion

    #region Reset Forces
    private void ResetVertical()
    {
        _verticalForce = 0;
        _shouldAddSpatialData = false;
    }   
    private void ResetHorizontal()
    {   
        _horizontalForce = 0;
        _shouldAddSpatialData = false;
    }   
    private void ResetDiagonal()
    {   
        _diagonalForce = 0;
        _shouldAddSpatialData = false;
    }
    #endregion

    #region Rotation

    private void AddRotation(float input, ref float axis)
    {
        axis = input;
    }

    void ResetRotation()
    {
        //_currentRotation = Vector3.zero;
        _targetRotation = Vector3.zero;
    }


    void ChangeOrientation(Vector2 newOrient)
    {
        _orientation.x += orientCurve.Evaluate(newOrient.x) * orientSpeed;
        _orientation.y += orientCurve.Evaluate(newOrient.y) * orientSpeed;
    }
    #endregion

    #region Shooting

    [ContextMenu("Test")]
    public void Test()
    {
        CustomEvents.CustomEventsInstance.HitWallPressureTemp.Invoke(0.5f, 0.5f);
    }
    private void ShootWater(float input)
    {

        _pressure = Mathf.Lerp(minPressure, maxPressure, input);
        UpdatePressureValue.Invoke(Mathf.Round(100*_pressure/maxPressure));
        Debug.Log($"Receivied: {_currentTemp}");
        GameObject globule = GlobuleObjectPool.sharedInstance.GetPooledObject();
        if (globule != null)
        {
            WaterGlobule waterGlobule = globule.GetComponent<WaterGlobule>();
            globule.transform.position = waterSpout.transform.position;
            waterGlobule.launchVelocity = waterSpout.forward;
            globule.GetComponent<Rigidbody>().AddForce(waterSpout.forward * _pressure);

            waterGlobule.pressure = _pressure;
            waterGlobule.SetVars(pressureCurve.Evaluate(input), _currentTemp);

            Debug.Log("Shooting Globule");

            StartCoroutine(waterGlobule.ResetGlobule(2f));

            AdjustTemp(.25f);
        }
    }

    private void ShootLaser(float input)
    {
        // Shoot raycast in current forward vector, using the orientation input as directional input
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 1000f, _mask))
        {
            Debug.Log($"Hit with damage output of: {input}");

        }
        // Damage the laser can afflict is proportional to input

        // Player must be careful not to hit too much of the underlying tissue
    }

    private void ChangeLiquid(int deltaL)
    {
        string[] liquids = System.Enum.GetNames(typeof(Liquid));

        if ((int)(deltaL + _currentLiquid) >= liquids.Length)
        {
            Debug.Log("Need To Wrap Around");
            _currentLiquid = 0;
            Debug.Log(_currentLiquid);
        }
        else if ((int)(deltaL + _currentLiquid) < 0)
        {
            Debug.Log("Need to Wrap to End");
            _currentLiquid = (Liquid)liquids.Length-1;
            Debug.Log(_currentLiquid);
        }
        else
        {
            _currentLiquid += deltaL;
            Debug.Log(_currentLiquid);
        }
    }

    private void ChangeMode(int deltaM)
    {
        _currentMode = (Mode)deltaM;
    }


    private void AdjustTemp(float deltaT)
    {
        Debug.LogWarning($"Delta Temperature is: {deltaT} with a current lerp {_currentTempLerp + deltaT}");
        _currentTempLerp += deltaT;
        _currentTempLerp = Mathf.Clamp(_currentTempLerp, 0f, 100f);

        _currentTemp = tempCurve.Evaluate(_currentTempLerp/100f);
        Debug.Log(_currentTemp);
        UpdateTemperatureValue.Invoke(Mathf.Round(Mathf.Lerp(-5, 50, _currentTempLerp / 100f)));
        UpdateEvaluatedTemperatureValue.Invoke(_currentTempLerp/100f);
    }


    private void AdjustTimeScale(bool toggle, float input)
    {
        Time.timeScale = timeScaleCurve.Evaluate(input);
    }
    
    
    #endregion




}
