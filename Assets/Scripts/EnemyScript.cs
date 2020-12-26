using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Enemy kullanıcı pozisyonunu alıp ona doğru yönelicek

    private GameObject Player;
    public float speed = 5f;
    void Start()
    {
        //find gameobject with tag player
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position),0.2f);

    }
}
