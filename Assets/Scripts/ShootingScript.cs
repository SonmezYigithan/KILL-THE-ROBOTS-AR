using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;

    public static int EnemiesKilled = 0;

<<<<<<< Updated upstream
=======
    public GameObject Pistol;
    private Animator animator;

    void Start()
    {
        spawnScriptObj = GameObject.FindGameObjectWithTag("SpawnEdge");
        SpawnScript = spawnScriptObj.GetComponent<SpawnTheRobots>();
        animator = Pistol.GetComponent<Animator>();
    }

>>>>>>> Stashed changes
    public void Shoot()
    {
        RaycastHit hit;
        animator.SetBool("isShooting", true);
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                Destroy(hit.transform.gameObject);

                EnemiesKilled++;

                // Direkt destroy etmeden iki,üç vuruşta ölsün

                //*****EXPLOSION ANIMATON****
                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if(hit.transform.tag == "Health Potion")
            {
                Destroy(hit.transform.gameObject);
<<<<<<< Updated upstream
=======
                GameControllerScript.HitHealthPotion();
            }
            else if(hit.transform.tag == "Boss1")
            {
                Boss1Script.Boss1_HP -= 200;
>>>>>>> Stashed changes
            }
        }
    }

    public void ShootanimStop()
    {
        animator.SetBool("isShooting", false);
    }
}
