using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{
    private Rigidbody rb;
    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelBackLeft;
    public bool wheelBackRight;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffnes;
    public float damperStiffnes;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springVelocity;
    private float springForce;
    private float damperForce;

    [Header("Wheel")]
    public float steerAngle;
    public float steerTime;

    private Vector3 suspensionForce;
    private Vector3 wheelVelocity; // in local space
    private float Fx;
    private float Fy;
    private float wheelAngle;

    [Header("Wheel")]
    public float wheelRadius;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.green);
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            springForce = springStiffnes * (restLength - springLength);
            damperForce = damperStiffnes * springVelocity;
            suspensionForce = (springForce + damperForce) * transform.up;
            wheelVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            Fx = Input.GetAxis("Vertical") * 0.7f * springForce;
            Fy = wheelVelocity.x * springForce;
            //Adds force
            rb.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * -transform.right), hit.point);
        }
    }
}
