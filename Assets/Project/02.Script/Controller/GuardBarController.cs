using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardBarController : MonoBehaviour
{
    public int Durability = 4;

    public TextMeshProUGUI Durability_Txt;

    void Start()
    {
        GameManager.Instance.gameOverDelegate += ResetDurability;
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
                Debug.Log("가드바 파괴됨");
            }
        }
    }

    public void ResetDurability()
    {
        gameObject.SetActive(true);
        Durability = 4;
        Durability_Txt.text = Durability.ToString();
    }
}
