using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SkinPong
{
    public string Name;
    public bool IsSelect; //#���� ��������?
    public bool IsUse; //#���������?

    [Header("Img/Sprite ���� ����")]
    public Sprite Skin_Spirte;
    public Image Lock_Img;
    public Image SkinPong_Img;
}

public class ShopView : MonoBehaviour
{
    [Header("���� ���� ����")]
    public GameObject Shop_Panel;
    public SkinPong[] Pongs;
    public List<GameObject> PongSelect_Btn = new List<GameObject>();

    [Header("Sprite ���� ����")]
    public Sprite Select_Sprite;
    public Sprite NonSelect_Sprite;

    [Header("��Ų�� ���̺� ������� ���̵� ���� ����")]
    public int SpiralClearWave = 15;
    public int ZebraClearWave = 15;
    public int LeopardClearWave = 15;
    public int CookieClearWave = 15;
    public int BasketballClearWave = 15;
    public int PearlClearWave = 15;
    public int DonutlClearWave = 15;
    public int CoinClearWave = 15;
    public int WafflelClearWave = 15;
    public int MarsClearWave = 15;
    public int OrangeClearWave = 15;

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

    public void SkinWaveClear(string _Name)
    {
        switch(_Name)
        {
            case "SpiralPong":
                break;
        }
    }
}

