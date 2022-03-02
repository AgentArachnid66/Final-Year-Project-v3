using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _verticalForce;
    private float _currentLerpVertical;

    private float _horizontalForce;
    private float _currentLerpHorizontal;

    private float _diagonalForce;
    private float _currentLerpDiagonal;

    private float _yawDelta;

    private Vector3 _velocity= Vector3.zero;
    public float speed;

    private Vector3 _currentRotation;
    public Vector3 _targetRotation;
    public float banking;
    private Vector3 _rotationVelocity;
    private Vector2 _orientation;
    public float orientSpeed;

    public float minPressure = 0f;
    public float maxPressure = 1000f;
    public bool on = false;
    private float _pressure;
    public Transform waterSpout;


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
            
    }

    private void FixedUpdate()
    {
        // Movement
        _rigidbody.MovePosition(Vector3.SmoothDamp(transform.position, transform.position + 
            (transform.up * _verticalForce) + 
            (transform.right * _horizontalForce) +
            (transform.forward * _diagonalForce), 
            ref _velocity, speed*Time.deltaTime));


        _currentRotation.x = Mathf.SmoothDamp(_currentRotation.x, _targetRotation.x + (-1f* _orientation.y), ref _rotationVelocity.x, banking);
        _currentRotation.y = Mathf.SmoothDamp(_currentRotation.y, _targetRotation.y + _orientation.x, ref _rotationVelocity.y, banking);
        _currentRotation.z = Mathf.SmoothDamp(_currentRotation.z, _targetRotation.z, ref _rotationVelocity.z, banking);

        _rigidbody.MoveRotation(Quaternion.Euler(_currentRotation));
    }

    #region Movement

    private void Move(ref float force, ref float currentLerpValue, float input)
    {
        currentLerpValue += Time.deltaTime;
        currentLerpValue = Mathf.Clamp01(currentLerpValue);
        force = Mathf.SmoothStep(force, input, currentLerpValue);
    }


    private void VerticalMovement(float input)
    {
        Move(ref _verticalForce, ref _currentLerpVertical, input);
        AddRotation(input, ref _targetRotation.x);
    }

    private void HorizontalMovement(float input)
    {
        Move(ref _horizontalForce, ref _currentLerpHorizontal, input);
        AddRotation(input, ref _targetRotation.z);
        
    }

    private void DiagonalMovement(float input)
    {
        Move(ref _diagonalForce, ref _currentLerpDiagonal, input);
        AddRotation(input, ref _targetRotation.y);
    }
    #endregion

    #region Reset Forces
    private void ResetVertical()
    {
        _currentLerpVertical = 0;
        _verticalForce = 0;
    }   
    private void ResetHorizontal()
    {   
        _currentLerpHorizontal = 0;
        _horizontalForce = 0;
    }   
    private void ResetDiagonal()
    {   
        _currentLerpDiagonal = 0;
        _diagonalForce = 0;
    }
    #endregion

    #region Rotation

    private void AddRotation(float input, ref float axis)
    {
        axis = input;
    }

    void ResetRotation()
    {
        _currentRotation = Vector3.zero;
        _targetRotation = Vector3.zero;
    }


    void ChangeOrientation(Vector2 newOrient)
    {
        _orientation += newOrient * orientSpeed;
        _orientation = Mathf.Clamp(_orientation, -360f, 360);
    }
    #endregion


    #region Shooting

    private void ShootWater(float input)
    {
        _pressure = Mathf.Lerp(minPressure, maxPressure, input);
        Debug.Log("Receivied");
        GameObject globule = GlobuleObjectPool.sharedInstance.GetPooledObject();
        if (globule != null)
        {
            WaterGlobule waterGlobule = globule.GetComponent<WaterGlobule>();
            globule.transform.position = waterSpout.transform.position;
            waterGlobule.launchVelocity = waterSpout.forward;
            globule.GetComponent<Rigidbody>().AddForce(waterSpout.forward * _pressure);

            waterGlobule.pressure = _pressure;

            Debug.Log("Shooting Globule");

            StartCoroutine(waterGlobule.ResetGlobule(2f));
        }
    }

    #endregion

}
