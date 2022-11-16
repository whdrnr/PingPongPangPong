using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdmobManager : Singleton<AdmobManager>
{
    void Start()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
            .SetTestDeviceIds(new List<string>() { "6B903E9177F61FED" }).build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        LoadBannerAd();
        LoadFrontAd();
        LoadRewardAd();

        GameManager.Instance.gameOverDelegate += ShowFrontAd;
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region #배너 광고
    const string bannerID = "ca-app-pub-2384053122441182~1809691604";
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    BannerView bannerAd;

    void LoadBannerAd()
    {
        if (GameManager.Instance.Data.IsAdParchase == false)
        {
            AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

            bannerAd = new BannerView(bannerTestID, adSize, AdPosition.Bottom);
            bannerAd.LoadAd(GetAdRequest());
        }
    }

    public void HideBannerAd() => bannerAd.Hide();

    #endregion

    #region #전면 광고
    const string frontID = "ca-app-pub-2384053122441182/3502085217";
    const string frontTestID = "ca-app-pub-3940256099942544/8691691433";
    InterstitialAd frontAd;

    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(frontTestID);
        frontAd.LoadAd(GetAdRequest());
    }

    public void ShowFrontAd()
    {
        if (GameManager.Instance.Data.IsAdParchase == true) return;

        frontAd.Show();
        LoadFrontAd();
    }
    #endregion

    #region #리워드 광고
    const string RewardID = "ca-app-pub-2384053122441182/7346659557";
    const string SkinRewardID = "ca-app-pub-2384053122441182/3330273362";
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    RewardedAd GameOverRewardAd;
    RewardedAd SkinRewardAd;

    void LoadRewardAd()
    {
        GameOverRewardAd = new RewardedAd(rewardTestID);
        SkinRewardAd = new RewardedAd(rewardTestID);

        RewardAdHandle();

        GameOverRewardAd.LoadAd(GetAdRequest());
        SkinRewardAd.LoadAd(GetAdRequest());
    }

    void RewardAdHandle()
    {
        GameOverRewardAd.OnAdClosed += HandleOnAdClosed;
        GameOverRewardAd.OnUserEarnedReward += HandleOnUserEarnedGameOverReward;

        SkinRewardAd.OnUserEarnedReward += HandleOnUserEarnedSkinReward;
    }

    public void ShowGameOverRewardAd()
    {
        GameOverRewardAd.Show();
        LoadRewardAd();
    }

    public void ShowSkinRewardAd()
    {
        SkinRewardAd.Show();
        LoadRewardAd();
    }

    //#광고가 종료되었을 때
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //새로운 광고 Load
    }

    //#광고를 끝까지 시청하였을 때
    public void HandleOnUserEarnedGameOverReward(object sencer, Reward args)
    {
        GameManager.Instance.ReStartBall();
    }

    public void HandleOnUserEarnedSkinReward(object sencer, Reward args)
    {
    }
    #endregion
}