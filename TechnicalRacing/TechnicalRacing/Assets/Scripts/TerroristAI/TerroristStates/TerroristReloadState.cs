using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristReloadState : TerroristBaseState
{
    //Makes sure to start and stop the same instance of this ienumerator
    IEnumerator reloadIEnumerator;

    public override void EnterState(TerroristAImenager terroMenager)
    {
        terroMenager.stateText.text = "Reload State";

        reloadIEnumerator = reloadTime(terroMenager);
        terroMenager.StartCoroutine(reloadIEnumerator);
    }

    public override void UpdateState(TerroristAImenager terroMenager)
    {
        
    }

    IEnumerator reloadTime(TerroristAImenager terroMenager)
    {
        yield return new WaitForSeconds(terroMenager.gunStats.reloadTime);
        Debug.Log("Reloading Done !!!");
        terroMenager.SwitchState(terroMenager.IdleState);
    }
}
