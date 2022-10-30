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
        //#캐싱
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
        GM = GameManager.Instance;

        //#Delegate 함수 연결
        GM.gameOverDelegate += AllResetDurability;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pong"))
        {
            GM.DurabilityGuard(Durability, BoxCollider2D, SR);
            GM.WaveBounce();

            //#가드바 상호작용
            Durability -= 1;
            Durability_Txt.text = Durability.ToString();

            if(Durability == 0)
            {
                gameObject.SetActive(false);
                IsDestroy = true;
            }
        }
    }

    //#파괴된 가드바를 제외한 가드바의 내구도를 회복한다.
    public void ResetDurability()
    {
        if (IsDestroy == false)
        {
            //#내구도 회복
            Durability = 4;
            Durability_Txt.text = Durability.ToString();

            //#Guard 이미지 교체
            SR.sprite = GM.Guard1;
        }
    }

    //#모든 가드바의 내구도를 회복한다.
    public void AllResetDurability()
    {
        gameObject.SetActive(true);

        //#내구도 회복
        Durability = 4;
        Durability_Txt.text = Durability.ToString();
        
        //#Guard 이미지 교체
        SR.sprite = GM.Guard1;

        IsDestroy = false;
    }
}
