using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInfo : MonoBehaviour
{
    public string Name;
    public int BulletsLeft;
    public int MaxBullet;
    public int Damage;
    public int Speed;
    public float SpeedBetweenShot;

    private void Start()
    {
        BulletsLeft = MaxBullet;
    }

    public bool CanShoot()
    {
        if(BulletsLeft == 0)
        {
            return false;
        }
        else
        {
            BulletsLeft--;
            return true;
        }
    }
}
