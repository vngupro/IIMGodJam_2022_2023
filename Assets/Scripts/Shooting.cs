using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private PlayerGun inventory;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool CanFire = true;

    private void Start()
    {
        inventory = GetComponent<PlayerGun>();
    }

    private void Update()
    {
        if (inventory.guns[inventory.currentSlot] != null)
        {
            if(inventory.guns[inventory.currentSlot].Type == 0)
            {
                NormalGun(inventory.guns[inventory.currentSlot]);
            }
            if(inventory.guns[inventory.currentSlot].Type == 1)
            {
                MachineGun(inventory.guns[inventory.currentSlot]);
            }
        }
    }

    void Shoot(float speed,int damage)
    {
        bulletPrefab.GetComponent<Bullet>().damage = damage;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);
    }

    void NormalGun(GunInfo guninfo)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanFire && guninfo.BulletsLeft <= guninfo.MaxBullet)
        {
            Shoot(guninfo.Speed, guninfo.Damage);
            guninfo.BulletsLeft--;
            CanFire = false;
            Invoke("ShootCoolDown", guninfo.SpeedBetweenShot);
        }
    }

    void MachineGun(GunInfo guninfo)
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanFire && guninfo.BulletsLeft <= guninfo.MaxBullet)
        {
            Shoot(guninfo.Speed, guninfo.Damage);
            guninfo.BulletsLeft--;
            CanFire = false;
            Invoke("ShootCoolDown", guninfo.SpeedBetweenShot);
        }
    }

    private void ShootCoolDown()
    {
        CanFire = true;
    }


    
}
