
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

        PlayGamesPlatform.DebugLogEnabled = true;  //#디버그용 변수
        PlayGamesPlatform.Activate();   //#구글 관련 Service Active

        #region #Setting UI Load
        //#BGM On/Off Img
        if (GM.Data.IsBGMOn == true)
            BGM_Setting_Img.sprite = On_Sprite;
        else
            BGM_Setting_Img.sprite = Off_Sprite;

        //#SFX On/Off Img
        if (GM.Data.IsSFXOn == true)
            SFX_Setting_Img.sprite = On_Sprite;
        else
            SFX_Setting_Img.sprite = Off_Sprite;

        //#진동음 On/Off Img
        if (GM.Data.IsVibrationOn == true)
            Vibration_Setting_Img.sprite = On_Sprite;
        else
            Vibration_Setting_Img.sprite = Off_Sprite;
        #endregion
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

        if (GM.Data.IsBGMOn == true) //#On
        {
            SoundManager.Instance.masterVolumeBGM = 0;
            SoundManager.Instance.BGMPlayer.volume = 0;
            BGM_Setting_Img.sprite = Off_Sprite;
        }
        else //#Off
        {
            SoundManager.Instance.masterVolumeBGM = 1;
            SoundManager.Instance.BGMPlayer.volume = 1;
            BGM_Setting_Img.sprite = On_Sprite;
        }

        GM.Data.IsBGMOn = !GM.Data.IsBGMOn;
    }

    public void SFX_Setting_Btn_Click()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (GM.Data.IsSFXOn == true) //#Off
        {
            SoundManager.Instance.masterVolumeSFX = 0;
            SFX_Setting_Img.sprite = Off_Sprite;
        }
        else //#On
        {
            SoundManager.Instance.masterVolumeSFX = 1;
            SFX_Setting_Img.sprite = On_Sprite;
        }

        GM.Data.IsSFXOn = !GM.Data.IsSFXOn;
    }

    public void Vibration_Setting_Btn_Click()
    {
        SoundManager.Instance.PlaySFX("Click-SFX", 1);

        if (GM.Data.IsVibrationOn == true)
            Vibration_Setting_Img.sprite = Off_Sprite;
        else
            Vibration_Setting_Img.sprite = On_Sprite;

        GM.Data.IsVibrationOn = !GM.Data.IsVibrationOn;
    }

    public void GoogleLogin_Btn_Click()
    {
        //#현재 기기와 연결된 계정이 인증이 아직 안됬는가?
        if(Social.localUser.authenticated == false && GM.Data.IsGoogleLogin == false)
        {
            Social.localUser.Authenticate((bool IsSuccess) =>
            {
                if (IsSuccess == true)  //#로그인 성공시 상호작용
                {
                    Google_Btn.SetActive(false);
                    GM.Data.IsGoogleLogin = true;
                }
            });
        }
        else 
            Google_Btn.SetActive(false);
    }
}
