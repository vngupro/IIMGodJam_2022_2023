using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth = 0;
    public float maxHealth = 0;

    public void SetMaxHealth(float _maxHealth)
    {
        currentHealth = maxHealth;
        maxHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        // VFX Sound etc...)
    }
}
