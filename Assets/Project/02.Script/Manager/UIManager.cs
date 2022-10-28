using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Panel 관련 참조")]
    public GameObject Lobby_Panel;
    public GameObject Play_Panel;

    [Header("Lobbyl 관련 참조")]
    public GameObject Title_Panel;
    public GameObject Main_Panel;

    [Header("Play-Panel의T관련 참조")]
    public TextMeshProUGUI CurBounce_Txt;
    public TextMeshProUGUI Wave_Txt;
    public GameObject GameOver_Panel; 

     [Header("Main-Panel의Txt 관련 참조")]
    public TextMeshProUGUI BeforeWave_Txt;
    public TextMeshProUGUI MaxWave_Txt;

    GameManager GM;

    private void Start()
    {
        GM = GameManager.Instance;
    }

    public void PlayGame_Btn()
    {
        Play_Panel.SetActive(true);
        Lobby_Panel.SetActive(false);

        GM.IsGame = true;

        //#Danger, Pong 위치 조정
        GM.StartBall();
        GM.Danger.transform.position = new Vector3(0, 0.5f, 0);
    }

    public void GmaeOver_Btn()
    {
        if (GM.IsGame == false)
        {
            //#UI On/Off
            Play_Panel.SetActive(false);
            Lobby_Panel.SetActive(true);

            Title_Panel.SetActive(false);
            Main_Panel.SetActive(true);

            //#가드바 내구도 회복
            GameObject[] Guards = GameObject.FindGameObjectsWithTag("Guard");

            for (int i = 0; i < Guards.Length; i++)
                Guards[i].GetComponent<GuardBarController>().ResetDurability();

            //#데인저의 각도를 0으로 되돌린다.
            GM.Danger.transform.rotation = new Quaternion(0, 0, 0, 0);

            //#퐁의 위치를 처음 위치로 되돌린다.
            Instantiate(GM.Pong, GM.Init_Pos.position, Quaternion.identity);
        }
    }
}
