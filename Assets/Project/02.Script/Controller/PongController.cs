using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DangerLine"))
            StartCoroutine(GameManager.Instance.GameOver());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            //#아이템 삭제
            Destroy(collision.gameObject);
            SoundManager.Instance.PlaySFX("ItemGet-SFX", 1);

            //#아이템 능력
            GameObject[] Guard = GameObject.FindGameObjectsWithTag("Guard");

            for (int i = 0; i < Guard.Length; i++)
            {
                Guard[i].GetComponent<GuardBarController>().ResetDurability();
            }

            //#아이템 생성
            ItemInitManager.Instance.ItemInit();
        }
    }
}
