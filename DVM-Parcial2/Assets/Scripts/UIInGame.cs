using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public PlayerController Player;
    public Slider HealthSlider;
    public GameObject Crosshair;
    public Text WaveText;
    public Text AliveEnemiesText;

    int PrevPlayerHealth;
    int ActualWave;
    int AliveEnemies;

    private void Awake()
    {
        EnemyWaveManager.OnNewWaveAction += SetNextWave;
    }

    void Start()
    {
        ActualWave = EnemyWaveManager.Instance.GetCurrentWave();
        PrevPlayerHealth = Player.GetHealth();
        AliveEnemies = 0;
#if UNITY_ANDROID
        Crosshair.SetActive(false);
#endif
    }

    void Update()
    {
        int currentPlayerHealth = Player.GetHealth();
        if(currentPlayerHealth != PrevPlayerHealth)
        {
            PrevPlayerHealth = currentPlayerHealth;
            HealthSlider.value = currentPlayerHealth;
        }

        int currentAliveEnemies = EnemyWaveManager.Instance.GetAliveEnemies();
        if (currentAliveEnemies != AliveEnemies)
        {
            AliveEnemies = currentAliveEnemies;
            AliveEnemiesText.text = "Enemies: "+ currentAliveEnemies;
        }
    }

    void SetNextWave()
    {
        ActualWave = EnemyWaveManager.Instance.GetCurrentWave();
        WaveText.text = "Wave: " + ActualWave;
    }
}
