using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPSManager : MonoBehaviourSingleton<GPSManager>
{
#if UNITY_ANDROID
    bool LoggedIn;

    private void Start()
    {
        InitializeGooglePlayServices();
    }

    void InitializeGooglePlayServices()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    public void ShowAchievements()
    {
        if (LoggedIn)
            Social.ShowAchievementsUI();
    }

    public void UnlockAchievementKiller()
    {
        if (LoggedIn)
        {
            Social.ReportProgress(GPGSIds.achievement_asesino_de_pataos, 100.0f, (bool success) =>
            {
                if (success)
                    ShowAchievements();
            });
        }
    }

    public void UnlockAchievementWaves()
    {
        if (LoggedIn)
        {
            Social.ReportProgress(GPGSIds.achievement_mejor_jugador_de_la_historia_del_mundo, 100.0f, (bool success) =>
            {
                if (success)
                    ShowAchievements();
            });
        }
    }

    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) => {
            
        });
    }
#endif
}
