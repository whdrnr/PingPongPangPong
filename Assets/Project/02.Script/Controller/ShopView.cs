using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class SkinPong
{
    public string Name;
    public bool IsSelect; //#선택 가능한지?
    public bool IsUse; //#사용중인지?

    [Header("광고 스킨인 경우")]
    public bool IsAdSkin; //#광고스킨인지
    public int AdNum;
    public TextMeshProUGUI AdNum_Txt;

    [Header("Img/Sprite 참조 관련")]
    public Sprite Skin_Spirte;
    public GameObject Lock_Img;
    public Image SkinPong_Img;
}

public class ShopView : MonoBehaviour
{
    [Header("상점 관련 참조")]
    public GameObject Shop_Panel;
    public List<GameObject> PongSelect_Btn = new List<GameObject>();
    public TextMeshProUGUI SkinHave_Txt;
    public int SkinNum = 0;

    [Header("Sprite 관련 참조")]
    public Sprite Select_Sprite;
    public Sprite NonSelect_Sprite;

    [Header("스킨별 웨이브 잠금해제 난이도 관련 참조")]
    public int SpiralClearWave = 15;
    public int ZebraClearWave = 20;
    public int LeopardClearWave = 25;
    public int CookieClearWave = 30;
    public int BasketballClearWave = 35;
    public int PearlClearWave = 40;
    public int DonutlClearWave = 45;
    public int CoinClearWave = 50;
    public int WafflelClearWave = 55;
    public int MarsClearWave = 60;
    public int OrangeClearWave = 65;

    GameManager GM;

    void Start()
    {
        //#Delegate 연결
        GameManager.Instance.gameOverDelegate += SkinWaveClear;

        GM = GameManager.Instance;
    }

    public void PongSelect_Btn_Click(int _Num)
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (GM.Data.Pongs[_Num].IsSelect == true) //#스킨 해체 되어있는 상태
        {
            for (int i = 0; i < GM.Data.Pongs.Count; i++)
            {
                GM.Data.Pongs[i].IsUse = false;
                PongSelect_Btn[i].GetComponent<Image>().sprite = NonSelect_Sprite;
            }

            GM.Data.Pongs[_Num].IsUse = true;
            PongSelect_Btn[_Num].GetComponent<Image>().sprite = Select_Sprite;

            GameManager.Instance.Pong.GetComponent<SpriteRenderer>().sprite =
                GM.Data.Pongs[_Num].Skin_Spirte;
        }
        else //#스킨 해체가 안되어있는 상태
        {
            //#AD Skin
            if(GM.Data.Pongs[_Num].IsAdSkin == true)
            {
                AdmobManager.Instance.ShowSkinRewardAd();
                GM.Data.Pongs[_Num].AdNum++;
                GM.Data.Pongs[_Num].AdNum_Txt.text = GM.Data.Pongs[_Num].AdNum.ToString() + "/3";

                if (GM.Data.Pongs[_Num].AdNum == 3)
                {
                    Debug.Log("스킨 해제");
                    SkinClear(_Num);
                }
            }
        }
    }

    //#Shop을 On 시킨다.
    public void ShopView_On()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Shop_Panel.SetActive(true);
    }

    //#Shop을 Off 시킨다.
    public void ShopView_Off()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Shop_Panel.SetActive(false);
    }

    //#Wave따라 스킨 해체 된다.
    public void SkinWaveClear()
    {
        float _Wave = GameManager.Instance.Data.BeforeWave;

        //#나선환
        if (_Wave >= SpiralClearWave)
            SkinClear(3);

        //#얼룩말
        if (_Wave >= ZebraClearWave)
            SkinClear(4);

        //#표범
        if (_Wave >= LeopardClearWave)
            SkinClear(5);

        //#쿠키
        if (_Wave >= CookieClearWave)
            SkinClear(6);

        //#농구공
        if (_Wave >= BasketballClearWave)
            SkinClear(7);

        //#펄
        if (_Wave >= PearlClearWave)
            SkinClear(8);

        //#도넛
        if (_Wave >= DonutlClearWave)
            SkinClear(9);

        //#코인
        if (_Wave >= CoinClearWave)
            SkinClear(10);

        //#와플
        if (_Wave >= WafflelClearWave)
            SkinClear(11);

        //#화성
        if (_Wave >= MarsClearWave)
            SkinClear(12);

        //#오렌지
        if (_Wave >= OrangeClearWave)
            SkinClear(13);
    }

    //#Wave따라 상호작용을 한다.
    public void SkinClear(int _Num)
    {
        GM.Data.Pongs[_Num].SkinPong_Img.enabled = true;
        GM.Data.Pongs[_Num].Lock_Img.SetActive(false);
        GM.Data.Pongs[_Num].IsSelect = true;

        HaveSkinNum();
    }

    //#현재 장착하고 있는 스킨이 무엇인지, 얼마큼 해체 했는지
    public void CheckClearSkin()
    {
        for(int i =0; i < GM.Data.Pongs.Count; i++)
        {
            if (GM.Data.Pongs[i].IsSelect == true)
                SkinClear(i);
        }
    }

    //#자신어 몇개의 Skin을 가지고 있는지 확인한다.
    public void HaveSkinNum()
    {
        SkinNum = 0; //#초기화

        for (int i = 0; i < GM.Data.Pongs.Count; i++)
        {
            if(GM.Data.Pongs[i].IsSelect == true)
            {
                SkinNum++;
                SkinHave_Txt.text = SkinNum.ToString() + "/15";
            }
        }
    }

    //#구매를 성공했을 때
    public void CatSkinPurchaseComplete() => SkinClear(14);

    //#구매를 실패했을 때
    public void CatSkinRemovePurchaseFail() => Debug.Log("구매 실패");
}

