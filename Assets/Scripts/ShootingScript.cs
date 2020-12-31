using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;

    public static int EnemiesKilled = 0;

    public void Shoot()
    {
        RaycastHit hit;

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
        }
    }
}
