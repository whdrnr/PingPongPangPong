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

    void Start()
    {
        GM = GameManager.Instance;
    }

    public void PlayGame_Btn()
    {
        //#UI On/Off
        Play_Panel.SetActive(true);
        Lobby_Panel.SetActive(false);

        //#���� �ʱ�ȭ
        GM.BounceNum = 3;
        CurBounce_Txt.text = GM.BounceNum.ToString();

        GM.CurWave = 1;
        Wave_Txt.text = GM.CurWave.ToString();

        //#Danger, Pong ��ġ ����
        GM.StartBall();
        GM.Danger.transform.position = new Vector3(0, 0.5f, 0);

        //#������ ����
        ItemInitManager.Instance.ItemInit();
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

            //#Text ��ȣ�ۿ�
            BeforeWave_Txt.text = GM.BeforeWave.ToString();
            MaxWave_Txt.text = "High Point " + GM.MaxWave.ToString();

            //#������ ����
            ItemInitManager.Instance.DestroyItem();

            GM.gameOverDelegate();
        }
    }
}
