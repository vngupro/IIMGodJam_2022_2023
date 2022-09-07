using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public List<EnemyPoolElement> enemyList;
    public List<GameObject> enemy_1;
    public List<GameObject> enemy_2;
    public GameObject[,] enemyDeactivated;
    public GameObject[,] enemyActivated;
    public Dictionary<int, int> countsDeactivated = new Dictionary<int, int>();
    public Dictionary<int, int> countsActivated = new Dictionary<int, int>();

    public static EnemyPool instance { get; private set; }
    private void Awake()
    {
        instance = this;

        enemyDeactivated = new GameObject[enemyList.Count, 99];
        enemyActivated = new GameObject[enemyList.Count, 99];
        foreach (EnemyPoolElement element in enemyList)
        {
            countsDeactivated.Add(element.id, element.count);
            countsActivated.Add(element.id, 0);
            int count = 0;

            while (count < element.count)
            {
                GameObject spawnedEnemy = Instantiate(element.enemyPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                spawnedEnemy.name = element.name + "_" + count;
                spawnedEnemy.gameObject.transform.parent = gameObject.transform;
                spawnedEnemy.SetActive(false);
                enemyDeactivated[element.id, count] = spawnedEnemy;
                count++;
            }
        }
    }

    public GameObject SelectEnemy()
    {
        GameObject selected = null;
        int id = Random.Range(0, enemyList.Count - 1);

        int count = 0;
        countsDeactivated.TryGetValue(id, out count);
        if (count > 0)
        {
            selected = enemyDeactivated[id, count - 1];
            countsDeactivated[id] = count - 1;
        }
        return selected;
    }
}

[System.Serializable]
public class EnemyPoolElement
{
    public string name;
    public GameObject enemyPrefab;
    public int count = 10;
    public int id = 0;
}
