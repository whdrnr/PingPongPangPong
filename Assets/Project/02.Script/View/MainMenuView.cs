using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    public Button GameStart_Btn;

    [Header("Icon 버튼 관련 참조")]
    public Button AD_Romove_Btn;
    public Button SkinChange_Btn;
    public Button Setting_Btn;
    public Button Ranking_Lock_Btn;

    public override void Initalize()
    {
        GameStart_Btn.onClick.AddListener(() => GameStart_Btn_Click());
    }

    void GameStart_Btn_Click()
    {
        GameManager.Instance.IsGame = true;
        GameManager.Instance.PlayGame();

        UIManager.Show<PlayMenuView>();
    }


    
}
