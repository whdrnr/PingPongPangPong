using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartUIAnimion : MonoBehaviour
{
    public AnimationCurve AnimationCurve;

    [Header("Object 관련 참조")]
    public List<GameObject> FadeObj = new List<GameObject>();
    public List<GameObject> ChangeObj = new List<GameObject>();

    void Start()
    {
        for(int i =0; i < FadeObj.Count; i++)
            FadeObj[i].GetComponent<Image>().DOFade(1 , AnimationCurve.length);

        for(int i =0; i < ChangeObj.Count; i++)
        {
            ChangeObj[i].transform.DORotate(new Vector3(0,0, 360), AnimationCurve.length, 
                RotateMode.FastBeyond360);
        }

        ChangeObj[0].transform.DOScale(new Vector3(0.8f, 0.8f, 1), AnimationCurve.length);
        ChangeObj[1].transform.DOScale(new Vector3(0.3f, 0.3f, 1), AnimationCurve.length);
    }
}
