using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void Awake()
    {
        Invoke("KillBullet", 5f);
    }
    private void KillBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if touching something other than player or enemy
        if( !( collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") ))
        {
            KillBullet();
        }

    }
}
