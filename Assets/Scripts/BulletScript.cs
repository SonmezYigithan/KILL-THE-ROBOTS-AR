using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour 
{
    public GameObject bullet;
    public float speed = 100f;

    private float lifeTimer= 0;
    public float lifeDuration = 2.0f;

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

    private void OnTriggerEnter(Collider other)
    {
        //çarptığı şeye ulaş ve canını azalt
        if (other.transform.tag == "Enemy")
        {
            other.transform.gameObject.GetComponent<EnemyScript>().HP -= DamageToRobot;

            //*****EXPLOSION ANIMATON****
            //Instantiate(smoke, collision.transform.position, Quaternion.LookRotation(ARCamera.transform));
        }
        else if (other.transform.tag == "Health Potion")
        {
            Destroy(other.transform.gameObject);

            GameControllerScript.HitHealthPotion();
        }
        else if (other.transform.tag == "Boss1")
        {
            Boss1Script.Boss1_HP -= DamageToBoss;
        }

        Destroy(gameObject); //Destroy Bullet
    }
}
