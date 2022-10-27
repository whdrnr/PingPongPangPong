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
    public int CurWave = 1;
    public int BounceNum = 3; //#튕겨야 하는 횟수

    public bool IsGame;
    public bool IsPause;

    //#Wave를 Clear하지 못했을 때
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        
        IsGame = false;
        BounceNum = 3;

        UIManager.Instance.GameOver_Panel.SetActive(true);

        GameObject CurPong = GameObject.FindWithTag("Pong");
        Destroy(CurPong);
    }

    public void WaveClear()
    {
        if(BounceNum == 0)
        {
            StartCoroutine(CountDown());

            //#데인저의 각도를 0으로 되돌린다.
            Danger.transform.rotation = new Quaternion(0, 0, 0, 0);

            GameObject CurPong = GameObject.FindWithTag("Pong");
            Destroy(CurPong);
        }
    }

    IEnumerator CountDown()
    {
        IsPause = true;

        UIManager.Instance.CurBounce_Txt.text = "3";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "2";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "1";
        yield return new WaitForSeconds(1f);

        IsPause = false;

        //#웨이브 증가 
        CurWave++;
        UIManager.Instance.Wave_Txt.text = CurWave.ToString();

        //#튕겨야 하는 횟수 증가
        BounceNum = 2 + CurWave;
        UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();
    }
}
