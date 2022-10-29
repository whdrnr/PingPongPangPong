using UnityEngine;
using TMPro;

public class GuardBarManager : MonoBehaviour
{
    [Header("DangerMove 관련 참조")]
    public float RotateSpeed = 30;
    [HideInInspector] public float RotateZ;

    [Header("가드바 내구도 관련")]
    public Sprite Guard4;
    public Sprite Guard3;
    public Sprite Guard2;
    public Sprite Guard1;

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
        }
    }

    public void DurabilityGuard(int _Durability, bool _IsDestory ,BoxCollider2D _BoxCollider2D, SpriteRenderer _Guard)
    {
        switch(_Durability)
        {
            case 4:
                _BoxCollider2D.size = new Vector2(2.2f, 0.8f);
                _Guard.sprite = Guard1;
                break;

            case 3:
                _BoxCollider2D.size = new Vector2(1.9f, 0.8f);
                _Guard.sprite = Guard2;
                break;

            case 2:
                _BoxCollider2D.size = new Vector2(1.45f, 0.8f);
                _Guard.sprite = Guard3;
                break;

            case 1:
                _BoxCollider2D.size = new Vector2(1.1f, 0.8f);
                _Guard.sprite = Guard4;
                break;

            case 0:
                _IsDestory = true;
                break;
        }
    }
}
