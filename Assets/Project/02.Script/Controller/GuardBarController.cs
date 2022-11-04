using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardBarController : MonoBehaviour
{
    [Header("�� ���� ����")]
    public int PongNum;
    public bool IsDestroy = false;

    [Header("������ ���� ����")]
    public int Durability = 7;
    public int MaxDurability = 7;
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
            //#����� ��ȣ�ۿ�
            GuardBarManager.Instance.DurabilityGuard(Durability, BoxCollider2D, SR);
            GuardBarManager.Instance.P_Guard_Hit[PongNum].Play();
            SoundManager.Instance.PlaySFX("GuardHit-SFX", 1);
            GM.WaveBounce();

            Durability -= 1;
            Durability_Txt.text = Durability.ToString();

            if(Durability == 0)
            {
                SoundManager.Instance.PlaySFX("GuardBleak-SFX", 1);
                GuardBarManager.Instance.P_Guard_Destory[PongNum].Play();
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
            Durability = MaxDurability;
            Durability_Txt.text = Durability.ToString();

            //#Guard �̹��� ��ü
            SR.sprite = GuardBarManager.Instance.Guard1;
        }
    }

    //#��� ������� �������� ȸ���Ѵ�.
    public void AllResetDurability()
    {
        gameObject.SetActive(true);

        //#������ ȸ��
        Durability = MaxDurability;
        Durability_Txt.text = Durability.ToString();
        
        //#Guard �̹��� ��ü
        SR.sprite = GuardBarManager.Instance.Guard1;

        IsDestroy = false;
    }
}
