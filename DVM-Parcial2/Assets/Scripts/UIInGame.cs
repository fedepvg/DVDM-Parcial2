using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public PlayerController Player;
    public Slider HealthSlider;
    public GameObject Crosshair;
    public Text WaveText;
    public Text AliveEnemiesText;
    public Text BulletsText;
    public Text NewWaveText;
    public GameObject VRGOCanvas;
    public GameObject PCGOCanvas;

    int PrevPlayerHealth;
    int ActualWave;
    int AliveEnemies;
    int BulletsLeft;
    bool UINewWave = false;
    float NewWaveTimer = 1f;

    private void Awake()
    {
#if UNITY_ANDROID
        GetComponent<EventSystem>().enabled = false;
        GetComponent<StandaloneInputModule>().enabled = false;
#endif
    }

    void Start()
    {
        GameManager.OnPlayerKilledAction = ActivateGOCanvas;
        ActualWave = 0;
        PrevPlayerHealth = Player.GetHealth();
        AliveEnemies = 0;
#if UNITY_ANDROID
        Crosshair.SetActive(false);
        GetComponent<EventSystem>().enabled = false;
        GetComponent<StandaloneInputModule>().enabled = false;
#endif
#if UNITY_STANDALONE
        PCGOCanvas.SetActive(false);
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
            AliveEnemiesText.text = currentAliveEnemies.ToString();
        }

        int currentBullets = Player.GetBulletsLeft();
        if (currentBullets != BulletsLeft)
        {
            BulletsLeft = currentBullets;
            BulletsText.text = currentBullets.ToString();
        }

        int currentWave = EnemyWaveManager.Instance.GetCurrentWave();
        if(currentWave != ActualWave)
        {
            ActualWave = currentWave;
            UINewWave = true;
            NewWaveText.gameObject.SetActive(true);
        }


        if (UINewWave)
        {
            NewWaveTimer -= Time.deltaTime;
            if(NewWaveTimer <= 0)
            {
                WaveText.text = "Wave: " + ActualWave;
                NewWaveTimer = 1;
                NewWaveText.gameObject.SetActive(false);
                UINewWave = false;
            }
            NewWaveText.text = "Starting New Wave";
        }
        else
        {
            NewWaveText.gameObject.SetActive(false);
        }
    }

    void ActivateGOCanvas()
    {
#if UNITY_ANDROID
        VRGOCanvas.SetActive(true);
#endif
#if UNITY_STANDALONE
        PCGOCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
#endif
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
