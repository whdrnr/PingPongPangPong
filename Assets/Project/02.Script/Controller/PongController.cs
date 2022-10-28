using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DangerLine"))
            StartCoroutine(GameManager.Instance.GameOver());

        if(collision.CompareTag("Item"))
        {
            GameObject[] Guard = GameObject.FindGameObjectsWithTag("Guard");

            for(int i =0; i < Guard.Length; i++)
            {
                Guard[i].GetComponent<GuardBarController>().ResetDurability();
            }
        }
    }
}
