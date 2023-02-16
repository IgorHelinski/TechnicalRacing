using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Terro Gun", menuName = "Terro Gun")]
public class TerroGunBase : ScriptableObject
{
    public string gunName;
    public int availableShoots;
    public float shootBuffer;
    public float aimTime;
    public float damagePerShot;
    public float reloadTime;
}
