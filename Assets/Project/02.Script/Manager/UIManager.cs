using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using GooglePlayGames;
using DG.Tweening;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Panel 관련 참조")]
    public CanvasGroup Lobby_CG;
    public CanvasGroup Play_CG;

    [Header("Lobbyl 관련 참조")]
    public GameObject Title_Panel;
    public GameObject Main_Panel;

    [Header("Play-Panel의T관련 참조")]
    public TextMeshProUGUI CurBounce_Txt;
    public TextMeshProUGUI Wave_Txt;
    public GameObject AD_Panel;
    public GameObject Die_Panel;

     [Header("Main-Panel의Txt 관련 참조")]
    public TextMeshProUGUI BeforeWave_Txt;
    public TextMeshProUGUI MaxWave_Txt;
    public IAPButton AdRemove_Btn;

    GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;

        //#광고 제거를 구매했다면 버튼 Off
        if (GM.Data.IsAdParchase == true)
            AdRemove_Btn.buttonType = IAPButton.ButtonType.Restore;
    }

    IEnumerator IGameStart()
    {
        if (GM.IsGame == false)
        {
            //#페이드 인/아웃 
            FadeUI(1, 0, true, false);
            GM.IsGame = true;

            Title_Panel.SetActive(false);

            //#점수 초기화
            GM.BounceNum = 3;
            CurBounce_Txt.text = GM.BounceNum.ToString();

            GM.CurWave = 1;
            Wave_Txt.text = GM.CurWave.ToString();

            yield return new WaitForSeconds(0.5f);

            //#Danger, Pong 위치 조정
            GM.gameStartDelegate();

            //#아이템 생성
            ItemInitManager.Instance.ItemInit();
        }
    }

    public void AdRemovePurchaseComplete()
    {
        GM.Data.IsAdParchase = true;
        AdRemove_Btn.buttonType = IAPButton.ButtonType.Restore;
        AdmobManager.Instance.HideBannerAd();
    }

    public void AdRemovePurchaseFail()
    {
        Debug.Log("구매 실패");
    }

    public void PlayGame_Btn() => StartCoroutine(IGameStart());

    public void RewardAd_Btn()
    {
        GM.IsCountDown = false;

        if (GM.Data.IsAdParchase == false)
            AdmobManager.Instance.ShowGameOverRewardAd();
        else
            GM.ReStartBall();
    }

    public void LeaderBoardOn_Btn()
    {
        Social.ReportScore(GameManager.Instance.Data.MaxWave, GPGSIds.leaderboard_ranking, (bool IsSuccess) => { });
        Social.ShowLeaderboardUI();

        //#현재 기기와 연결된 계정이 인증이 아직 안됬는가?
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool IsSuccess) =>
            {
                if (IsSuccess == true)  //#로그인 성공시 상호작용
                {
                    GM.Data.IsGoogleLogin = true;

                    Social.ReportScore(GameManager.Instance.Data.MaxWave, GPGSIds.leaderboard_ranking, (bool IsSuccess) => { }); ;
                    Social.ShowLeaderboardUI();
                }
            });
        }
    }

    public void GmaeOver_Btn()
    {
        if (GM.CurWave >= 5) AdmobManager.Instance.ShowFrontAd();

        //#UI On/Off
        FadeUI(0, 1, false, true);

        //#Text 상호작용
        BeforeWave_Txt.text = GM.Data.BeforeWave.ToString();
        MaxWave_Txt.text = "High Point " + GM.Data.MaxWave.ToString();

        //#BGM 재생
        SoundManager.Instance.PlayBGM("BG1", 0.5f);

        GM.gameOverDelegate();
        GM.IsAdSee = false;
        GM.IsCountDown = false;

        AD_Panel.SetActive(false);
        Die_Panel.SetActive(false);
        Main_Panel.SetActive(true);
    }

    void FadeUI(int _FadeNum1, int _FadeNum2, bool _PlayUI, bool _LobbyUI)
    {
        Play_CG.DOFade(_FadeNum1, 0.5f);
        Play_CG.blocksRaycasts = _PlayUI;
        Play_CG.interactable = _PlayUI;

        Lobby_CG.DOFade(_FadeNum2, 0.5f);
        Lobby_CG.blocksRaycasts = _LobbyUI;
        Lobby_CG.interactable = _LobbyUI;
    }
}
