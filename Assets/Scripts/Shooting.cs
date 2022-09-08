using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool CanFire = true;

    public string son = "";

    public static Shooting instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Shooting");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.T) && CanFire)
        {
            InventoryManager iv = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            if(iv.guns[iv.currentSlot] != null)
            {
                GunInfo gunInfo = iv.guns[iv.currentSlot].GetComponent<GunInfo>();
                if (gunInfo.CanShoot())
                {
                    CanFire = false;
                    Shoot(gunInfo.Speed, gunInfo.Damage);
                    StartCoroutine(FireSpeed(gunInfo.SpeedBetweenShot));
                }
            }
        }
    }


    void Shoot(float speed,int damage)
    {
        bulletPrefab.GetComponent<Bullet>().damage = damage;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);

        SoundManager.Instance.PlaySound(son);
    }

    IEnumerator FireSpeed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CanFire = true;
    }

}
