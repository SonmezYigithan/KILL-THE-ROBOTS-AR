using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke; 

    public void Shoot()
    {
        RaycastHit hit; 

        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if(hit.transform.name == "Enemy")
            {
                Destroy(hit.transform.gameObject);
                //Can azaltılacak
                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
