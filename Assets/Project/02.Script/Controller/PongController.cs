using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("DangerLine"))
            StartCoroutine(GameOver());

        if(collision.CompareTag("Item"))
        {
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("게임 종료");
        GameManager.Instance.IsGame = false;
        UIManager.Show<MainMenuView>();

        Destroy(this.gameObject);
    }
}
