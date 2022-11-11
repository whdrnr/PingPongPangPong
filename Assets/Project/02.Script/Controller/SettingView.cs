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

    UIManager UM;
    GameManager GM;

    private void Start()
    {
        UM = UIManager.Instance;
        GM = GameManager.Instance;

        //#디버그용 변수
        PlayGamesPlatform.DebugLogEnabled = true;
        //#구글 관련 Service Active
        PlayGamesPlatform.Activate();
    }

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

        if (GM.Data.IsBGMOn == true) //#Off
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

        GM.Data.IsBGMOn = !GM.Data.IsBGMOn;
    }

    public void SFX_Setting_Btn_Click()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (GM.Data.IsSFXOn == true) //#Off
        {
            SoundManager.Instance.masterVolumeSFX = 0;
            SFX_Setting_Img.sprite = On_Sprite;
        }
        else //#On
        {
            SoundManager.Instance.masterVolumeSFX = 1;
            SFX_Setting_Img.sprite = Off_Sprite;
        }

        GM.Data.IsSFXOn = !GM.Data.IsSFXOn;
    }

    public void Vibration_Setting_Btn_Click()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (GM.Data.IsVibrationOn == true)
        {
            GM.Data.IsVibrationOn = true;
            Vibration_Setting_Img.sprite = On_Sprite;
        }
        else
        {
            GM.Data.IsVibrationOn = false;
            Vibration_Setting_Img.sprite = Off_Sprite;
        }

        GM.Data.IsVibrationOn = !GM.Data.IsVibrationOn;
    }

    public void GoogleLogin_Btn_Click()
    {
        //#현재 기기와 연결된 계정이 인증이 아직 안됬는가?
        if(Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool IsSuccess) =>
            {
                if (IsSuccess == true)
                {
                    Debug.Log("로그인 완료");
                    Google_Btn.SetActive(false);
                }
            });
        }
    }
}
