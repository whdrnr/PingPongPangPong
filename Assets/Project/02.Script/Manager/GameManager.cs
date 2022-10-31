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

    [Header("�� ���� ���� ����")]
    public GameObject Pong_Prefeb;
    public Transform Init_Pos;
    public float Speed = 3;

    [Header("���̺� ���� ����")]
    public int MaxWave = 0;
    public int CurWave = 1;
    public int BeforeWave = 0;
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
    }

    void ObjectSetting()
    {
        //#�� ����
        GameObject CurPong = GameObject.FindWithTag("Pong");

        if (CurPong != null)
        {
            Destroy(CurPong);
            Instantiate(Pong_Prefeb, Init_Pos.position, Quaternion.identity);
        }
        else
            Instantiate(Pong_Prefeb, Init_Pos.position, Quaternion.identity);
    }

    //#���� ���� �� ���� �Ʒ��� ��Ѵ�.
    public void StartBall()
    {
        GameObject NewPong = GameObject.FindGameObjectWithTag("Pong");

        NewPong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
    }

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

        UIManager.Instance.GameOver_Panel.SetActive(true);
    }

    //#Wave�� Clear ���� ��
    IEnumerator WaveClear()
    {
        IsPause = true;

        waveClearDelegate();

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
  