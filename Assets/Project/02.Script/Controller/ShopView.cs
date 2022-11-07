using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public struct SkinPong
{
    public string Name;
    public bool IsSelect; //#���� ��������?
    public bool IsUse; //#���������?
    public bool IsAdSkin; //#����Ų����

    [Header("Img/Sprite ���� ����")]
    public Sprite Skin_Spirte;
    public GameObject Lock_Img;
    public Image SkinPong_Img;
}

public class ShopView : MonoBehaviour
{
    [Header("���� ���� ����")]
    public GameObject Shop_Panel;
    public SkinPong[] Pongs;
    public List<GameObject> PongSelect_Btn = new List<GameObject>();
    public TextMeshProUGUI SkinHave_Txt;

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

    void Start()
    {
        GameManager.Instance.gameOverDelegate += SkinWaveClear;
    }

    public void PongSelect_Btn_Click(int _Num)
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (Pongs[_Num].IsSelect == true)
        {
            for (int i = 0; i < Pongs.Length; i++)
            {
                Pongs[i].IsUse = false;
                PongSelect_Btn[i].GetComponent<Image>().sprite = NonSelect_Sprite;
            }

            Pongs[_Num].IsUse = true;
            PongSelect_Btn[_Num].GetComponent<Image>().sprite = Select_Sprite;

            GameManager.Instance.Pong.GetComponent<SpriteRenderer>().sprite =
                Pongs[_Num].Skin_Spirte;
        }
        else
        {
            Debug.Log("������ ���� ��Ų�Դϴ�.");
        }
    }

    public void ShopView_On()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Shop_Panel.SetActive(true);
    }

    public void ShopView_Off()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Shop_Panel.SetActive(false);
    }

    public void SkinWaveClear()
    {
        float _Wave = GameManager.Instance.BeforeWave;

        //#����ȯ
        if (_Wave >= SpiralClearWave)
            SkinClear(3);

        //#��踻
        if (_Wave >= ZebraClearWave)
            SkinClear(4);

        //#ǥ��
        if (_Wave >= LeopardClearWave)
            SkinClear(5);

        //#��Ű
        if (_Wave >= CookieClearWave)
            SkinClear(6);

        //#�󱸰�
        if (_Wave >= BasketballClearWave)
            SkinClear(7);

        //#��
        if (_Wave >= PearlClearWave)
            SkinClear(8);

        //#����
        if (_Wave >= DonutlClearWave)
            SkinClear(9);

        //#����
        if (_Wave >= CoinClearWave)
            SkinClear(10);

        //#����
        if (_Wave >= WafflelClearWave)
            SkinClear(11);

        //#ȭ��
        if (_Wave >= MarsClearWave)
            SkinClear(12);

        //#������
        if (_Wave >= OrangeClearWave)
            SkinClear(13);
    }

    public void SkinClear(int _Num)
    {
        Pongs[_Num].SkinPong_Img.enabled = true;
        Pongs[_Num].Lock_Img.SetActive(false);
        Pongs[_Num].IsSelect = true;
    }
}

