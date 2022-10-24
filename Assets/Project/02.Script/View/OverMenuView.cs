using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverMenuView : View
{
    public Button ReStart_Btn;
    public Button Home_Btn;
    public Button Ranking_Lock_Btn;

    public override void Initalize()
    {
        Home_Btn.onClick.AddListener(() => Home_Btn_Click());
    }

    public void Home_Btn_Click()
    {


        UIManager.Show<MainMenuView>();
    }

    public void ReStart_Btn_Click()
    {

    }

    public void Ranking_Lock_Btn_Click()
    {

    }
}
