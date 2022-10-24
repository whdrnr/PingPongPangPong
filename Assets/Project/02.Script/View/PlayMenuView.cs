using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenuView : View
{
    public Button Pause_Btn;

    public override void Initalize()
    {
        Pause_Btn.onClick.AddListener(() => UIManager.ShowLast());
    }
}
