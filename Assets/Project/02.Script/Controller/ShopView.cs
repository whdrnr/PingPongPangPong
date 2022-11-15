using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
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
    public bool IsAdSee; //#광고를 볼 수 있는지, 없는지
    public int AdNum;
}

public class ShopView : MonoBehaviour
{
    [Header("상점 관련 참조")]
    public GameObject Shop_Panel;
    public TextMeshProUGUI SkinHave_Txt;
    public IAPButton Cat_Btn;
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

    [Header("SKin별 Img/Sprite 참조 관련")]
    public List<GameObject> PongSelect_Btn = new List<GameObject>();
    public List<Sprite> Skin_Spirte = new List<Sprite>();
    public List<GameObject> Lock_Img = new List<GameObject>();
    public List<Image> SkinPong_Img = new List<Image>();

    public List<TextMeshProUGUI> AdNum_Txt = new List<TextMeshProUGUI>();

    [Header("파티클 관련 참조")]
    public GameObject P_Cat;

    GameManager GM;

    void Start()
    {
        //#Delegate 연결
        GameManager.Instance.gameOverDelegate += SkinWaveClear;

        GM = GameManager.Instance;
        ShopLoad();
    }

    public void PongSelect_Btn_Click(int _Num)
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (GM.Data.Pongs[_Num].IsSelect == true) //#스킨 해체 되어있는 상태
        {
            for (int i = 0; i < GM.Data.Pongs.Count; i++)
            {
                CatSkinNotUse();
                GM.Data.Pongs[i].IsUse = false;
                PongSelect_Btn[i].GetComponent<Image>().sprite = NonSelect_Sprite;
            }

            GM.Data.Pongs[_Num].IsUse = true;

            //#고양이 스킨 장착시
            if (GM.Data.Pongs[15].IsUse == true)
                CatSkinUse();

            PongSelect_Btn[_Num].GetComponent<Image>().sprite = Select_Sprite;
            GM.Pong.GetComponent<SpriteRenderer>().sprite = Skin_Spirte[_Num];
            GM.Data.CurSkin_Num = _Num;
        }
        else //#스킨 해체가 안되어있는 상태
        {
            //#AD Skin인지, 현재 볼 수 있는지
            if(GM.Data.Pongs[_Num].IsAdSkin == true && GM.Data.Pongs[_Num].IsAdSee == true)
            {
                AdmobManager.Instance.ShowSkinRewardAd();

                StartCoroutine(AdSkinTime(_Num));
                GM.Data.Pongs[_Num].AdNum++;
                AdNum_Txt[_Num - 1].text = GM.Data.Pongs[_Num].AdNum.ToString() + "/3";

                if (GM.Data.Pongs[_Num].AdNum == 3)
                    SkinClear(_Num);
            }
        }
    }

    IEnumerator AdSkinTime(int _Num)
    {
        float AdSeeTime = 10;
        GM.Data.Pongs[_Num].IsAdSee = false;

        while (AdSeeTime > 0)
        {
            AdSeeTime -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        Debug.Log($"{GM.Data.Pongs[_Num].Name}은 광고 시청 가능");
        GM.Data.Pongs[_Num].IsAdSee = true;

        yield return null;
    }

    //#Shop을 On 시킨다.
    public void ShopView_On()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Shop_Panel.GetComponent<CanvasGroup>().alpha = 1;
        Shop_Panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //#Shop을 Off 시킨다.
    public void ShopView_Off()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Shop_Panel.GetComponent<CanvasGroup>().alpha = 0;
        Shop_Panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //#Shop UI Load 
    public void ShopLoad()
    {
        for (int i = 0; i < GM.Data.Pongs.Count; i++)
        {
            //#현재 자신이 선택한 스킨 Load
            PongSelect_Btn[i].GetComponent<Image>().sprite = NonSelect_Sprite;
            PongSelect_Btn[GM.Data.CurSkin_Num].GetComponent<Image>().sprite = Select_Sprite;

            GM.Pong.GetComponent<SpriteRenderer>().sprite = SkinPong_Img[GM.Data.CurSkin_Num].sprite;

            //#Wave Skin& Ad Skin
            if (GM.Data.Pongs[i].IsSelect == true)
                SkinClear(i);

            //#Ad Num Txt
            if(GM.Data.Pongs[i].IsAdSkin == true)
                AdNum_Txt[i - 1].text = GM.Data.Pongs[i].AdNum.ToString() + "/3";

            //#고양이 스킨을 구매했다면 스킨 해제
            if (GM.Data.IsCatPurchase == true)
            {
                SkinClear(15);
                Cat_Btn.buttonType = IAPButton.ButtonType.Restore;
            }
        }
    }

    //#Wave따라 스킨 해체 된다.
    public void SkinWaveClear()
    {
        float _Wave = GameManager.Instance.Data.BeforeWave;

        //#나선환
        if (_Wave >= SpiralClearWave)
        {
            SkinClear(4);
        }

        //#얼룩말
        if (_Wave >= ZebraClearWave)
            SkinClear(5);

        //#표범
        if (_Wave >= LeopardClearWave)
            SkinClear(6);

        //#쿠키
        if (_Wave >= CookieClearWave)
            SkinClear(7);

        //#농구공
        if (_Wave >= BasketballClearWave)
            SkinClear(8);

        //#펄
        if (_Wave >= PearlClearWave)
            SkinClear(9);

        //#도넛
        if (_Wave >= DonutlClearWave)
            SkinClear(10);

        //#코인
        if (_Wave >= CoinClearWave)
            SkinClear(11);

        //#와플
        if (_Wave >= WafflelClearWave)
            SkinClear(12);

        //#화성
        if (_Wave >= MarsClearWave)
            SkinClear(13);

        //#오렌지
        if (_Wave >= OrangeClearWave)
            SkinClear(14);
    }

    //#Wave따라 상호작용을 한다.
    public void SkinClear(int _Num)
    {
        SkinPong_Img[_Num].enabled = true;
        Lock_Img[_Num].SetActive(false);
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
                SkinHave_Txt.text = SkinNum.ToString() + "/16";
            }
        }
    }

    //#구매를 성공했을 때
    public void CatSkinPurchaseComplete()
    {
        SkinClear(15);
        Cat_Btn.buttonType = IAPButton.ButtonType.Restore;
    }

    //#구매를 실패했을 때
    public void CatSkinRemovePurchaseFail() => Debug.Log("구매 실패");

    //#고양이 스킨 사용시 파티클 재생
    public void CatSkinUse() => P_Cat.SetActive(true);

    //#고양이 스킨이 아닐 때
    public void CatSkinNotUse() => P_Cat.SetActive(false);
}