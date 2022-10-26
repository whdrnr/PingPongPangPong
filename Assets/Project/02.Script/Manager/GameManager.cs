using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [Header("공 생성 관련 참조")]
    public GameObject Pong;
    public Transform Init_Pos;
    public float Speed;

    [Header("데인저 관련 참조")]
    public GameObject Danger;

    [Header("점수 관련 참조")]
    public int High_Score = 0;
    public int Cur_Socre = 0;

    public bool IsGame;

    public void PlayGame() 
    {
        UIManager.Instance.Play_Panel.SetActive(true);
        UIManager.Instance.Lobby_Panel.SetActive(false);

        GameObject CurPong = GameObject.FindWithTag("Pong");
        CurPong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);

        IsGame = false;
        Danger.transform.rotation = new Quaternion(0, 0, 0, 0);

        //#UI On
        UIManager.Instance.Play_Panel.SetActive(false);
        UIManager.Instance.Lobby_Panel.SetActive(true);

        UIManager.Instance.Title_Panel.SetActive(false);
        UIManager.Instance.Main_Panel.SetActive(true);

        //#점수 관련 참조
        UIManager.Instance.BeforePoint_Txt.text = Cur_Socre.ToString();
        UIManager.Instance.CurPoint_Txt.text = "0";
        Cur_Socre = 0;

        //#공 삭제
        GameObject CurPong = GameObject.FindWithTag("Pong");
        Destroy(CurPong);

        //#새로운 공 생성
        GameObject NewPong = Instantiate(Pong, Init_Pos.position, Quaternion.identity);
    }
}
