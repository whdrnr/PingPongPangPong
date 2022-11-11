using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartUIAnimion : MonoBehaviour
{
    public AnimationCurve AnimationCurve;

    [Header("Object 관련 참조")]
    public CanvasGroup Lobby_CG;
    public List<GameObject> ChangeObj = new List<GameObject>();

    void Start()
    {
        StartCoroutine(StartUI());
    }

    IEnumerator StartUI()
    {
        Lobby_CG.DOFade(1, AnimationCurve.length);

        for (int i = 0; i < ChangeObj.Count; i++)
        {
            ChangeObj[i].transform.DORotate(new Vector3(0, 0, 360), AnimationCurve.length,
                RotateMode.FastBeyond360);
        }

        yield return new WaitForSeconds(AnimationCurve.length);

        //#Start Btn Acive
        Lobby_CG.blocksRaycasts = true;
    }
}
