using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(SpriteRenderer))]
[System.Serializable]
public class Enemy : MonoBehaviour
{
    public EnemyAgent enemyAgent;

    [Header("____________DEBUG___________")]
    public float damage = 0.0f;
    public Vector2 speed = new Vector2(0.0f, 0.0f);
    public float speedMultiplier = 1.0f;
    public GameObject target;

    public EnemyHealth health;
    public SpriteRenderer spriteRenderer;
    public Vector2 saveVelocity; // save Direction

    public virtual void Awake()
    {
        if(enemyAgent == null)
        {
            Debug.LogError(gameObject.name + "Enemy Agent is null.");
            return;
        }

        health = GetComponent<EnemyHealth>();
        health.SetMaxHealth(enemyAgent.health);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyAgent.sprite;

        speed = new Vector2(enemyAgent.speed, enemyAgent.speed) * speedMultiplier;
        damage = enemyAgent.damage;
    }
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
        if(target != null)
        {
            MoveToTarget();
        }
        else
        {
            MoveRandom();
        }
    }

    public void SetTarget(GameObject value)
    {
        target = value;
    }

    public void MoveToTarget()
    {
        transform.position += (Vector3)saveVelocity;
    }

    public void MoveRandom()
    {
        transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y + 1.0f);
    }

    public virtual void CalculateVelocity()
    {
        Vector2 vEnemyShip = target.transform.position - transform.position;
        Vector2 dirEnemy = vEnemyShip.normalized;
        Vector2 velocity = dirEnemy * speed * Time.fixedDeltaTime;
        saveVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Contains("PlayerBullet"))
        {
            //float damage = other.gameObject.GetComponent<DamageSystem>().damage;
            //health.TakeDamage(damage);
            health.TakeDamage(1);
            Destroy(other.gameObject);

            //SoundManager.Instance.PlaySound("Enemy_TakeDamage");
        }

        if(other.gameObject.tag.Contains("Weapons"))
        {
            //float damage = other.gameObject.GetComponent<DamageSystem>().damage;
            //health.TakeDamage(damage);
            health.TakeDamage(1);

            //SoundManager.Instance.PlaySound("Enemy_TakeDamage");
        }
    }

    public void TakeDamage(int value)
    {
        health.TakeDamage(value);

        if(health.currentHealth < 0)
        {
            // Coroutine ? or OnDestroy ?
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        // Feedback | VFX | Sound
    }
}