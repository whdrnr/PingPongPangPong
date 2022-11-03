using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class SettingView : MonoBehaviour
{
    public GameObject Setting_Panel;
    public GameObject Google_Btn;

    [Header("On/Off ���� ����")]
    public Sprite On_Sprite;
    public Sprite Off_Sprite;

    public void Setting_Btn()
    {
        Setting_Panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Back_Btn()
    {
        Setting_Panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void BGM_Setting_Btn_Click()
    {
    }

    public void SFX_Setting_Btn_Click()
    {
    }

    public void GoogleLogin_Btn_Click()
    {
    }
}
