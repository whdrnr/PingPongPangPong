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

    [Header("����� ������ ���� ����")]
    public Sprite Guard4;
    public Sprite Guard3;
    public Sprite Guard2;
    public Sprite Guard1;

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

        NewPong.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        NewPong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
    }

    public void StopBall()
    {
        GameObject NewPong = GameObject.FindGameObjectWithTag("Pong");

        NewPong.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
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

    //#Guard�� ���� ������ ������ ���� �� �̹��� ����
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
  