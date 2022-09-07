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

    private Coroutine moveCoroutine = null;
    [SerializeField] private Direction direction = Direction.NONE;
    private int lastRng = -1;
    //[SerializeField] List<Transform> context = new List<Transform>();
    [SerializeField] bool forceDirection = false;

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
            direction = Direction.RIGHT;
        }
        else
        {
            if(moveCoroutine == null)
            {
                moveCoroutine =  StartCoroutine(MoveRandom());
            }
        }

        GetNearbyObjects();
    }

    public void SetTarget(GameObject value)
    {
        target = value;
    }

    private void MoveToTarget()
    {
        rb.MovePosition(rb.position + saveVelocity);
        //transform.position += (Vector3)saveVelocity;
    }

    IEnumerator MoveRandom()
    {
        
        int rng = Random.Range(0, 3);

        while(lastRng == rng)
        {
            rng = Random.Range(0, 3);
        }

        Vector2 dir = new Vector2(0, 0);
        
        switch(rng)
        {
            case 0: dir = new Vector2(1, 0); direction = Direction.RIGHT;  break;
            case 1: dir = new Vector2(-1, 0); direction = Direction.LEFT; break;
            case 2: dir = new Vector2(0, 1); direction = Direction.UP;  break;
            case 3: dir = new Vector2(0, -1); direction = Direction.DOWN; break;
        }

        float timer = timeBeforeChangeDirection;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            rb.MovePosition(rb.position + dir * enemyAgent.speed * Time.fixedDeltaTime);
            yield return null;
        }

        moveCoroutine = null;
        lastRng = rng;
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
}

public enum Direction
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}