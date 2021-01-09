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
    public GameObject ShootAudioObj;

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
        Debug.LogWarning("PointerDOWN");
        animator.SetBool("isShooting", true);
        AudioManager.playShootSound();
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    public void ShootanimStop()
    {
        Debug.LogWarning("PointerUP");
        animator.SetBool("isShooting", false);
    }

}
