using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInitManager : Singleton<ItemInitManager>
{
    public GameObject Item_Prefeb;
    public CircleCollider2D CircleCollider2D;

    //#������ ����
    public void ItemInit()
    {
        Instantiate(Item_Prefeb, Get_RandomCirclePos(), Quaternion.identity);
    }

    //#������ ����
    public void DestroyItem()
    {
        GameObject CurItem = GameObject.FindGameObjectWithTag("Item");
        Destroy(CurItem);
    }

    //#�������� �����ϰ� ���� �� Vector2�� ���� ��ȯ�Ѵ�.
    Vector2 Get_RandomCirclePos()
    {
        float Ramdom_X = Random.Range(-CircleCollider2D.bounds.size.x / 2f, CircleCollider2D.bounds.size.x / 2f);
        float Ramdom_Y = Random.Range(-CircleCollider2D.bounds.size.y / 2f, CircleCollider2D.bounds.size.y / 2f);

        Vector2 Spawn_Pos = new Vector2(Ramdom_X, Ramdom_Y);

        return Spawn_Pos;
    }
}
