using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardBarManager : MonoBehaviour
{
    [Header("DangerMove1 관련 변수")]
    public float RotateSpeed = 30;
    float RotateZ;

    Vector3 Touch_Start;

    [Header("DangerMove2 관련 변수")]
    Touch Touch;

    public GameObject Danger;

    void Update()
    {
        DangerMove2();
    }

    void DangerMove1()
    {
        Danger.transform.rotation = Quaternion.Euler(0, 0, RotateZ);

        if (Input.GetMouseButtonDown(0))
            Touch_Start = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector2 Dir = Touch_Start - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Dir.x > 0)
                RotateZ -= RotateSpeed * Time.deltaTime;
            else
                RotateZ += RotateSpeed * Time.deltaTime;
        }
    }

    void DangerMove2()
    {
        if(Input.touchCount > 0)
        {
            Touch Touch = Input.GetTouch(0);

            if(Touch.phase == TouchPhase.Began)
            {
                Debug.Log("Began");
            }
            if (Touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Moved");
            }
            if (Touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Stationary");
            }
        }
    }
}
