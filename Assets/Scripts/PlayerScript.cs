﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int HP = 100;
    public static int DamageAmount = 10;

    public static int DamagedCount=0;

    private void OnCollisionEnter(Collision collision)
    {
        //decrease HP when collide with something
        HP -= DamageAmount;
        DamagedCount++;
    }
}
