using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Panel ���� ����")]
    public CanvasGroup Lobby_CG;
    public CanvasGroup Play_CG;

    [Header("Lobbyl ���� ����")]
    public GameObject Title_Panel;
    public GameObject Main_Panel;

    [Header("Play-Panel��T���� ����")]
    public TextMeshProUGUI CurBounce_Txt;
    public TextMeshProUGUI Wave_Txt;
    public GameObject AD_Panel;
    public GameObject Die_Panel;

     [Header("Main-Panel��Txt ���� ����")]
    public TextMeshProUGUI BeforeWave_Txt;
    public TextMeshProUGUI MaxWave_Txt;

    [Header("���� bool ����")]
    public bool IsBGMOn = true;
    public bool IsSFXOn = true;
    public bool IsVibrationOn = true;

    GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;
    }

    IEnumerator IGameStart()
    {
        if (GM.IsGame == false)
        {
            //#���̵� ��/�ƿ� 
            FadeUI(1, 0, true, false);
            GM.IsGame = true;

            Title_Panel.SetActive(false);

            //#���� �ʱ�ȭ
            GM.BounceNum = 3;
            CurBounce_Txt.text = GM.BounceNum.ToString();

            GM.CurWave = 1;
            Wave_Txt.text = GM.CurWave.ToString();

            yield return new WaitForSeconds(0.5f);

            //#Danger, Pong ��ġ ����
            GM.gameStartDelegate();

            //#������ ����
            ItemInitManager.Instance.ItemInit();
        }
    }

    public void PlayGame_Btn() => StartCoroutine(IGameStart());

    public void RewardAd_Btn() => AdmobManager.Instance.ShowRewardAd();

    public void GmaeOver_Btn()
    {
        //#UI On/Off
        FadeUI(0, 1, false, true);

        //#Text ��ȣ�ۿ�
        BeforeWave_Txt.text = GM.BeforeWave.ToString();
        MaxWave_Txt.text = "High Point " + GM.MaxWave.ToString();

        //#BGM ���
        SoundManager.Instance.PlayBGM("BG1", 1);

        GM.gameOverDelegate();
        GM.IsAdSee = false;

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
