using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardBarController : MonoBehaviour
{
    public int Durability = 4;
    public bool IsDestroy = false;

    public TextMeshProUGUI Durability_Txt;

    BoxCollider2D BoxCollider2D;
    SpriteRenderer SR;
    GameManager GM;

    void Start()
    {
        //#ĳ��
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
        GM = GameManager.Instance;

        //#Delegate �Լ� ����
        GM.gameOverDelegate += AllResetDurability;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pong"))
        {
            GM.DurabilityGuard(Durability, BoxCollider2D, SR);
            GM.WaveBounce();

            //#����� ��ȣ�ۿ�
            Durability -= 1;
            Durability_Txt.text = Durability.ToString();

            if(Durability == 0)
            {
                gameObject.SetActive(false);
                IsDestroy = true;
            }
        }
    }

    //#�ı��� ����ٸ� ������ ������� �������� ȸ���Ѵ�.
    public void ResetDurability()
    {
        if (IsDestroy == false)
        {
            //#������ ȸ��
            Durability = 4;
            Durability_Txt.text = Durability.ToString();

            //#Guard �̹��� ��ü
            SR.sprite = GM.Guard1;
        }
    }

    //#��� ������� �������� ȸ���Ѵ�.
    public void AllResetDurability()
    {
        gameObject.SetActive(true);

        //#������ ȸ��
        Durability = 4;
        Durability_Txt.text = Durability.ToString();
        
        //#Guard �̹��� ��ü
        SR.sprite = GM.Guard1;

        IsDestroy = false;
    }
}
