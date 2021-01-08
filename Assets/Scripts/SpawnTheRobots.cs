﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnTheRobots : MonoBehaviour
{
    [SerializeField] GameObject RobotPrefab;
    [SerializeField] GameObject HealthPotionPrefab;
    [SerializeField] GameObject ARCamera;
    [SerializeField] float Distance;
    public static int currentNumberOfEnemies;
    public static List<GameObject> Robots = new List<GameObject>();
    private List<string> InstatiatePosition;
    public int HealthPotionSpawnPercentage;

    

    private bool startspawnbool;


    private void SpawnRobot(string XZ, float distance)
    {
        float x = 0;
        float y = ARCamera.transform.position.y - 0.5f;
        float z = 0;

        if (XZ == "PositiveX_PositiveZ")
        {
            x = Random.Range(0.0f, 3.0f);
            z = ARCamera.transform.position.z + (distance - x);
        }
        else if (XZ == "NegativeX_PositiveZ")
        {
            x = Random.Range(-3.0f, 0.0f);
            z = ARCamera.transform.position.z + (distance + x);
        }
        else if (XZ == "NegativeX_NegativeZ")
        {
            x = Random.Range(-3.0f, 0.0f);
            z = ARCamera.transform.position.z + ((distance * -1) - x);
        }
        else if (XZ == "PositiveX_NegativeZ")
        {
            x = Random.Range(0.0f, 3.0f);
            z = ARCamera.transform.position.z + ((distance * -1) + x);
        }

        x += ARCamera.transform.position.x;
        Vector3 robotPosition = new Vector3(x, y, z);
        GameObject robot = Instantiate(RobotPrefab, robotPosition, Quaternion.identity);
        Robots.Add(robot);

    }

    public void SpawnHealthPotion(Transform transform)
    {
        if(Random.Range(1, 100) <= HealthPotionSpawnPercentage)
        {
            Vector3 healthPotionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject healthPotion = Instantiate(HealthPotionPrefab, healthPotionPosition, Quaternion.identity);
        }
    }

    private IEnumerator StartSpawning(float waitTime, int MaxNumofEnemies, int spawnAtATime)
    {
        // NumofEnemis Sayısı kadar total robot spawnlar
        // spawnAtATime her waitTime da spawnlanacak robot sayısı
        //Robots = new List<GameObject>();
        for (int i = 0; i < MaxNumofEnemies / spawnAtATime; i++)
        {
            for (int j = 0; j < spawnAtATime; j++) // spawnAtATime = 4
            {
                //get random position
                var list = new List<string> { "PositiveX_PositiveZ", "NegativeX_PositiveZ", "NegativeX_NegativeZ", "PositiveX_NegativeZ" };
                int randindex = Random.Range(0, list.Count);

                SpawnRobot(list[randindex], Distance);
            }
            yield return new WaitForSeconds(waitTime);
        }
        //Robots.Clear(); //clears list
    }

    public void StartSpawnCoroutine(int MaxNumofEnemies, float waitTime, int spawnAtATime)
    {
        currentNumberOfEnemies = MaxNumofEnemies;
        StartCoroutine(StartSpawning(waitTime, currentNumberOfEnemies, spawnAtATime));
    }

    public static void KillTheRobotsCheatingButton()
    {
        foreach (GameObject robot in Robots)
        {
            Destroy(robot);
            GameControllerScript.EnemiesKilled++;
        }
    }

}
