using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public View StartView;
    public View[] Views;

    View CurView;

    readonly Stack<View> History = new Stack<View>();

    private void Start()
    {
        for (int i = 0; i < Views.Length; i++)
        {
            Views[i].Initalize();

            Views[i].Hide();
        }

        if(StartView != null)
        {
            Show(StartView, true);
        }
    }

    public static T GetView<T>() where T : View
    {
        for(int i =0; i < UIManager.Instance.Views.Length; i++)
        {
            if(UIManager.Instance.Views[i] is T tView)
            {
                return tView;
            }
        }

        return null;
    }

    public static void Show<T>(bool _Remember = true) where T : View
    {
        for(int i = 0; i < UIManager.Instance.Views.Length; i++)
        {
            if (UIManager.Instance.Views[i] is T tView)
            {
                if (UIManager.Instance.CurView != null)
                {
                    if(_Remember)
                    {
                        UIManager.Instance.History.Push(UIManager.Instance.CurView);
                    }

                    UIManager.Instance.CurView.Hide();
                }

                UIManager.Instance.Views[i].Show();

                UIManager.Instance.CurView = UIManager.Instance.Views[i];
            }
        }
    }

    public static void Show(View _View, bool _Remember = true)
    {
        if(UIManager.Instance.CurView != null)
        {
            if(_Remember)
            {
                UIManager.Instance.History.Push(UIManager.Instance.CurView);
            }

            UIManager.Instance.CurView.Hide();
        }

        _View.Show();

        UIManager.Instance.CurView = _View;
    }

    public static void ShowLast()
    {
        if(UIManager.Instance.History.Count != 0)
        {
            Show(UIManager.Instance.History.Pop(), false);
        }
    }
}
