using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristAimState : TerroristBaseState
{
    //Makes sure to start and stop the same instance of this ienumerator
    IEnumerator aimIEnumerator;
    private Camera myCamera;

    public override void EnterState(TerroristAImenager terroMenager)
    {
        myCamera = Camera.main;
        terroMenager.stateText.text = "Aim State";
        aimIEnumerator = aimTime(terroMenager);
        terroMenager.StartCoroutine(aimIEnumerator);
    }

    public override void UpdateState(TerroristAImenager terroMenager)
    {
        terroMenager.animator.SetBool("Aim", true);

        terroMenager.transform.LookAt(myCamera.transform);
        terroMenager.transform.rotation = Quaternion.Euler(0f, terroMenager.transform.rotation.eulerAngles.y, 0f);

        if (terroMenager.fovScript.canSeePlayer == false)
        {
            terroMenager.StopCoroutine(aimIEnumerator);
            terroMenager.animator.SetBool("Aim", false);
            terroMenager.SwitchState(terroMenager.IdleState);
        }
    }

    IEnumerator aimTime(TerroristAImenager terroMenager)
    {
        while (true)
        {
            yield return new WaitForSeconds(terroMenager.gunStats.aimTime);
            for (int i = 0; i < terroMenager.gunStats.availableShoots; i++)
            {
                terroMenager.GunShoot();
                yield return new WaitForSeconds(terroMenager.gunStats.shootBuffer);
            }
            terroMenager.StopCoroutine(aimIEnumerator);
            terroMenager.animator.SetBool("Aim", false);
            terroMenager.SwitchState(terroMenager.ReloadState);
        }
    }
}
