using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    public bool isTestMode;
    public Text LogText;
    public Button FrontAdsBtn, RewardAdsBtn;

    void Start()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration
           .Builder()
           .SetTestDeviceIds(new List<string>() { "6B903E9177F61FED" }) // test Device ID
           .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        LoadBannerAd();
        //LoadFrontAd();
        //LoadRewardAd();
    }

    void Update()
    {
        //FrontAdsBtn.interactable = frontAd.IsLoaded();
        //RewardAdsBtn.interactable = rewardAd.IsLoaded();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region ¹è³Ê ±¤°í
    const string bannerID = "ca-app-pub-2384053122441182/4618891615";
    BannerView bannerAd;

    void LoadBannerAd()
    {
        bannerAd = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd.LoadAd(GetAdRequest());
        ToggleBannerAd(false);
    }

    public void ToggleBannerAd(bool b)
    {
        if (b) bannerAd.Show();
        else bannerAd.Hide();
    }
    #endregion

    #region Àü¸é ±¤°í
    const string frontID = "";
    InterstitialAd frontAd;

    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(frontID);
        frontAd.LoadAd(GetAdRequest());
        frontAd.OnAdClosed += (sender, e) =>
        {
            LogText.text = "Àü¸é±¤°í ¼º°ø";
        };
    }

    public void ShowFrontAd()
    {
        frontAd.Show();
        LoadFrontAd();
    }
    #endregion

    #region #¸®¿öµå ±¤°í
    const string RewardID = "ca-app-pub-2384053122441182/8016045296";
    RewardedAd RewardAd;

    void LoadRewardAd()
    {
        RewardAd = new RewardedAd(RewardID);
        RewardAd.LoadAd(GetAdRequest());

        RewardAd.OnUserEarnedReward += (sender, e) =>
        {
            LogText.text = "¸®¿öµå ±¤°í ¼º°ø";
        };
    }

    public void ShowRewardAd()
    {
        RewardAd.Show();
        LoadRewardAd();
    }
    #endregion
}