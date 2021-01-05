using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour 
{
    public GameObject bullet;
    public float speed = 100f;

    private float lifeTimer= 0;
    public float lifeDuration = 2.0f;

    public static int EnemiesKilled = 0;

    public int DamageToRobot;
    public int DamageToBoss;
    

    public GameObject smoke = null;

    // Start is called before the first frame update
    void Start()
    {
        //HealthPotion SPAWNLA
        //SpawnScript.SpawnHealthPotion(hit.transform);
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        
        //mermi Yok etme
        if (lifeTimer >= lifeDuration)
        {
            Destroy(gameObject);
        }

        lifeTimer += Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        //çarptığı şeye ulaş ve canını azalt
        if (collision.transform.tag == "enemy")
        {
            collision.transform.gameObject.GetComponent<EnemyScript>().HP -= DamageToRobot;
            
            Destroy(collision.transform.gameObject);

            EnemiesKilled++;

            //*****EXPLOSION ANIMATON****
            //Instantiate(smoke, collision.transform.position, Quaternion.LookRotation(ARCamera.transform));
        }
        else if (collision.transform.tag == "Health Potion")
        {
            Destroy(collision.transform.gameObject);

            GameControllerScript.HitHealthPotion();
        }
        else if (collision.transform.tag == "Boss1")
        {
            Boss1Script.Boss1_HP -= DamageToBoss;
        }

        Destroy(gameObject); //Destroy Bullet
    }
}
