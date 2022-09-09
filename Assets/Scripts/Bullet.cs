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
}
