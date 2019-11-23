using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public float EnemySpawnTime;

    int CurrentWaveEnemies;
    int CurrentWave = 1;

    void Start()
    {
        CurrentWaveEnemies = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
