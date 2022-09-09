using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesEnd;
    public float bulletSpeed = 20f;
    public float damage = 1f;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(particlesEnd != null)
        {
            Instantiate(particlesEnd, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (!(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")))
        {
            Destroy(gameObject);
        }
    }


}
