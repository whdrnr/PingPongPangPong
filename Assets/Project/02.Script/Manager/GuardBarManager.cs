using UnityEngine;
using TMPro;

public class GuardBarManager : MonoBehaviour
{
    [Header("DangerMove1 관련 변수")]
    public float RotateSpeed = 30;
    [HideInInspector] public float RotateZ;

    Vector3 Touch_Start;

    [Header("DangerMove2 관련 변수")]
    Vector2 StartTouch_Pos;
    Vector2 EndTounch_Pos;

    private void Start()
    {
        GameManager.Instance.gameOverDelegate += ResetAngle;
        GameManager.Instance.waveClearDelegate += ResetAngle;
    }

    public void ResetAngle() => RotateZ = 0;

    void Update()
    {
        //#게임을 플레이할 떄만 데인저를 움직일 수 있다.
        if(GameManager.Instance.IsGame == true && GameManager.Instance.IsPause == false)
            DangerMove2();
    }

    void DangerMove1()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 Dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameManager.Instance.Danger.transform.rotation = Quaternion.Euler(0, 0, RotateZ);

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
            }
        }
    }
}
