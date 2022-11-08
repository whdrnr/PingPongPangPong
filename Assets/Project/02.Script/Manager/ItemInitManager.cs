using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInitManager : Singleton<ItemInitManager>
{
    public GameObject Item_Prefeb;
    public CircleCollider2D CircleCollider2D;

    void Start()
    {
        //#Delegate �Լ� ����
        GameManager.Instance.gameOverDelegate += DestroyItem;
    }

    //#������ ����
    public void ItemInit() => Instantiate(Item_Prefeb, Get_RandomCirclePos(), Quaternion.identity);

    //#T�� ���� ������ ����
    public void ItemInit(float _T) => Invoke("ItemInit", _T);

    //#������ ����
    public void DestroyItem()
    {
        GameObject CurItem = GameObject.FindGameObjectWithTag("Item");
        Destroy(CurItem);
    }

    //#�������� �����ϰ� ���� �� Vector2�� ���� ��ȯ�Ѵ�.
    Vector2 Get_RandomCirclePos()
    {
        float Ramdom_X = Random.Range(CircleCollider2D.radius / 2f * -1, CircleCollider2D.radius / 2f);
        float Ramdom_Y = Random.Range(CircleCollider2D.radius / 2f * -1, CircleCollider2D.radius / 2f);

        Vector2 Spawn_Pos = new Vector2(Ramdom_X, Ramdom_Y);

        return Spawn_Pos;
    }
}
