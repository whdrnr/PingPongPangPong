using UnityEngine;
using TMPro;

public class GuardBarManager : Singleton<GuardBarManager>
{
    [Header("DangerMove ���� ����")]
    public float RotateSpeed = 30;
    [HideInInspector] public float RotateZ;
    public GameObject Danger;

    [Header("����� ������ ���� ����")]
    public Sprite Guard4;
    public Sprite Guard3;
    public Sprite Guard2;
    public Sprite Guard1;

    [Header("��ƼŬ ����")]
    public ParticleSystem[] P_Guard_Hit;
    public ParticleSystem[] P_Guard_Destory;

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

            if (Touch.phase == TouchPhase.Moved)
            {
                Vector3 Rotation = Input.GetTouch(0).deltaPosition;

                Danger.transform.Rotate(0, 0, Rotation.x * RotateSpeed * Time.deltaTime);
            }

        }
    }

    //#Guard�� ���� ������ ������ ���� �� �̹��� ����
    public void DurabilityGuard(int _Durability, BoxCollider2D _BoxCollider2D, SpriteRenderer _Guard)
    {
        switch (_Durability)
        {
            case 7:
            case 6:
                _BoxCollider2D.size = new Vector2(2.2f, 0.8f);
                _Guard.sprite = Guard1;
                break;

            case 5:
            case 4:
                _BoxCollider2D.size = new Vector2(1.9f, 0.8f);
                _Guard.sprite = Guard2;
                break;

            case 3:
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

    public void StartDangerPos() => Danger.transform.position = new Vector3(0, 0.5f, 0);

    public void ResetDamgerAngle() => Danger.transform.rotation = new Quaternion(0, 0, 0, 0);
}
