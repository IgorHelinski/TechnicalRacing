using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristIdleState : TerroristBaseState
{
    public override void EnterState(TerroristAImenager terroMenager)
    {
        terroMenager.stateText.text = "Idle State";
    }

    public override void UpdateState(TerroristAImenager terroMenager)
    {
        terroMenager.transform.rotation = Quaternion.Slerp(terroMenager.transform.rotation, terroMenager.startRot, 5f * Time.deltaTime);

        //if terro spots the player switch to aim state
        if (terroMenager.fovScript.canSeePlayer == true)
        {
            terroMenager.SwitchState(terroMenager.AimState);
        }
    }
}
