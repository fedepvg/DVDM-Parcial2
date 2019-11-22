using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject []SpawnPoints;
    public GameObject []EnemiesPrefabs;
    public GameObject EnemiesParent;
    public Transform PlayerTransform;
    public float TimeToSpawn;

    const int MaxEnemies = 7;
    List<GameObject> EnemyList;
    float SpawnTimer = 0;
    int ActivatedEnemies = 0;

    void Start()
    {
        EnemyList = new List<GameObject>();
        for (int i = 0; i < MaxEnemies; i++)
        {
            int randomEnemy = Random.Range(0, EnemiesPrefabs.Length);
            GameObject go = Instantiate(EnemiesPrefabs[randomEnemy], EnemiesParent.transform);
            go.name = EnemiesPrefabs[randomEnemy].name + i;
            go.SetActive(false);
            go.GetComponent<PataoMovement>().PlayerTransform = PlayerTransform;
            EnemyList.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnTimer >= TimeToSpawn && ActivatedEnemies < MaxEnemies)
        {
            SpawnTimer = 0;
            int enemyIndex;
            for(enemyIndex = 0; enemyIndex < EnemyList.Count; enemyIndex++)
            {
                if (!EnemyList[enemyIndex].activeSelf)
                    break;
            }
            int randomSpawnPoint = Random.Range(0, SpawnPoints.Length);
            EnemyList[enemyIndex].transform.position = SpawnPoints[randomSpawnPoint].transform.position;
            EnemyList[enemyIndex].transform.rotation = Quaternion.identity;
            EnemyList[enemyIndex].SetActive(true);
            ActivatedEnemies++;
        }
        SpawnTimer += Time.deltaTime;
    }
}
