using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject []SpawnPoints;
    public GameObject []EnemiesPrefabs;
    public GameObject EnemiesParent;
    public float TimeToSpawn;

    const int MaxEnemies = 7;
    List<GameObject> EnemyList;
    float SpawnTimer;

    void Start()
    {
        EnemyList = new List<GameObject>();
        for (int i = 0; i < MaxEnemies; i++)
        {
            int randomEnemy = Random.Range(0, EnemiesPrefabs.Length);
            GameObject go = Instantiate(EnemiesPrefabs[randomEnemy], EnemiesParent.transform);
            go.name = EnemiesPrefabs[randomEnemy].name + i;
            go.SetActive(false);
            EnemyList.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
