using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviourSingleton<EnemyWaveManager>
{
    public delegate void OnNewWave();
    public static OnNewWave OnNewWaveAction;
    public int FirstWaveEnemies;

    float EnemySpawnTime = 3;
    int CurrentWaveEnemies;
    int AliveEnemies;
    int CurrentWave = 1;

    private void Start()
    {
        if (OnNewWaveAction != null)
            OnNewWaveAction();
        ResetWaves();
    }

    public int GetCurrentWaveEnemies()
    {
        return CurrentWaveEnemies;
    }
    
    public float GetCurrentWaveSpawnTime()
    {
        return EnemySpawnTime;
    }
    
    public int GetCurrentWave()
    {
        return CurrentWave;
    }
    
    public int GetAliveEnemies()
    {
        return AliveEnemies;
    }

    void CheckWaveStatus()
    {
        if(AliveEnemies <= 0)
            StartCoroutine(StartNewWave());
    }

    public void KillEnemy()
    {
        AliveEnemies--;
        CheckWaveStatus();
    }

    void ResetWaves()
    {
        CurrentWave = 1;
        CurrentWaveEnemies = FirstWaveEnemies;
        AliveEnemies = CurrentWaveEnemies;
        EnemySpawnTime = 3;
        if (OnNewWaveAction != null)
            OnNewWaveAction();
    }

    IEnumerator StartNewWave()
    {

        yield return new WaitForSeconds(3);

        CurrentWave++;
        CurrentWaveEnemies += 3;
        AliveEnemies = CurrentWaveEnemies;
        EnemySpawnTime -= 0.5f;
        if (EnemySpawnTime <= 0.5f)
            EnemySpawnTime = 0.5f;
        if (OnNewWaveAction != null)
            OnNewWaveAction();
    }
}
