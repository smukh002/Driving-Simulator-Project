using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool breaksBool;
    [SerializeField] private float gaspedal;
    [SerializeField] private float breakpedal;
    [SerializeField] private float maxSteerAngle;
    //front left
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private Transform frontLeftWheelTransform;
    //front right
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private Transform frontRightWheeTransform;
    //rear left
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private Transform rearLeftWheelTransform;
    // rear right
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate(){
        GetInput();
        Motor();
        Steering();
        Wheels();
    }

    private void GetInput(){
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        breaksBool = Input.GetKey(KeyCode.Space);
    }

    private void Motor(){
        frontLeftWheelCollider.motorTorque = verticalInput * gaspedal;
        frontRightWheelCollider.motorTorque = verticalInput * gaspedal;
        currentbreakForce = breaksBool ? breakpedal : 0f;
        if (breaksBool){
            Breaks();
        }
    }

    private void Breaks(){
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void Steering(){
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void Wheels(){
        UpdateWheelPosition(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPosition(frontRightWheelCollider, frontRightWheeTransform);
        UpdateWheelPosition(rearRightWheelCollider, rearRightWheelTransform);
        UpdateWheelPosition(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateWheelPosition(WheelCollider wheelCollider, Transform wheelTransform){
        Vector3 pos;
        Quaternion rot;       
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
