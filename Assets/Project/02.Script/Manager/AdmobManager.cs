using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdmobManager : Singleton<AdmobManager>
{
    public bool isTestMode;
    public Text LogText;

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
    BannerView bannerAd;

    void LoadBannerAd()
    {
        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        //AdSize adSize = new AdSize(320, 50);

        bannerAd = new BannerView(bannerID, adSize, AdPosition.Bottom);
        bannerAd.LoadAd(GetAdRequest());
    }
    #endregion

    #region #전면 광고
    const string frontID = "ca-app-pub-2384053122441182/3502085217";
    InterstitialAd frontAd;

    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(frontID);
        frontAd.LoadAd(GetAdRequest());
    }

    public void ShowFrontAd()
    {
        frontAd.Show();
        LoadFrontAd();
    }
    #endregion

    #region #리워드 광고
    const string RewardID = "ca-app-pub-2384053122441182/8016045296";
    RewardedAd GameOverRewardAd;

    void LoadRewardAd()
    {
        GameOverRewardAd = new RewardedAd(RewardID);
        RewardAdHandle(GameOverRewardAd);
        GameOverRewardAd.LoadAd(GetAdRequest());
    }

    void RewardAdHandle(RewardedAd _RewardedAd)
    {
        _RewardedAd.OnAdClosed += HandleOnAdClosed;
        _RewardedAd.OnUserEarnedReward += HandleOnUserEarnedReward;
    }

    public void ShowRewardAd()
    {
        GameOverRewardAd.Show();
        LoadRewardAd();
    }

    //#광고가 종료되었을 때
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //새로운 광고 Load
    }

    //#광고를 끝까지 시청하였을 때
    public void HandleOnUserEarnedReward(object sencer, Reward args)
    {
        GameManager.Instance.IsAdSee = true;

        UIManager.Instance.AD_Panel.SetActive(false);
    }
    #endregion
}