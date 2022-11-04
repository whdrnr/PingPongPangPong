using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoBehaviour
{
    public GameObject Setting_Panel;
    public GameObject Google_Btn;

    [Header("On/Off 관련 참조")]
    public Sprite On_Sprite;
    public Sprite Off_Sprite;
    public Image BGM_Setting_Img;
    public Image SFX_Setting_Img;
    public Image Vibration_Setting_Img;

    bool _IsBGMOn = true;
    bool _IsSFXOn = true;
    bool _IsVibrationOn = true;

    public void Setting_Btn()
    {
        Setting_Panel.SetActive(true);
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Time.timeScale = 0;
    }

    public void Back_Btn()
    {
        Setting_Panel.SetActive(false);
        SoundManager.Instance.PlaySFX("Click-SFX", 1);
        Time.timeScale = 1;
    }

    public void BGM_Setting_Btn_Click()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (_IsBGMOn == true) //#Off
        {
            SoundManager.Instance.masterVolumeBGM = 0;
            SoundManager.Instance.BGMPlayer.volume = 0;
            BGM_Setting_Img.sprite = On_Sprite;
        }
        else //#On
        {
            SoundManager.Instance.masterVolumeBGM = 1;
            SoundManager.Instance.BGMPlayer.volume = 1;
            BGM_Setting_Img.sprite = Off_Sprite;
        }

        _IsBGMOn = !_IsBGMOn;
    }

    public void SFX_Setting_Btn_Click()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (_IsSFXOn == true) //#Off
        {
            SoundManager.Instance.masterVolumeSFX = 0;
            SFX_Setting_Img.sprite = On_Sprite;
        }
        else //#On
        {
            SoundManager.Instance.masterVolumeSFX = 1;
            SFX_Setting_Img.sprite = Off_Sprite;
        }

        _IsSFXOn = !_IsSFXOn;
    }

    public void Vibration_Setting_Btn_Click()
    {
        _IsVibrationOn = !_IsVibrationOn;
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (_IsVibrationOn == true)
        {
            Debug.Log("On");
            Vibration_Setting_Img.sprite = On_Sprite;
        }
        else
        {
            Debug.Log("Off");
            Vibration_Setting_Img.sprite = Off_Sprite;
        }
    }

    public void GoogleLogin_Btn_Click()
    {
    }
}
