using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
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

    public float motorTorque;
    public float brakeTorque;
    public float decelerationForce;
    public float maxSteerAngle;
    public float downForceCoefficient;
    public Rigidbody body;
    public Vector3 respawnPosition;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        // Accleration();
        // Steering();
        // Break();
        AddDownForce();
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

    // Car action
    public void Accleration(float verticalInput)
    {
        // float verticalInput = Input.GetAxis("Vertical");
        // verticalInput = -verticalInput;
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
        // Debug.Log(verticalInput);

    }

    public void Break()
    {
        if(Input.GetKey(KeyCode.Space)){
            wheelColliderLeftRear.brakeTorque = brakeTorque;
            wheelColliderRightRear.brakeTorque = brakeTorque;
            wheelColliderLeftFront.brakeTorque = brakeTorque;
            wheelColliderRightFront.brakeTorque = brakeTorque;
        }
        // Debug.Log(wheelColliderRightRear.brakeTorque);
    }

    public void Steering(float horizontalInput)
    {
        wheelColliderLeftFront.steerAngle = horizontalInput * maxSteerAngle;
        wheelColliderRightFront.steerAngle = horizontalInput * maxSteerAngle;
    }

    private void AddDownForce()
    {
        float force = downForceCoefficient * body.velocity.sqrMagnitude;
        body.AddForce(-force * transform.up);
    }

    public void Respawn()
    {
        body.MovePosition(respawnPosition);
        transform.position = respawnPosition;
        transform.rotation = transform.rotation = Quaternion.identity;
    }

    public float LocalSpeed()
    {
        float dot = Vector3.Dot(transform.forward, body.velocity);
        float speed = body.velocity.magnitude;
        return dot < 0 ? -speed : speed;
    }

}
