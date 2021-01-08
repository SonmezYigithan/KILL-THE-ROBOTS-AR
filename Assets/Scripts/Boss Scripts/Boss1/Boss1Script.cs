using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Script : MonoBehaviour
{
    // BOSS 1
    // Boss canı handle la
    // belli süre aralıklarıyla ataş edecek 
    // random animasyonlar olacak

    public static int Boss1_HP;


    void Start()
    {
        Boss1_HP = 200;
    }

    void Update()
    {
        if (Boss1_HP <= 0)
        {
            Debug.Log("Boss1 has Died");
            
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        GameControllerScript.IsBossDead = true;
    }
}
