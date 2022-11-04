using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DangerLine"))
        {
            if (GameManager.Instance.IsPause == false)
                StartCoroutine(GameManager.Instance.GameOver());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            //#아이템 삭제
            Destroy(collision.gameObject);

            //#사운드 
            SoundManager.Instance.PlaySFX("ItemGet-SFX", 1);

            //#파티클
            for(int i =0; i < 3; i++)
                GuardBarManager.Instance.P_Guard_Item_Get[i].Play();

            //#아이템 능력
            GameObject[] Guard = GameObject.FindGameObjectsWithTag("Guard");

            for (int i = 0; i < Guard.Length; i++)
            {
                Guard[i].GetComponent<GuardBarController>().ResetDurability();
            }

            //#아이템 생성
            ItemInitManager.Instance.ItemInit(0.5f);
        }
    }
}
