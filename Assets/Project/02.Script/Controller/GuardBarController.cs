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

    void Start()
    {
        GameManager.Instance.gameOverDelegate += AllResetDurability;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pong"))
        {
            GameManager.Instance.HitGuard();

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

    public void ResetDurability()
    {
        if (IsDestroy == false)
        {
            Durability = 4;
            Durability_Txt.text = Durability.ToString();
        }
    }

    public void AllResetDurability()
    {
        gameObject.SetActive(true);
        Durability = 4;
        Durability_Txt.text = Durability.ToString();
        IsDestroy = true;
    }
}
