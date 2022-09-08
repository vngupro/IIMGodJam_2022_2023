using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public Wave[] waves;

    public Enemy enemy;
    

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingInAlive;
    float nextSpawnTime;

    public Transform spawnPoint;

    

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

            //Enemy spawnEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            Enemy spawnEnemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity) as Enemy;

            //GameObject spawnEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

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
