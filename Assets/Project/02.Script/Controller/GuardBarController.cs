using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardBarController : MonoBehaviour
{
    [Header("퐁 관련 참조")]
    public int PongNum;
    public bool IsDestroy = false;

    [Header("내구도 관련 참조")]
    public int Durability = 4;
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
            //#가드바 상호작용
            GuardBarManager.Instance.DurabilityGuard(Durability, BoxCollider2D, SR);
            GuardBarManager.Instance.P_Guard_Hit[PongNum].Play();
            GM.WaveBounce();

            Durability -= 1;
            Durability_Txt.text = Durability.ToString();

            if(Durability == 0)
            {
                GuardBarManager.Instance.P_Guard_Destory[PongNum].Play();
                SoundManager.Instance.PlaySFX("GuardBleak-SFX", 1);
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
            SR.sprite = GuardBarManager.Instance.Guard1;
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
        SR.sprite = GuardBarManager.Instance.Guard1;

        IsDestroy = false;
    }
}
