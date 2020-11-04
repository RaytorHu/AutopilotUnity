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

    public float motorTorque = 100f;
    public float maxSteerAngle = 20f;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        // Debug.Log(Input.GetAxis("Vertical"));
        wheelColliderLeftRear.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderRightRear.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
        wheelColliderRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
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
}
