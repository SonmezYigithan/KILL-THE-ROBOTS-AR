using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Script : MonoBehaviour
{
    // BOSS 1
    // Boss canı handle la
    // belli süre aralıklarıyla ataş edecek 
    // random animasyonlar olacak

    public static int Boss1_HP = 200;


    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log("HPBOSS" + Boss1_HP);
    }
}
