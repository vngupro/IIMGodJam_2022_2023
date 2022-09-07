using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float timeBeforeSpawn = 5.0f;
    public List<Tile> SpawnPoint = new List<Tile>();
    public List<Tile> DecorPoint = new List<Tile>();

    public float chrono = 0.0f;

    private void Start()
    {
        SpawnPoint = GridManager.Instance.borderTiles;
        chrono = timeBeforeSpawn;
    }

    private void FixedUpdate()
    {
        chrono -= Time.fixedDeltaTime;
        if (chrono < 0.0f)
        {
            SpawnEnemy();
            chrono = timeBeforeSpawn;
        }
    }
    public void SpawnEnemy()
    {
        int selectedSpawnPoint = Random.Range(0,SpawnPoint.Count);
        Vector3 spawnPosition = SpawnPoint[selectedSpawnPoint].transform.position;

        GameObject selectedEnemy = EnemyPool.instance.SelectEnemy();
        if(selectedEnemy == null)
        {
            return;
        }

        selectedEnemy.transform.position = spawnPosition;
        selectedEnemy.SetActive(true);
    }
}
