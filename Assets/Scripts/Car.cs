using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;

    public WheelCollider wheelColliderLeftFront;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderLeftRear;
    public WheelCollider wheelColliderRightRear;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftRear;
    public Transform wheelRightRear;

    public float motorTorque = 300f;
    public float brakeTorque = 450f;
    public float decelerationForce = 50f;
    public float maxSteerAngle = 20f;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        Accleration(Input.GetAxis("Vertical"));
        Break();
        FrontWheelRotate();
    }

    void Update()
    {
        var position = Vector3.zero;
        var rotation = Quaternion.identity;

        wheelColliderLeftFront.GetWorldPose(out position, out rotation);
        wheelLeftFront.position = position;
        wheelLeftFront.rotation = rotation * Quaternion.Euler(0, 180, 0);

        wheelColliderRightFront.GetWorldPose(out position, out rotation);
        wheelRightFront.position = position;
        wheelRightFront.rotation = rotation;

        wheelColliderLeftRear.GetWorldPose(out position, out rotation);
        wheelLeftRear.position = position;
        wheelLeftRear.rotation = rotation  * Quaternion.Euler(0, 180, 0);

        wheelColliderRightRear.GetWorldPose(out position, out rotation);
        wheelRightRear.position = position;
        wheelRightRear.rotation = rotation;
    }

    private void Accleration(float verticalInput){
        if(verticalInput != 0f){
            wheelColliderLeftRear.motorTorque = verticalInput * motorTorque;
            wheelColliderRightRear.motorTorque = verticalInput * motorTorque;
            wheelColliderLeftRear.brakeTorque = 0;
            wheelColliderRightRear.brakeTorque = 0;
            wheelColliderLeftFront.brakeTorque = 0;
            wheelColliderRightFront.brakeTorque = 0;
        }
        else{
            wheelColliderLeftRear.brakeTorque = decelerationForce;
            wheelColliderRightRear.brakeTorque = decelerationForce;
            wheelColliderLeftFront.brakeTorque = decelerationForce;
            wheelColliderRightFront.brakeTorque = decelerationForce;
        }
        // Debug.Log(wheelColliderRightRear.brakeTorque);


        // if(verticalInput > 0f){
        //     wheelColliderLeftRear.motorTorque = verticalInput * motorTorque;
        //     wheelColliderRightRear.motorTorque = verticalInput * motorTorque;
        // }
        // else if(verticalInput < 0f){
        //     wheelColliderLeftRear.motorTorque = verticalInput * brakeTorque;
        //     wheelColliderRightRear.motorTorque = verticalInput * brakeTorque;           
        // }
        // else{
        //     wheelColliderLeftRear.motorTorque = 0;
        //     wheelColliderRightRear.motorTorque = 0;
        //     // wheelColliderLeftRear.brakeTorque = - verticalInput * brakeTorque;
        //     // wheelColliderRightRear.brakeTorque = - verticalInput * brakeTorque;
        //     // wheelColliderLeftFront.brakeTorque = - verticalInput * brakeTorque;
        //     // wheelColliderRightFront.brakeTorque = - verticalInput * brakeTorque;
        //     // Debug.Log(wheelColliderLeftRear.brakeTorque);
        // }

    }

    private void Break(){
        if(Input.GetKey(KeyCode.Space)){
            wheelColliderLeftRear.brakeTorque = brakeTorque;
            wheelColliderRightRear.brakeTorque = brakeTorque;
            wheelColliderLeftFront.brakeTorque = brakeTorque;
            wheelColliderRightFront.brakeTorque = brakeTorque;
        }
        // Debug.Log(wheelColliderRightRear.brakeTorque);
    }

    private void FrontWheelRotate(){
        wheelColliderLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
        wheelColliderRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
    }
}
