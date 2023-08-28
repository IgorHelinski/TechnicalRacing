using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInput : MonoBehaviour
{
    public DeveloperConsoleBehaviour devConsoleScript;
    public bool devConsoleAccess;
    public KeyCode devConsoleKey = KeyCode.Tab;

    public bool controllerInput;
    public bool joystickActive;

    public float horizontalInput;
    public float accelInput;
    public float breakInput;

    public bool driftInput;

    public bool shootInput;
    public bool scopeInput;

    public float Xon;
    public float Yon;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        accelInput = Input.GetAxis("Accel");
        breakInput = Input.GetAxis("Break");

        driftInput = Input.GetButton("Jump");

        shootInput = Input.GetButton("Fire1");
        scopeInput = Input.GetButton("Fire2");

        Xon = Input.GetAxis("Joy X");
        Yon = Input.GetAxis("Joy Y");

        if (Input.GetKeyDown(devConsoleKey) && devConsoleAccess)
        {
            devConsoleScript.Toggle();
        }
    }
}
