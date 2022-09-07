using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData")]
public class GunInfo : ScriptableObject
{
    public string Name;
    public int Type; //0 = FireGun 1 = MachineGun
    public int BulletsLeft;
    public int MaxBullet;
    public int Damage;
    public int Speed;
    public float SpeedBetweenShot;
    

}
