using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //Enemy kullanıcı pozisyonunu alıp ona doğru yönelicek

    private GameObject Player;
    public float speed = 5f;
    public float HP;

    //HP BAR
    public float maxHP;
    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        //find gameobject with tag player
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveTowardsToThePlayer();
        EnemyHealthBarHandle();
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

    float CalculateHealth()
    {
        return HP / maxHP;
    }

    void EnemyHealthBarHandle()
    {
        slider.value = CalculateHealth();

        if (HP < maxHP)
        {
            healthBarUI.SetActive(true);
        }
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        if (HP > maxHP)
        {
            HP = maxHP;
        }
    }
}
