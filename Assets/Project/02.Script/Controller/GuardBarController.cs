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
            GameManager.Instance.Cur_Socre += 1;
            UIManager.Instance.PointUp();

            Durability -= 1;
            Durability_Txt.text = Durability.ToString();

            if(Durability == 0)
            {
                GuardDestory();
            }
        }
    }

    public void ResetDurability() => Durability = 4;

    void GuardDestory()
    {
        Destroy(this.gameObject);
    }
}
