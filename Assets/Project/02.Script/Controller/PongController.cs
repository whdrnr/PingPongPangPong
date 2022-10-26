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
        }
    }
}
