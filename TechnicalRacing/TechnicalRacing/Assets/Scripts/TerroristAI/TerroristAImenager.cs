using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerroristAImenager : MonoBehaviour
{
    public Animator animator;

    //Gun Stats
    public TerroGunBase gunStats;

    //Field of view 
    public TerroristFov fovScript;

    //State Machine
    public TextMeshPro stateText;
    public TerroristBaseState currentState;
    [HideInInspector]
    public TerroristIdleState IdleState = new TerroristIdleState();
    [HideInInspector]
    public TerroristAimState AimState = new TerroristAimState();
    [HideInInspector]
    public TerroristReloadState ReloadState = new TerroristReloadState();

    //Mis
    [HideInInspector]
    public Quaternion startRot;

    public void SwitchState(TerroristBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void Start()
    {
        animator.SetTrigger("TakeOut");
        startRot = transform.rotation;
        SwitchState(IdleState);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void GunShoot()
    {
        Debug.Log("Shooooot !!!");
    }
}
