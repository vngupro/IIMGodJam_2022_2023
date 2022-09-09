using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;
    public List<Transform> spawnPoints = new List<Transform>();

    [Header("_____ DEBUG ______")]
    [SerializeField] private Wave currentWave;
    [SerializeField] private int currentWaveNumber;
    [SerializeField] private int enemiesRemainingToSpawn;
    [SerializeField] private int enemiesRemainingInAlive;
    [SerializeField] private float nextSpawnTime;

    void Start()
    {
        NextWave();
    }

    void Update()
    {
        if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            int rng = Random.Range(0, spawnPoints.Count - 1);
            Enemy spawnEnemy = Instantiate(enemy, spawnPoints[rng].position, Quaternion.identity) as Enemy;
            spawnEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingInAlive--;

        if(enemiesRemainingInAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        if(currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingInAlive = enemiesRemainingToSpawn;
        }   
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
