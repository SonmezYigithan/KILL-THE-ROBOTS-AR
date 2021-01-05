using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;

    GameObject spawnScriptObj;
    SpawnTheRobots SpawnScript;

    public static int EnemiesKilled = 0;

    void Start()
    {
        spawnScriptObj = GameObject.FindGameObjectWithTag("SpawnEdge");
        SpawnScript = spawnScriptObj.GetComponent<SpawnTheRobots>();
    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                SpawnTheRobots.Robots.Remove(hit.transform.gameObject);

                Destroy(hit.transform.gameObject);

                EnemiesKilled++;

                // Direkt destroy etmeden iki,üç vuruşta ölsün

                //*****EXPLOSION ANIMATON****
                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));

                SpawnScript.SpawnHealthPotion(hit.transform);
            }
            else if(hit.transform.tag == "Health Potion")
            {
                Destroy(hit.transform.gameObject);
                GameControllerScript.HitHealthPotion();
            }
            else if(hit.transform.tag == "Boss1")
            {
                Boss1Script.Boss1_HP -= 10;
            }

        }
    }
}
