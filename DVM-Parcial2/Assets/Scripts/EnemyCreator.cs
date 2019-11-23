using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject []SpawnPoints;
    public GameObject []EnemiesPrefabs;
    public GameObject EnemiesParent;
    public Transform PlayerTransform;
    public float TimeToSpawn;
    public int WaveEnemiesLeft;

    const int MaxEnemies = 7;
    List<GameObject> EnemyList;
    float SpawnTimer = 0;

    private void Awake()
    {
        EnemyWaveManager.OnNewWaveAction = StartNextWave;
    }

    void Update()
    {
        if(SpawnTimer >= TimeToSpawn && WaveEnemiesLeft > 0)
        {
            SpawnTimer = 0;
            int randomSpawnPoint = Random.Range(0, SpawnPoints.Length);
            GameObject go = ObjectPooler.Instance.GetPooledObject("Enemy");
            go.transform.position = SpawnPoints[randomSpawnPoint].transform.position;
            go.transform.rotation = Quaternion.identity;
            go.SetActive(true);
            go.GetComponent<PataoBehaviour>().SetPlayerTransform(PlayerTransform);

            WaveEnemiesLeft--;
        }
        SpawnTimer += Time.deltaTime;
    }

    public void StartNextWave()
    {
        TimeToSpawn = EnemyWaveManager.Instance.GetCurrentWaveSpawnTime();
        WaveEnemiesLeft = EnemyWaveManager.Instance.GetCurrentWaveEnemies();
        SpawnTimer = 0;
    }
}
