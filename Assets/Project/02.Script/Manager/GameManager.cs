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

    public delegate void GameStartDelegate();
    public GameStartDelegate gameStartDelegate;

    [Header("�� ���� ����")]
    public GameObject Pong;
    public Transform Init_Pos;
    public float Speed = 3;

    [Header("���̺� ���� ����")]
    public int MaxWave = 0;
    public int BeforeWave = 0;
    public int CurWave = 1;
    public int BounceNum = 3; //#ƨ�ܾ� �ϴ� Ƚ��

    [Header("Bool ���� ����")]
    public bool IsGame; //#���� ����������
    public bool IsPause; //#�����߿� �Ͻ����� ��������

    void Start()
    {
        //#Delegate �Լ� ����
        waveClearDelegate += ObjectSetting;
        gameOverDelegate += ObjectSetting;
        gameStartDelegate += StartBall;

        //#�����
        SoundManager.Instance.PlayBGM("BG1", 1);
    }

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

    //#����ٿ� ���� ����� �� �������� �پ���.
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

    //#Wave�� Clear���� ������ ��
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);

        IsGame = false;

        //#�ű���� �޼��ߴ���, ���ߴ���
        BeforeWave = CurWave;

        if(BeforeWave > MaxWave)
            MaxWave = CurWave;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX("GameOver-SFX", 1);
        UIManager.Instance.GameOver_Panel.SetActive(true);
    }

    //#Wave�� Clear ���� ��
    IEnumerator WaveClear()
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

        //#ƨ�ܾ� �ϴ� Ƚ�� ����
        BounceNum = 2 + CurWave;
        UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();

        IsPause = false;

       gameStartDelegate();
    }
}

[System.Serializable]
public class Data
{

}
