using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public delegate void OnPlayerKilled();
    public static OnPlayerKilled OnPlayerKilledAction;
    int WavesCleared = 0;
    int WavesToUnlockAchievement = 4;
    int DeadPataos = 0;
    int DeadPataosToUnlockAchievement = 15;

    void Start()
    {
#if UNITY_ANDROID
        GPSManager.Instance.LogIn();
#endif
    }

    void Update()
    {
#if UNITY_ANDROID
        if (EnemyWaveManager.Instance.GetCurrentWave() == WavesToUnlockAchievement)
        {
            GPSManager.Instance.UnlockAchievementWaves();
        }
        
        if (DeadPataos == DeadPataosToUnlockAchievement)
        {
            GPSManager.Instance.UnlockAchievementKiller();
        }
#endif
    }

    public void WatchAd()
    {
        Time.timeScale = 1;
        AdManager.Instance.UIWatchRewardedAd();        
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void KillPlayer()
    {
        Time.timeScale = 0;
        if (OnPlayerKilledAction != null)
            OnPlayerKilledAction();
    }
}
