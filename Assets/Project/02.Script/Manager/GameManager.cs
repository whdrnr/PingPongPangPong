using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct WaveType
{
    public string Name;
    public int ChangeWave;
    public float PongSpeed;
    public Sprite BG_Sprite;
}

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
    public WaveType[] Waves;
    public SpriteRenderer BG_SR;
    public int CurWave = 1;
    public int BounceNum = 3; //#튕겨야 하는 횟수

    [Header("Bool 관련 참조")]
    public bool IsGame; //#현재 게임중인지
    public bool IsPause; //#게임중에 일시정지 상태인지
    public bool IsAdSee; //#죽은 뒤에 광고를 시청하였는지
    public bool IsAdParchase; //#광고를 구매하였는지
    public bool IsCountDown;

    [Header("죽엇을 떄 관련 참조")]
    public Image CountBar_Img;
    public float MaxDieTime = 8;
    public float CurDieTime;

    public Data Data;

    public int ClickBackCount = 0;

    void Start()
    {
        //#Delegate 함수 연결
        waveClearDelegate += ObjectSetting;

        gameOverDelegate += ObjectSetting;
        gameOverDelegate += ResetWaveBG;

        gameReStartDelegate += ObjectSetting;

        gameStartDelegate += StartBall;

        //#배경음
        SoundManager.Instance.PlayBGM("BG1", 1);
    }

    void Update()
    {
        //#돌아가기 두번 클릭시 게임이 종료된다.
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClickBackCount++;
                if (!IsInvoking("ResetDoubleClick"))
                    Invoke("ResetDoubleClick", 1.0f);

            }
            else if (ClickBackCount == 2)
            {
                CancelInvoke("ResetDoubleClick");
                ResetDoubleClick();
                JsonManager.Instance.SaveGameData();
                Application.Quit();
            }
        }
    }

    void ResetDoubleClick()  => ClickBackCount = 0;

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
            StartCoroutine(IEWaveClear());
        }
        else
        {
            BounceNum--;
            UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();
        }
    }

    //#Wave따라 BG가 변한다.
    public void WaveBG(int _CurWave)
    {
        //#검은 산
        if (_CurWave >= Waves[0].ChangeWave && _CurWave <= Waves[1].ChangeWave)
        {
            BG_SR.sprite = Waves[0].BG_Sprite;
            Speed = Waves[0].PongSpeed;
        }

        //#가시 덤블
        else if (_CurWave >= Waves[1].ChangeWave && _CurWave <= Waves[2].ChangeWave)
        {
            BG_SR.sprite = Waves[1].BG_Sprite;
            Speed = Waves[1].PongSpeed;

            if (SoundManager.Instance.BGMPlayer.clip.name != "BG2")
                SoundManager.Instance.PlayBGM("BG2", 1);
        }

        //#사막
        else if (_CurWave >= Waves[2].ChangeWave && _CurWave <= Waves[3].ChangeWave)
        {
            BG_SR.sprite = Waves[2].BG_Sprite;
            Speed = Waves[2].PongSpeed;

            if (SoundManager.Instance.BGMPlayer.clip.name != "BG3")
                SoundManager.Instance.PlayBGM("BG3", 1);
        }

        //#늪지
        else if (_CurWave >= Waves[3].ChangeWave)
        {
            BG_SR.sprite = Waves[3].BG_Sprite;
            Speed = Waves[3].PongSpeed;

            if (SoundManager.Instance.BGMPlayer.clip.name != "BG4")
                SoundManager.Instance.PlayBGM("BG4", 1);
        }
    }

    //#죽었을 때 배경은 "검은 산"으로 돌아온다.
    public void ResetWaveBG()
    {
        BG_SR.sprite = Waves[0].BG_Sprite;
        Speed = Waves[0].PongSpeed;
        SoundManager.Instance.PlayBGM("BG1", 1);
    }

    //#Wave를 Clear하지 못했을 때
    public IEnumerator IEGameOver()
    {
        yield return new WaitForSeconds(1f);

        IsGame = false;

        //#신기록을 달성했는지, 안했는지
        Data.BeforeWave = CurWave;

        if(Data.BeforeWave > Data.MaxWave)
            Data.MaxWave = CurWave;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX("GameOver-SFX", 1);

        if (IsAdSee == false && CurWave >= 5) //#부활 광고 안보았다면
        {
            UIManager.Instance.AD_Panel.SetActive(true);
            StartCoroutine(IEDieCountTime());
        }
        else
        {
            UIManager.Instance.Die_Panel.SetActive(true);
        }
    }

    //#User가 죽었을 때 
    public IEnumerator IEDieCountTime()
    {
        CurDieTime = MaxDieTime;
        IsCountDown = true;

        while(CurDieTime > 0 && IsCountDown == true)
        {
            CurDieTime -= Time.deltaTime;
            CountBar_Img.fillAmount = CurDieTime / MaxDieTime;

            yield return null;
        }

        if(IsCountDown == true)
            UIManager.Instance.GmaeOver_Btn();

        CurDieTime = MaxDieTime;
    }

    //#Wave를 Clear 했을 때
    IEnumerator IEWaveClear()
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
        WaveBG(CurWave);

        //#튕겨야 하는 횟수 증가
        BounceNum = 2 + CurWave;
        UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();

        IsPause = false;

       gameStartDelegate();
    }

    void OnApplicationQuit()
    {
        JsonManager.Instance.SaveGameData();
    }

    void OnApplicationPause(bool _Pause)
    {
        if(_Pause == true)
            OnApplicationQuit();
    }
}

[System.Serializable]
public class Data
{
    public int MaxWave;
    public int BeforeWave;

    [Header("설정 bool 참조")]
    public bool IsBGMOn = true;
    public bool IsSFXOn = true;
    public bool IsVibrationOn = true;

    public List<SkinPong> Pongs = new List<SkinPong>();
}