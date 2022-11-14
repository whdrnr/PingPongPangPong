using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    public string Name;
    public AudioClip Clip;
}

public class SoundManager : Singleton<SoundManager>
{
    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    public Sound[] BGM; // ����� �ҽ��� ����.
    public Sound[] SFX; // ����� �ҽ��� ����.

    public AudioSource[] SFXPlayer;
    public AudioSource BGMPlayer;

    public void PlayBGM(string _BGMName, float _Volume)
    {
        for (int i = 0; i < BGM.Length; i++)
        {
            if (_BGMName == BGM[i].Name)
            {
                BGMPlayer.clip = BGM[i].Clip;
                BGMPlayer.volume = masterVolumeBGM * _Volume;
                BGMPlayer.Play();
            }
        }
    }

    public void StopBGM() => BGMPlayer.Stop();

    public void PlaySFX(string _SFXName,  float _Volume)
    {
        for (int i = 0; i < SFX.Length; i++)
        {
            if (_SFXName == SFX[i].Name)
            {
                SFXPlayer[i].clip = SFX[i].Clip;
                SFXPlayer[i].volume = masterVolumeSFX * _Volume;
                SFXPlayer[i].Play();

                return;
            }
        }

        Debug.Log(_SFXName + "�̸��� ȿ������ �����ϴ�");
    }

    public void BGMVolume(int _Volume)
    {
        if (_Volume == 0)
            masterVolumeBGM = 0;
        else if (_Volume == 1)
            masterVolumeBGM = 1;
    }

    public void SFXVolume(int _Volume)
    {
        if (_Volume == 0)
            masterVolumeSFX = 0;
        else if (_Volume == 1)
            masterVolumeSFX = 1;
    }

    public void CatSound()
    {
        int RandomNum = Random.Range(0, 2);

        if(RandomNum == 0)
            PlaySFX("Meaw1-SFX", 1);
        else if(RandomNum == 1)
            PlaySFX("Meaw2-SFX", 1);
    }
}
