using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInput : MonoBehaviour
{
    public DeveloperConsoleBehaviour devConsoleScript;
    public bool devConsoleAccess;
    public KeyCode devConsoleKey = KeyCode.Tab;

    public bool controllerInput;
    public bool joystickActive;

    public float horizontalInput;
    public float accelInput;
    public float breakInput;

    public bool shootInput;
    public bool scopeInput;

    public float Xon;
    public float Yon;

    public Mouse mouse;
    public Gamepad gamepad;

    private void Start()
    {
        mouse = InputSystem.GetDevice<Mouse>();
        gamepad = InputSystem.GetDevice<Gamepad>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        accelInput = Input.GetAxis("Accel");
        breakInput = Input.GetAxis("Break");

        shootInput = Input.GetButton("Fire1");
        scopeInput = Input.GetButton("Fire2");

        Xon = Input.GetAxis("Joy X");
        Yon = Input.GetAxis("Joy Y");

        if (Input.GetKeyDown(devConsoleKey) && devConsoleAccess)
        {
            devConsoleScript.Toggle();
        }

        if(gamepad != null)
        {
            if (gamepad.startButton.isPressed)
            {
                controllerInput = true;
            }
            else
            {
                if (gamepad.selectButton.isPressed)
                {
                    controllerInput = false;
                }
            }
        }
    }  
}
