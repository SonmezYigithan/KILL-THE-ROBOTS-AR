using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAganistPlayer : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        TurnTowardsToThePlayer();
    }

    void TurnTowardsToThePlayer()
    {
        //Kendi etrafında dönsün
        Vector3 relativePos = Player.transform.position - transform.position;
        //turn towards to the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relativePos, Vector3.up), 0.2f);
    }
}
