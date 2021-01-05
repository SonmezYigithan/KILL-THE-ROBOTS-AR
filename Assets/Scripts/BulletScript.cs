using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour 
{
    public GameObject ARCamera;
    public GameObject bullet;
    public float speed = 100f;

    private float lifeTimer= 0;
    public float lifeDuration = 2.0f;

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
}
