using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("KillBullet", 0.2f);
    }

    private void KillBullet()
    {
        Destroy(gameObject);
    }
}
