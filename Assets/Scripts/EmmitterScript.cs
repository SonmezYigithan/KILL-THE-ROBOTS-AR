using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmitterScript : MonoBehaviour
{
    public GameObject arCamera;

    GameObject spawnScriptObj;
    SpawnTheRobots SpawnScript;

    public static int EnemiesKilled = 0;

    public GameObject Pistol;
    private Animator animator;

    public GameObject bulletPrefab;

    void Start()
    {
        spawnScriptObj = GameObject.FindGameObjectWithTag("SpawnEdge");
        SpawnScript = spawnScriptObj.GetComponent<SpawnTheRobots>();
        animator = Pistol.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        animator.SetBool("isShooting", true);
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    public void ShootanimStop()
    {
        animator.SetBool("isShooting", false);
    }

}
