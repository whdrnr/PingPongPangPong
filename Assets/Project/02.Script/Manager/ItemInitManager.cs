using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInitManager : Singleton<ItemInitManager>
{
    public GameObject Item_Prefeb;
    public CircleCollider2D CircleCollider2D;

    void Start()
    {
        //#Delegate 함수 연결
        GameManager.Instance.gameOverDelegate += DestroyItem;
    }

    //#아이템 생성
    public void ItemInit() => Instantiate(Item_Prefeb, Get_RandomCirclePos(), Quaternion.identity);

    //#T초 마다 아이템 생성
    public void ItemInit(float _T) => Invoke("ItemInit", _T);

    //#아이템 삭제
    public void DestroyItem()
    {
        GameObject CurItem = GameObject.FindGameObjectWithTag("Item");
        Destroy(CurItem);
    }

    //#아이템이 랜덤하게 생성 될 Vector2의 값을 반환한다.
    Vector2 Get_RandomCirclePos()
    {
        float Ramdom_X = Random.Range(CircleCollider2D.radius / 2f * -1, CircleCollider2D.radius / 2f);
        float Ramdom_Y = Random.Range(CircleCollider2D.radius / 2f * -1, CircleCollider2D.radius / 2f);

        Vector2 Spawn_Pos = new Vector2(Ramdom_X, Ramdom_Y);

        return Spawn_Pos;
    }
}
