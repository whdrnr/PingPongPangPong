using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public GameObject Pong;
    public Transform Init_Pos;

    public float Speed;
    public bool IsGame;

    public void PlayGame()
    {
        GameObject NewPong = Instantiate(Pong, Init_Pos.position, Quaternion.identity);
        NewPong.GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed;
    }
}
