using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData")]
public class GunInfo : ScriptableObject
{
    public string Name;
    public int type; //0 = Corp � corp 1 = FireGun
    public int BulletsLeft;
    public int MaxBullet;
    public int Damage;
}
