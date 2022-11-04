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
            //#������ ����
            Destroy(collision.gameObject);

            //#���� 
            SoundManager.Instance.PlaySFX("ItemGet-SFX", 1);

            //#��ƼŬ
            for(int i =0; i < 3; i++)
                GuardBarManager.Instance.P_Guard_Item_Get[i].Play();

            //#������ �ɷ�
            GameObject[] Guard = GameObject.FindGameObjectsWithTag("Guard");

            for (int i = 0; i < Guard.Length; i++)
            {
                Guard[i].GetComponent<GuardBarController>().ResetDurability();
            }

            //#������ ����
            ItemInitManager.Instance.ItemInit(0.5f);
        }
    }
}
