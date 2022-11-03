using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Pong
{
    public string Name;
    public bool IsSelect; //#���� ��������?
    public bool IsUse; //#���������?
    public Sprite Skin_Spirte;
}

public class ShopView : MonoBehaviour
{
    [Header("���� ���� ����")]
    public GameObject Shop_Panel;
    public Pong[] Pongs;
    public List<GameObject> PongSelect_Btn = new List<GameObject>();

    [Header("Sprite ���� ����")]
    public Sprite Select_Sprite;
    public Sprite NonSelect_Sprite;

    public void PongSelect_Btn_Click(int _Num)
    {
        for(int i = 0; i < Pongs.Length; i++)
        {
            Pongs[i].IsUse = false;
            PongSelect_Btn[i].GetComponent<Image>().sprite = NonSelect_Sprite;
        }

        Pongs[_Num].IsUse = true;
        PongSelect_Btn[_Num].GetComponent<Image>().sprite = Select_Sprite;
    }

    public void ShopView_On() => Shop_Panel.SetActive(true);

    public void ShopView_Off() => Shop_Panel.SetActive(false);
}

