using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarWheel : MonoBehaviour
{
    public MyInput inputMenager;
    public Transform carTransform;
    public Rigidbody carRigidbody;
    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelBackLeft;
    public bool wheelBackRight;

    [Header("Acceleration")]
    public float accelInput;
    public float carAcceTime;
    public float carTopSpeed;
    public float MyCarSpeed;

    [Header("Breaking")]
    public float breakInput;

    [Header("Suspencion")]
    public float wheelRadius;
    public float suspensionRestDist;
    public float springStrength;
    public float springDamper;

    [Header("Steering Force")]
    public float tireGrip; // how well tires grip - if lower car slides(drifts)
    public float tireMass;

    [HideInInspector]
    public float steerAngle;
    public float steerTime;
    private float wheelAngle;

    private void Update()
    {
        accelInput = inputMenager.accelInput;
        breakInput = inputMenager.breakInput;

        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        MyCarSpeed = Vector3.Dot(carTransform.forward, carRigidbody.velocity);

        Debug.DrawRay(transform.position, -transform.up * (suspensionRestDist), Color.green);

        transform.GetChild(0).gameObject.transform.Rotate(carRigidbody.velocity.magnitude * 100 * Time.deltaTime, 0, 0, Space.Self);
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, wheelRadius))
        {
            //Suspencion
            Vector3 springDir = transform.up;
            Vector3 tireWorldVel = carRigidbody.GetPointVelocity(transform.position);
            float offset = suspensionRestDist - hit.distance;
            float vel = Vector3.Dot(springDir, tireWorldVel);
            float force = (offset * springStrength) - (vel * springDamper);
            carRigidbody.AddForceAtPosition(springDir * force, transform.position);
            Debug.DrawRay(transform.position, springDir * (force / 20), Color.green);

            //Steering force
            Vector3 steeringDir = transform.right;
            float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
            float desiredVelChange = -steeringVel * tireGrip;
            float desiredAccel = desiredVelChange / Time.fixedDeltaTime;
            carRigidbody.AddForceAtPosition(steeringDir * tireMass * desiredAccel, transform.position);
            Debug.DrawRay(transform.position, (steeringDir * tireMass * desiredAccel) / 20, Color.red);

            //Acceleration
            Vector3 accelDir = transform.forward;
            if(accelInput > 0 && MyCarSpeed <= carTopSpeed)
            {
                float carSpeed = Vector3.Dot(carTransform.forward, carRigidbody.velocity);
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
                //float availableTorque = powerCurve.Evaluate(normalizedSpeed) * accelInput;
                float accel = carAcceTime * accelInput;
                carRigidbody.AddForceAtPosition(accelDir * accel, carRigidbody.transform.position);
            }

            //Decceleration ? goin back
            if(breakInput > 0 && (wheelBackLeft || wheelBackRight))
            {
                //Debug.Log("Breaking halo");
                float carSpeed = Vector3.Dot(carTransform.forward, carRigidbody.velocity);
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
                //float availableTorque = powerCurve.Evaluate(normalizedSpeed) * accelInput;
                float accel = carAcceTime * (breakInput * -1);
                carRigidbody.AddForceAtPosition(accelDir * accel, carRigidbody.transform.position);
            }

            //Keeping car stopped
            if(accelInput == 0 && breakInput == 0)
            {

            }
        }
    }
}
