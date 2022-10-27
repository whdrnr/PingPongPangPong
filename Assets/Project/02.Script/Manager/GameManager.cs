using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
     public delegate void GameOverDelegate();
     public GameOverDelegate gameOverDelegate;

    [Header("공 생성 관련 참조")]
    public GameObject Pong;
    public Transform Init_Pos;
    public float Speed;

    [Header("데인저 관련 참조")]
    public GameObject Danger;

    [Header("점수 관련 참조")]
    public int MaxWave = 0;
    public int CurWave = 0;
    public int BounceNum = 3; //#튕겨야 하는 횟수

    public bool IsGame;

    //#Wave를 Clear하지 못했을 때
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        
        IsGame = false;

        UIManager.Instance.GameOver_Panel.SetActive(true);

        GameObject CurPong = GameObject.FindWithTag("Pong");
        Destroy(CurPong);
    }

    public void WaveClear()
    {
        if(BounceNum == 0)
        {
            Debug.Log("웨이브 클리어");
        }
    }
}
