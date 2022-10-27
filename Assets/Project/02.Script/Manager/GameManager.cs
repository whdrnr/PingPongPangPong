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
    public int CurWave = 0;
    public int BounceNum = 3; //#ƨ�ܾ� �ϴ� Ƚ��

    public bool IsGame;

    //#Wave�� Clear���� ������ ��
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        
        IsGame = false;

        UIManager.Instance.GameOver_Panel.SetActive(true);

        GameObject CurPong = GameObject.FindWithTag("Pong");
        Destroy(CurPong);
    }

    public void WaveClear()
    {
        if(BounceNum == 0)
        {
            Debug.Log("���̺� Ŭ����");
        }
    }
}
