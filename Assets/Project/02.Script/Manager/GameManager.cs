using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
     public delegate void GameStartDelegate();
     public GameStartDelegate gameStartDelegate;

    public delegate void GameOverDelegate();
    public GameStartDelegate gameOverDelegate;

    public delegate void GamePauseDelegate();
    public GameStartDelegate gamePauseDelegate;

    [Header("�� ���� ���� ����")]
    public GameObject Pong;
    public Transform Init_Pos;
    public float Speed;

    [Header("������ ���� ����")]
    public GameObject Danger;

    [Header("���� ���� ����")]
    public int MaxWave = 0;
    public int CurWave = 1;
    public int BounceNum = 3; //#ƨ�ܾ� �ϴ� Ƚ��

    public bool IsGame;
    public bool IsPause;

    void Start()
    {
        gameStartDelegate += ObjectSetting;
        gameOverDelegate += ObjectSetting;
    }

    void ObjectSetting()
    {
        //#�������� ������ 0���� �ǵ�����.
        Danger.transform.rotation = new Quaternion(0, 0, 0, 0);

        //#�� ����
        GameObject CurPong = GameObject.FindWithTag("Pong");

        if (CurPong != null)
        {
            Destroy(CurPong);
            Instantiate(Pong, Init_Pos.position, Quaternion.identity);
        }
        else
            Instantiate(Pong, Init_Pos.position, Quaternion.identity);
    }

    //#Wave�� Clear���� ������ ��
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        
        IsGame = false;
        BounceNum = 3;

        UIManager.Instance.GameOver_Panel.SetActive(true);

        gameOverDelegate();
    }

    public void HitGuard()
    {
        if(BounceNum == 0)
        {
            StartCoroutine(WaveClear());
        }
        else
        {
            BounceNum--;
            UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();
        }
    }

    public void StartBall()
    {
        GameObject CurPong = GameObject.FindWithTag("Pong");
        CurPong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
    }

    IEnumerator WaveClear()
    {
        IsPause = true;

        ObjectSetting();

         //#���̺� ���� 
         CurWave++;
         UIManager.Instance.Wave_Txt.text = CurWave.ToString();

        //#ī��Ʈ �ٿ�
        UIManager.Instance.CurBounce_Txt.text = "3";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "2";
        yield return new WaitForSeconds(1f);

        UIManager.Instance.CurBounce_Txt.text = "1";
        yield return new WaitForSeconds(1f);

        //#ƨ�ܾ� �ϴ� Ƚ�� ����
        BounceNum = 2 + CurWave;
        UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();

        IsPause = false;

        StartBall();
    }
}
  