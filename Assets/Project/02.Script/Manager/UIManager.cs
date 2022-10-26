using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Panel 관련 참조")]
    public GameObject Lobby_Panel;
    public GameObject Play_Panel;

    [Header("Lobby-Panel 관련 참조")]
    public GameObject Title_Panel;
    public GameObject Main_Panel;

    [Header("Play-Panel의Txt 관련 참조")]
    public TextMeshProUGUI CurPoint_Txt;
    public TextMeshProUGUI Wave_Txt;

    [Header("Main-Panel의Txt 관련 참조")]
    public TextMeshProUGUI BeforePoint_Txt;

    public void PlayGame_Btn()
    {
        Play_Panel.SetActive(true);
        Lobby_Panel.SetActive(false);

        GameManager.Instance.IsGame = true;

        //#Danger, Pong 위치 조정
        GameObject CurPong = GameObject.FindWithTag("Pong");
        CurPong.GetComponent<Rigidbody2D>().velocity = Vector2.down * GameManager.Instance.Speed;
        GameManager.Instance.Danger.transform.position = new Vector3(0, 0.5f, 0);
    }

    public void PointUp() => CurPoint_Txt.text = GameManager.Instance.Cur_Socre.ToString();
}
