using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
     public delegate void WaveClearDelegate();
     public WaveClearDelegate waveClearDelegate;

    public delegate void GameOverDelegate();
    public GameOverDelegate gameOverDelegate;

    public delegate void GamePauseDelegate();
    public GamePauseDelegate gamePauseDelegate;

    [Header("공 생성 관련 참조")]
    public GameObject Pong_Prefeb;
    public Transform Init_Pos;
    public float Speed;

    [Header("데인저 관련 참조")]
    public GameObject Danger;

    [Header("웨이브 관련 참조")]
    public int MaxWave = 0;
    public int CurWave = 1;
    public int BeforeWave = 0;
    public int BounceNum = 3; //#튕겨야 하는 횟수

    [Header("가드바 내구도 관련 참조")]
    public Sprite Guard4;
    public Sprite Guard3;
    public Sprite Guard2;
    public Sprite Guard1;

    [Header("Bool 관련 참조")]
    public bool IsGame;
    public bool IsPause;

    void Start()
    {
        //#Delegate 함수 연결
        waveClearDelegate += ObjectSetting;
        gameOverDelegate += ObjectSetting;
    }

    void ObjectSetting()
    {
        //#데인저의 각도를 0으로 되돌린다.
        Danger.transform.rotation = new Quaternion(0, 0, 0, 0);

        //#퐁 생성
        GameObject CurPong = GameObject.FindWithTag("Pong");

        if (CurPong != null)
        {
            Destroy(CurPong);
            Instantiate(Pong_Prefeb, Init_Pos.position, Quaternion.identity);
        }
        else
            Instantiate(Pong_Prefeb, Init_Pos.position, Quaternion.identity);
    }

    //#게임 시작 시 퐁을 아래로 운동한다.
    public void StartBall()
    {
        IsGame = true;

        GameObject CurPong = GameObject.FindWithTag("Pong");
        CurPong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
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

    //#Guard에 닿을 때마다 내구도 감소 및 이미지 변경
    public void DurabilityGuard(int _Durability, BoxCollider2D _BoxCollider2D, SpriteRenderer _Guard)
    {
          switch (_Durability)
           {
                case 4:
                    _BoxCollider2D.size = new Vector2(2.2f, 0.8f);
                    _Guard.sprite = Guard1;
                    break;

                case 3:
                    _BoxCollider2D.size = new Vector2(1.9f, 0.8f);
                    _Guard.sprite = Guard2;
                    break;

                case 2:
                    _BoxCollider2D.size = new Vector2(1.45f, 0.8f);
                    _Guard.sprite = Guard3;
                    break;

                case 1:
                    _BoxCollider2D.size = new Vector2(1.1f, 0.8f);
                    _Guard.sprite = Guard4;
                     break;
           }
    }

    //#Wave를 Clear하지 못했을 때
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);

        BeforeWave = CurWave;

        if(BeforeWave > MaxWave)
            MaxWave = CurWave;

        IsGame = false;

        UIManager.Instance.GameOver_Panel.SetActive(true);
    }

    //#Wave를 Clear 했을 때
    IEnumerator WaveClear()
    {
        IsPause = true;

        waveClearDelegate();

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

        StartBall();
    }
}
  