using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardBarController : MonoBehaviour
{
    public int Durability = 4;

    public TextMeshProUGUI Durability_Txt;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pong"))
        {
            GameManager.Instance.BounceNum--;
            UIManager.Instance.CurBounce_Txt.text = GameManager.Instance.BounceNum.ToString();

            //#가드바 상호작용
            Durability -= 1;
            Durability_Txt.text = Durability.ToString();

            if(Durability == 0)
            {
                GuardDestory();
            }
        }
    }

    public void ResetDurability()
    {
        gameObject.SetActive(true);
        Durability = 4;
        Durability_Txt.text = Durability.ToString();
    }
     
    void GuardDestory()
    {
        gameObject.SetActive(false);
    }
}
