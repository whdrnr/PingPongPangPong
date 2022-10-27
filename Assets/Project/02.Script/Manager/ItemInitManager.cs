using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInitManager : MonoBehaviour
{
    #region  #아이템  생성 방식1
    public CircleCollider2D CircleCollider2D;

    Vector2 Get_RandomCirclePos()
    {
        float Ramdom_X = Random.Range(-CircleCollider2D.bounds.size.x / 2f, CircleCollider2D.bounds.size.x / 2f);
        float Ramdom_Y = Random.Range(-CircleCollider2D.bounds.size.y / 2f, CircleCollider2D.bounds.size.y / 2f);

        Vector2 Spawn_Pos = new Vector2(Ramdom_X, Ramdom_Y);

        return Spawn_Pos;
    }
    #endregion
}
