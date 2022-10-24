using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    public Button Start_Btn;

    public override void Initalize()
    {
        Start_Btn.onClick.AddListener(() => Start_Btn_Click());
    }

    void Start_Btn_Click()
    {
        GameManager.Instance.IsGame = true;
        GameManager.Instance.PlayGame();

        UIManager.Show<PlayMenuView>();
    }
}
