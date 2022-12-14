using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartUIAnimion : MonoBehaviour
{
    public AnimationCurve AnimationCurve;

    [Header("Object ???? ????")]
    public CanvasGroup Lobby_CG;
    public List<GameObject> ChangeObj = new List<GameObject>();
    public GameObject TeamLogo_Cvs;
    public Image TitleLogo_Img;

    void Start()
    {
        StartCoroutine(StartUI());
    }

    IEnumerator StartUI()
    {
        TitleLogo_Img.DOFade(1, 1);
        SoundManager.Instance.PlaySFX("TeamLogo-SFX", 1);

        yield return new WaitForSeconds(1f);

        TeamLogo_Cvs.SetActive(false);

        Lobby_CG.DOFade(1, AnimationCurve.length);

        for (int i = 0; i < ChangeObj.Count; i++)
        {
            ChangeObj[i].transform.DORotate(new Vector3(0, 0, 360), AnimationCurve.length,
                RotateMode.FastBeyond360);
        }

        yield return new WaitForSeconds(AnimationCurve.length);

        //#Sound BGM On
        SoundManager.Instance.PlayBGM("BG1", 0.5f);

        //#Start Btn Acive
        Lobby_CG.blocksRaycasts = true;
    }
}
