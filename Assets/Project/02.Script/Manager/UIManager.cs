using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Panel ���� ����")]
    public GameObject Lobby_Panel;
    public GameObject Play_Panel;

    [Header("Lobbyl ���� ����")]
    public GameObject Title_Panel;
    public GameObject Main_Panel;

    [Header("Play-Panel��T���� ����")]
    public TextMeshProUGUI CurBounce_Txt;
    public TextMeshProUGUI Wave_Txt;
    public GameObject GameOver_Panel; 

     [Header("Main-Panel��Txt ���� ����")]
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

        //#Danger, Pong ��ġ ����
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

            //#����� ������ ȸ��
            GameObject[] Guards = GameObject.FindGameObjectsWithTag("Guard");

            for (int i = 0; i < Guards.Length; i++)
                Guards[i].GetComponent<GuardBarController>().ResetDurability();

            //#�������� ������ 0���� �ǵ�����.
            GM.Danger.transform.rotation = new Quaternion(0, 0, 0, 0);

            //#���� ��ġ�� ó�� ��ġ�� �ǵ�����.
            Instantiate(GM.Pong, GM.Init_Pos.position, Quaternion.identity);
        }
    }
}
