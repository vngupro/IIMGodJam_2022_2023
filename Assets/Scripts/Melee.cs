using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float Damage = 1f;
    private bool CanAttack = false;
    private GameObject enemy;
    private void Update()
    {
        if(CanAttack && Input.GetKeyDown(KeyCode.F))
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            CanAttack = true;
            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CanAttack = false;
            enemy = collision.gameObject;
        }
    }
}
