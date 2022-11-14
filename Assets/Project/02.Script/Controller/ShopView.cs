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
    public bool IsSelect; //#���� ��������?
    public bool IsUse; //#���������?

    [Header("���� ��Ų�� ���")]
    public bool IsAdSkin; //#����Ų����
    public bool IsAdSee; //#���� �� �� �ִ���, ������
    public int AdNum;
    public TextMeshProUGUI AdNum_Txt;

    [Header("Img/Sprite ���� ����")]
    public Sprite Skin_Spirte;
    public GameObject Lock_Img;
    public Image SkinPong_Img;
}

public class ShopView : MonoBehaviour
{
    [Header("���� ���� ����")]
    public GameObject Shop_Panel;
    public List<GameObject> PongSelect_Btn = new List<GameObject>();
    public TextMeshProUGUI SkinHave_Txt;
    public IAPButton Cat_Btn;
    public int SkinNum = 0;

    [Header("Sprite ���� ����")]
    public Sprite Select_Sprite;
    public Sprite NonSelect_Sprite;

    [Header("��Ų�� ���̺� ������� ���̵� ���� ����")]
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

    [Header("��ƼŬ ���� ����")]
    public GameObject P_Cat;

    GameManager GM;

    void Start()
    {
        //#Delegate ����
        GameManager.Instance.gameOverDelegate += SkinWaveClear;

        GM = GameManager.Instance;
    }

    public void PongSelect_Btn_Click(int _Num)
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (GM.Data.Pongs[_Num].IsSelect == true) //#��Ų ��ü �Ǿ��ִ� ����
        {
            for (int i = 0; i < GM.Data.Pongs.Count; i++)
            {
                CatSkinNotUse();
                GM.Data.Pongs[i].IsUse = false;
                PongSelect_Btn[i].GetComponent<Image>().sprite = NonSelect_Sprite;
            }

            GM.Data.Pongs[_Num].IsUse = true;
            PongSelect_Btn[_Num].GetComponent<Image>().sprite = Select_Sprite;

            GameManager.Instance.Pong.GetComponent<SpriteRenderer>().sprite =
                GM.Data.Pongs[_Num].Skin_Spirte;

            //#����� ��Ų ������
            if (GM.Data.Pongs[15].IsUse == true)
                CatSkinUse();
        }
        else //#��Ų ��ü�� �ȵǾ��ִ� ����
        {
            //#AD Skin����, ���� �� �� �ִ���
            if(GM.Data.Pongs[_Num].IsAdSkin == true && GM.Data.Pongs[_Num].IsAdSee == true)
            {
                AdmobManager.Instance.ShowSkinRewardAd();

                GM.Data.Pongs[_Num].AdNum++;
                GM.Data.Pongs[_Num].AdNum_Txt.text = GM.Data.Pongs[_Num].AdNum.ToString() + "/3";

                if (GM.Data.Pongs[_Num].AdNum == 3)
                {
                    Debug.Log("��Ų ����");
                    SkinClear(_Num);
                }
            }
        }
    }

    //#Shop�� On ��Ų��.
    public void ShopView_On()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        ShopLoad();
        Shop_Panel.GetComponent<CanvasGroup>().alpha = 1;
        Shop_Panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //#Shop�� Off ��Ų��.
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
            if (GM.Data.Pongs[i].IsSelect == true)
                SkinClear(i);
        }
    }

    //#Wave���� ��Ų ��ü �ȴ�.
    public void SkinWaveClear()
    {
        float _Wave = GameManager.Instance.Data.BeforeWave;
        Debug.Log(_Wave);

        //#����ȯ
        if (_Wave >= SpiralClearWave)
        {
            SkinClear(4);
        }

        //#��踻
        if (_Wave >= ZebraClearWave)
            SkinClear(5);

        //#ǥ��
        if (_Wave >= LeopardClearWave)
            SkinClear(6);

        //#��Ű
        if (_Wave >= CookieClearWave)
            SkinClear(7);

        //#�󱸰�
        if (_Wave >= BasketballClearWave)
            SkinClear(8);

        //#��
        if (_Wave >= PearlClearWave)
            SkinClear(9);

        //#����
        if (_Wave >= DonutlClearWave)
            SkinClear(10);

        //#����
        if (_Wave >= CoinClearWave)
            SkinClear(11);

        //#����
        if (_Wave >= WafflelClearWave)
            SkinClear(12);

        //#ȭ��
        if (_Wave >= MarsClearWave)
            SkinClear(13);

        //#������
        if (_Wave >= OrangeClearWave)
            SkinClear(14);
    }

    //#Wave���� ��ȣ�ۿ��� �Ѵ�.
    public void SkinClear(int _Num)
    {
        GM.Data.Pongs[_Num].SkinPong_Img.enabled = true;
        GM.Data.Pongs[_Num].Lock_Img.SetActive(false);
        GM.Data.Pongs[_Num].IsSelect = true;

        HaveSkinNum();
    }

    //#���� �����ϰ� �ִ� ��Ų�� ��������, ��ŭ ��ü �ߴ���
    public void CheckClearSkin()
    {
        for(int i =0; i < GM.Data.Pongs.Count; i++)
        {
            if (GM.Data.Pongs[i].IsSelect == true)
                SkinClear(i);
        }
    }

    //#�ڽž� ��� Skin�� ������ �ִ��� Ȯ���Ѵ�.
    public void HaveSkinNum()
    {
        SkinNum = 0; //#�ʱ�ȭ

        for (int i = 0; i < GM.Data.Pongs.Count; i++)
        {
            if(GM.Data.Pongs[i].IsSelect == true)
            {
                SkinNum++;
                SkinHave_Txt.text = SkinNum.ToString() + "/16";
            }
        }
    }

    //#���Ÿ� �������� ��
    public void CatSkinPurchaseComplete()
    {
        SkinClear(15);
        Cat_Btn.buttonType = IAPButton.ButtonType.Restore;
    }

    //#���Ÿ� �������� ��
    public void CatSkinRemovePurchaseFail() => Debug.Log("���� ����");

    //#����� ��Ų ���� ��ƼŬ ���
    public void CatSkinUse() => P_Cat.SetActive(true);

    //#����� ��Ų�� �ƴ� ��
    public void CatSkinNotUse() => P_Cat.SetActive(false);
}