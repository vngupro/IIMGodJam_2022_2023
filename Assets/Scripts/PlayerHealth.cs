using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTime = 2f;

    public HealthBar healthBar;

    public bool isInvincible = false;

    public SpriteRenderer graphics;

    public GameObject deathSceneUI;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }


    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(InvincibilityDelay());


        }
        
    }
    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.2f);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator InvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible=false;
    }

    public void Die()
    {
        SoundManager.Instance.PlaySound("Dead");
        Debug.Log("IsDead");
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCollider.enabled = false;        
        deathSceneUI.SetActive(true);
        Shooting.instance.enabled = false;

        //Enemy.instance.enabled = false;


    }

}
