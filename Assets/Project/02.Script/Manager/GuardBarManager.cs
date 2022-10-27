using UnityEngine;
using TMPro;

public class GuardBarManager : MonoBehaviour
{
    [Header("DangerMove1 ���� ����")]
    public float RotateSpeed = 30;
    float RotateZ;

    Vector3 Touch_Start;

    [Header("DangerMove2 ���� ����")]
    Vector2 StartTouch_Pos;
    Vector2 EndTounch_Pos;

    void Update()
    {
        //#������ �÷����� ���� �������� ������ �� �ִ�.
        if(GameManager.Instance.IsGame == true && GameManager.Instance.IsPause == false)
            DangerMove2();
    }

    void DangerMove1()
    {
        //Danger.transform.rotation = Quaternion.Euler(0, 0, RotateZ);

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

            if (Touch.phase == TouchPhase.Began)
            {
                StartTouch_Pos = Input.GetTouch(0).position;
            }
            if (Touch.phase == TouchPhase.Moved)
            {
                EndTounch_Pos = Input.GetTouch(0).position;

                if (EndTounch_Pos.x < StartTouch_Pos.x)
                    RotateZ += -RotateSpeed * Time.deltaTime;
                else if (EndTounch_Pos.x > StartTouch_Pos.x)
                    RotateZ += RotateSpeed * Time.deltaTime;

                GameManager.Instance.Danger.transform.rotation = Quaternion.Euler(0, 0, RotateZ);
            }
            if (Touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Stationary");
            }
        }
    }
}
