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
    public GameObject VrStuff;

    void Start()
    {
#if UNITY_ANDROID
        GPSManager.Instance.LogIn();
        VrStuff.SetActive(true);
#endif
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    public void WatchAd()
    {
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

    public void AddClearedWave()
    {
        WavesCleared++;
#if UNITY_ANDROID
        if (WavesCleared == WavesToUnlockAchievement)
        {
            GPSManager.Instance.UnlockAchievementWaves();
        }
#endif
    }

    public void AddDeadEnemy()
    {
        DeadPataos++;
#if UNITY_ANDROID
        if (DeadPataos == DeadPataosToUnlockAchievement)
        {
            GPSManager.Instance.UnlockAchievementKiller();
        }
#endif
    }
}
