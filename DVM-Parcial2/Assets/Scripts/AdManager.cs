using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdManager : MonoBehaviourSingleton<AdManager>
{
    private string GameIdAndroid = "3373380";
    private string VideoKey = "video";
    private string RewardedKey = "rewardedVideo";

    public override void Awake()
    {
        base.Awake();
        Advertisement.Initialize(GameIdAndroid, true);
    }

    public void UIWatchRewardedAd()
    {
        WatchRewardedVideoAd(VideoRewardedAdEnded);
    }

    public void WatchRewardedVideoAd(Action<ShowResult> result)
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = result;

        if (Advertisement.IsReady(RewardedKey))
            Advertisement.Show(RewardedKey, so);
        else
            Debug.Log("No carga, master");
    }

    public void VideoRewardedAdEnded(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("El Ad Rewarded fallo");
                break;
            case ShowResult.Finished:
                Debug.Log("El Ad Rewarded termino");
                break;
            case ShowResult.Skipped:
                Debug.Log("El Ad Rewarded se skipeo");
                break;
        }
    }
}
