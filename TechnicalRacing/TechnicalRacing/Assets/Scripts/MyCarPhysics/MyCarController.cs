using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarController : MonoBehaviour
{
    public MyCarWheel[] wheels;
    public MyInput inputMenager;
    private Rigidbody carRb;
    //EngineSound
    public AudioSource engineSound;
    private float carSpeed;
    public int gearShiftLength;
    public float PitchBoost;
    public float PitchRange;
    float Temp1;
    int Temp2;

    public float driftValue;
    public float driftAngle;

    [Header("Turning")]
    public float wheelBase;
    public float rearTrack;
    public float turnRadius;

    [Header("Inputs")]
    public float steerInput;

    private float ackermannAngleLeft;
    private float ackermannAngleRight;

    private void Start()
    {
        carRb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        steerInput = inputMenager.horizontalInput;

        driftValue = Vector3.Dot(carRb.velocity.normalized, transform.forward);
        driftAngle = Mathf.Acos(driftValue) * Mathf.Rad2Deg;

        carSpeed = carRb.velocity.magnitude;
        Temp1 = carSpeed / gearShiftLength;
        Temp2 = (int)Temp1;
        float diffrence = Temp1 - Temp2;
        engineSound.pitch = Mathf.Lerp(engineSound.pitch, (PitchRange * diffrence) + PitchBoost, 0.1f);
        

        if (steerInput > 0) // is turning right
        {
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0) // is turning left
        {
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else // is 0
        {
            ackermannAngleLeft = 0;
            ackermannAngleRight = 0;
        }

        foreach (MyCarWheel w in wheels)
        {
            if (w.wheelFrontLeft)
            {
                w.steerAngle = ackermannAngleLeft;
            }
            if (w.wheelFrontRight)
            {
                w.steerAngle = ackermannAngleRight;
            }
        }
    }
}
