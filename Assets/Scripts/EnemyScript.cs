using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Enemy kullanıcı pozisyonunu alıp ona doğru yönelicek

    private GameObject Player;
    public float speed = 5f;
    public int HP;

    void Start()
    {
        //find gameobject with tag player
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveTowardsToThePlayer();
    }

    void MoveTowardsToThePlayer()
    {
        
        float step = speed * Time.deltaTime; // calculate distance to move
        Vector3 relativePos = Player.transform.position - transform.position;
        //move towards to the player
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        //turn towards to the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relativePos, Vector3.up), 0.2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //delete enemy object
        Debug.Log("Enemy Destroy");
        Destroy(gameObject);

        //play death animation

    }
}
