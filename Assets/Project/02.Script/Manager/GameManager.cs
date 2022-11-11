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

    [Header("�� ���� ����")]
    public GameObject Pong;
    public Transform Init_Pos;
    public float Speed = 3;

    [Header("���̺� ���� ����")]
    public WaveType[] Waves;
    public SpriteRenderer BG_SR;
    public int CurWave = 1;
    public int BounceNum = 3; //#ƨ�ܾ� �ϴ� Ƚ��

    [Header("Bool ���� ����")]
    public bool IsGame; //#���� ����������
    public bool IsPause; //#�����߿� �Ͻ����� ��������
    public bool IsAdSee; //#���� �ڿ� ���� ��û�Ͽ�����
    public bool IsAdParchase; //#���� �����Ͽ�����
    public bool IsCountDown;

    [Header("�׾��� �� ���� ����")]
    public Image CountBar_Img;
    public float MaxDieTime = 8;
    public float CurDieTime;

    public Data Data;

    public int ClickBackCount = 0;

    void Start()
    {
        //#Delegate �Լ� ����
        waveClearDelegate += ObjectSetting;

        gameOverDelegate += ObjectSetting;
        gameOverDelegate += ResetWaveBG;

        gameReStartDelegate += ObjectSetting;

        gameStartDelegate += StartBall;

        //#�����
        SoundManager.Instance.PlayBGM("BG1", 1);
    }

    void Update()
    {
        //#���ư��� �ι� Ŭ���� ������ ����ȴ�.
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
        if (Pong.activeSelf == false) //#���� ��Ȱ��ȭ ������ ��
        {
            Pong.SetActive(true);
        }
        else //#���� Ȱ��ȭ ������ ��
        {
            StopBall();
            Pong.transform.position = new Vector3(0, 0.5f, 0);
        }
    }

    //#���� ���� �� ���� �Ʒ��� ��Ѵ�.
    public void StartBall()
    {
        Pong.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Pong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
    }

    //#���� �����,
    public void StopBall() => Pong.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    //#��Ȱ ����
    public void ReStartBall()
    {
        IsAdSee = true;
        IsGame = true;
        UIManager.Instance.AD_Panel.SetActive(false);

        gameReStartDelegate();

        Invoke("StartBall", 1.5f);
    }

    //#����ٿ� ���� ����� �� �������� �پ���.
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

    //#Wave���� BG�� ���Ѵ�.
    public void WaveBG(int _CurWave)
    {
        //#���� ��
        if (_CurWave >= Waves[0].ChangeWave && _CurWave <= Waves[1].ChangeWave)
        {
            BG_SR.sprite = Waves[0].BG_Sprite;
            Speed = Waves[0].PongSpeed;
        }

        //#���� ����
        else if (_CurWave >= Waves[1].ChangeWave && _CurWave <= Waves[2].ChangeWave)
        {
            BG_SR.sprite = Waves[1].BG_Sprite;
            Speed = Waves[1].PongSpeed;

            if (SoundManager.Instance.BGMPlayer.clip.name != "BG2")
                SoundManager.Instance.PlayBGM("BG2", 1);
        }

        //#�縷
        else if (_CurWave >= Waves[2].ChangeWave && _CurWave <= Waves[3].ChangeWave)
        {
            BG_SR.sprite = Waves[2].BG_Sprite;
            Speed = Waves[2].PongSpeed;

            if (SoundManager.Instance.BGMPlayer.clip.name != "BG3")
                SoundManager.Instance.PlayBGM("BG3", 1);
        }

        //#����
        else if (_CurWave >= Waves[3].ChangeWave)
        {
            BG_SR.sprite = Waves[3].BG_Sprite;
            Speed = Waves[3].PongSpeed;

            if (SoundManager.Instance.BGMPlayer.clip.name != "BG4")
                SoundManager.Instance.PlayBGM("BG4", 1);
        }
    }

    //#�׾��� �� ����� "���� ��"���� ���ƿ´�.
    public void ResetWaveBG()
    {
        BG_SR.sprite = Waves[0].BG_Sprite;
        Speed = Waves[0].PongSpeed;
        SoundManager.Instance.PlayBGM("BG1", 1);
    }

    //#Wave�� Clear���� ������ ��
    public IEnumerator IEGameOver()
    {
        yield return new WaitForSeconds(1f);

        IsGame = false;

        //#�ű���� �޼��ߴ���, ���ߴ���
        Data.BeforeWave = CurWave;

        if(Data.BeforeWave > Data.MaxWave)
            Data.MaxWave = CurWave;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX("GameOver-SFX", 1);

        if (IsAdSee == false && CurWave >= 5) //#��Ȱ ���� �Ⱥ��Ҵٸ�
        {
            UIManager.Instance.AD_Panel.SetActive(true);
            StartCoroutine(IEDieCountTime());
        }
        else
        {
            UIManager.Instance.Die_Panel.SetActive(true);
        }
    }

    //#User�� �׾��� �� 
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

    //#Wave�� Clear ���� ��
    IEnumerator IEWaveClear()
    {
        IsPause = true;

        waveClearDelegate();
        SoundManager.Instance.PlaySFX("Rest-SFX", 1);

        //#ī��Ʈ �ٿ�
        UIManager.Instance.CurBounce_Txt.text = "3";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "2";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "1";
        yield return new WaitForSeconds(1f);

        //#���̺� ���� 
        CurWave++;
        UIManager.Instance.Wave_Txt.text = CurWave.ToString();
        WaveBG(CurWave);

        //#ƨ�ܾ� �ϴ� Ƚ�� ����
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

    [Header("���� bool ����")]
    public bool IsBGMOn = true;
    public bool IsSFXOn = true;
    public bool IsVibrationOn = true;

    public List<SkinPong> Pongs = new List<SkinPong>();
}