using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
     public delegate void WaveClearDelegate();
     public WaveClearDelegate waveClearDelegate;

    public delegate void GameOverDelegate();
    public GameOverDelegate gameOverDelegate;

    public delegate void GameStartDelegate();
    public GameStartDelegate gameStartDelegate;

    public delegate void GameReStartDelegate();
    public GameReStartDelegate gameReStartDelegate;

    [Header("공 관련 참조")]
    public GameObject Pong;
    public Transform Init_Pos;
    public float Speed = 3;

    [Header("웨이브 관련 참조")]
    public int MaxWave = 0;
    public int BeforeWave = 0;
    public int CurWave = 1;
    public int BounceNum = 3; //#튕겨야 하는 횟수

    [Header("Bool 관련 참조")]
    public bool IsGame; //#현재 게임중인지
    public bool IsPause; //#게임중에 일시정지 상태인지
    public bool IsAdSee; //#죽은 뒤에 광고를 시청하였는지

    [Header("죽엇을 떄 관련 참조")]
    public Image CountBar_Img;
    public float MaxDieTime = 8;
    public float CurDieTime;

    void Start()
    {
        //#Delegate 함수 연결
        waveClearDelegate += ObjectSetting;
        gameOverDelegate += ObjectSetting;
        gameReStartDelegate += ObjectSetting;
        gameStartDelegate += StartBall;

        //#배경음
        SoundManager.Instance.PlayBGM("BG1", 1);
    }

    void ObjectSetting()
    {
        if (Pong.activeSelf == false) //#공이 비활성화 상태일 때
        {
            Pong.SetActive(true);
        }
        else //#공이 활성화 상태일 때
        {
            StopBall();
            Pong.transform.position = new Vector3(0, 0.5f, 0);
        }
    }

    //#게임 시작 시 퐁을 아래로 운동한다.
    public void StartBall()
    {
        Pong.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Pong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
    }

    //#공을 멈춘다,
    public void StopBall() => Pong.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    //#부활 이후
    public void ReStartBall()
    {
        IsAdSee = true;
        IsGame = true;
        UIManager.Instance.AD_Panel.SetActive(false);

        gameReStartDelegate();

        Invoke("StartBall", 1.5f);
    }

    //#가드바에 퐁이 닿았을 때 내구도가 줄어든다.
    public void WaveBounce()
    {
        if (BounceNum == 1)
        {
            StartCoroutine(WaveClear());
        }
        else
        {
            BounceNum--;
            UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();
        }
    }

    //#Wave를 Clear하지 못했을 때
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);

        IsGame = false;

        //#신기록을 달성했는지, 안했는지
        BeforeWave = CurWave;

        if(BeforeWave > MaxWave)
            MaxWave = CurWave;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX("GameOver-SFX", 1);

        if (IsAdSee == false) //#부활 광고 안보았다면
        {
            UIManager.Instance.AD_Panel.SetActive(true);
            StartCoroutine(DieCountTime());
        }
        else
        {
            UIManager.Instance.Die_Panel.SetActive(true);
        }
    }

    //#User가 죽었을 때 
    public IEnumerator DieCountTime()
    {
        CurDieTime = MaxDieTime;

        while(CurDieTime > 0)
        {
            CurDieTime -= Time.deltaTime;
            CountBar_Img.fillAmount = CurDieTime / MaxDieTime;

            yield return new WaitForFixedUpdate();
        }

        CurDieTime = MaxDieTime;
    }

    //#Wave를 Clear 했을 때
    IEnumerator WaveClear()
    {
        IsPause = true;

        waveClearDelegate();
        SoundManager.Instance.PlaySFX("Rest-SFX", 1);

        //#카운트 다운
        UIManager.Instance.CurBounce_Txt.text = "3";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "2";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "1";
        yield return new WaitForSeconds(1f);

        //#웨이브 증가 
        CurWave++;
        UIManager.Instance.Wave_Txt.text = CurWave.ToString();

        //#튕겨야 하는 횟수 증가
        BounceNum = 2 + CurWave;
        UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();

        IsPause = false;

       gameStartDelegate();
    }
}
