using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardBarManager : MonoBehaviour
{
    [Header("Unity Editor Version")]
    public float RotateSpeed = 30;
    float RotateZ;

    Vector3 Touch_Start;

    public GameObject Danger;

    void Update()
    {
        #region #¾¾¹ß 
        /*
        Danger.transform.rotation = Quaternion.Euler(0, 0, RotateZ);

        if (Input.GetMouseButtonDown(0))
            Touch_Start = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector2 Dir = Touch_Start - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Dir.x > 0)
            {
                Debug.Log("¿À¸¥ÂÊ");
            }
            else if(Dir.x < 0)
            {
                Debug.Log("¿ÞÂÊ");
            }
        }
        */
        #endregion

    }
}
