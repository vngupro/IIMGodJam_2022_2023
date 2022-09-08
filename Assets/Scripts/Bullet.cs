using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesEnd;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(particlesEnd != null)
        {
            Instantiate(particlesEnd, transform.position, Quaternion.Euler(0, 0, 0));
        }
        Destroy(gameObject);
    }
}
