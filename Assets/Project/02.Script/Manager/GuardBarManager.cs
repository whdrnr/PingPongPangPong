using UnityEngine;
using TMPro;

public class GuardBarManager : Singleton<GuardBarManager>
{
    [Header("DangerMove ���� ����")]
    public float RotateSpeed = 30;
    [HideInInspector] public float RotateZ;
    public GameObject Dnager;

    [Header("����� ������ ���� ����")]
    public Sprite Guard4;
    public Sprite Guard3;
    public Sprite Guard2;
    public Sprite Guard1;

    [Header("��ƼŬ ����")]
    public ParticleSystem[] P_Guard_Hit;
    public ParticleSystem[] P_Guard_Destory;

    Vector2 StartTouch_Pos;
    Vector2 EndTounch_Pos;

    void Start()
    {
        //#Delegate �Լ� ����
        GameManager.Instance.gameOverDelegate += ResetAngle;
        GameManager.Instance.gameOverDelegate += ResetDamgerAngle;

        GameManager.Instance.waveClearDelegate += ResetAngle;
        GameManager.Instance.waveClearDelegate += ResetDamgerAngle;

        GameManager.Instance.gameStartDelegate += StartDangerPos;
    }

    void Update()
    {
        //#������ �÷����� ���� �������� ������ �� �ִ�.
        if(GameManager.Instance.IsGame == true && GameManager.Instance.IsPause == false)
            DangerMove2();
    }

    void DangerMove2()
    {
        if (Input.touchCount > 0)
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

                Dnager.transform.rotation = Quaternion.Euler(0, 0, RotateZ);
            }
        }
    }

    //#Guard�� ���� ������ ������ ���� �� �̹��� ����
    public void DurabilityGuard(int _Durability, BoxCollider2D _BoxCollider2D, SpriteRenderer _Guard)
    {
        switch (_Durability)
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
        }
    }

    public void ResetAngle() => RotateZ = 0;

    public void StartDangerPos() => Dnager.transform.position = new Vector3(0, 0.5f, 0);

    public void ResetDamgerAngle() => Dnager.transform.rotation = new Quaternion(0, 0, 0, 0);
}
