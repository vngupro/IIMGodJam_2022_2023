using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(SpriteRenderer))]
[System.Serializable]
public class Enemy : MonoBehaviour
{
    public EnemyAgent enemyAgent;
    public float timeBeforeChangeDirection = 3.0f;
    public float distanceTolerance = 0.2f;                  // anti trembling mouvement

    [Header("____________DEBUG___________")]
    public float damage = 0.0f;
    public Vector2 speed = new Vector2(0.0f, 0.0f);
    public float speedMultiplier = 1.0f;
    public GameObject target;

    public EnemyHealth health;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Collider2D _collider;
    public Vector2 saveVelocity; // save Direction

    //[SerializeField] List<Transform> context = new List<Transform>();
    [SerializeField] bool forceDirection = false;
    private float maxHeight = 0.0f;
    private float minHeight = 0.0f;
    private float maxWidth = 0.0f;
    private float minWidth = 0.0f;
    private float halfWidth = 0.0f;
    [SerializeField] Vector2 targetPosition = new Vector2(0.0f, 0.0f);

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

        rb = GetComponent<Rigidbody2D>();

        speed = new Vector2(enemyAgent.speed, enemyAgent.speed) * speedMultiplier;
        damage = enemyAgent.damage;

        _collider = GetComponent<Collider2D>();


    }
    public virtual void Start()
    {
        minHeight = GridManager.Instance.GetMinHeight() / 2;
        maxHeight = GridManager.Instance.GetMaxHeight() / 2;
        halfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        targetPosition = transform.position;
    }

    public virtual void Update()
    {
        if(target != null)
        {
            if(Vector2.Distance(target.transform.position, transform.position) > distanceTolerance)
            {
                CalculateVelocity();
                MoveToTarget();
            }
        }
        else if (forceDirection)
        {
            rb.MovePosition(rb.position + new Vector2(1, 0) * enemyAgent.speed * Time.fixedDeltaTime);
        }
        else
        {
            MoveRandom();
        }
        
        GetNearbyObjects();
    }

    public void SetTarget(GameObject value)
    {
        target = value;
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemyAgent.speed * Time.fixedDeltaTime);
    }

    public void MoveRandom()
    {
        if(Vector2.Distance(targetPosition, transform.position) > distanceTolerance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enemyAgent.speed * Time.fixedDeltaTime);
        }
        else
        {
            minWidth = Camera.main.transform.position.x - halfWidth;
            maxWidth = Camera.main.transform.position.x + halfWidth;

            float newPosX = Random.Range(minWidth, maxWidth);
            float newPosY = Random.Range(minHeight, maxHeight);
            targetPosition = new Vector2(newPosX, newPosY);
        }
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

        if(other.gameObject.tag.Contains("PlayerWeapon"))
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

    private void GetNearbyObjects()
    {
        //context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, enemyAgent.detectionRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != _collider && c.gameObject.CompareTag("Player") && target == null)
            {
                target = c.gameObject;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyAgent.detectionRadius);
    }
}

public enum Direction
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}