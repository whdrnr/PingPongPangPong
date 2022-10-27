using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
     public delegate void GameOverDelegate();
     public GameOverDelegate gameOverDelegate;

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

    //#Wave�� Clear���� ������ ��
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

            //#�������� ������ 0���� �ǵ�����.
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

        //#���̺� ���� 
        CurWave++;
        UIManager.Instance.Wave_Txt.text = CurWave.ToString();

        //#ƨ�ܾ� �ϴ� Ƚ�� ����
        BounceNum = 2 + CurWave;
        UIManager.Instance.CurBounce_Txt.text = BounceNum.ToString();
    }
}
