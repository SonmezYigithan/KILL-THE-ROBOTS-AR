﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //Enemy kullanıcı pozisyonunu alıp ona doğru yönelicek

    private GameObject Player;
    public float speed = 5f;
    public float HP;
    SpawnTheRobots SpawnTheRobots;

    //HP BAR
    public float maxHP;
    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        //find gameobject with tag player
        Player = GameObject.FindGameObjectWithTag("Player");
        SpawnTheRobots = GameObject.FindGameObjectWithTag("SpawnEdge").GetComponent<SpawnTheRobots>();
    }

    void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
            GameControllerScript.EnemiesKilled++;
            SpawnTheRobots.SpawnHealthPotion(transform);
        }

        MoveTowardsToThePlayer();

        if (healthBarUI)
        {
            EnemyHealthBarHandle();
        }
        
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
        if (collision.collider.name == "Player")
        {
            Destroy(gameObject);
        }
        

        //play Player get hit animation

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
