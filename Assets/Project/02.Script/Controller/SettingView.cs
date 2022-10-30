using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingView : MonoBehaviour
{
    public GameObject Setting_Panel;

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
}
