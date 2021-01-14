using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnTheRobots : MonoBehaviour
{
    [SerializeField] GameObject RobotPrefab;
    [SerializeField] GameObject HealthPotionPrefab;
    public GameObject[] BossPrefabs;
    public GameObject Portal;
    public GameObject ARCamera;
    [SerializeField] float Distance;
    public static int currentNumberOfEnemies;
    public static List<GameObject> Robots = new List<GameObject>();
    private List<string> InstatiatePosition;
    public int HealthPotionSpawnPercentage;

    private void SpawnRobot(string XZ, float distance, GameObject prefab)
    {
        float x = 0;
        float y = ARCamera.transform.position.y - 0.5f;
        float z = 0;

        if (XZ == "PositiveX_PositiveZ")
        {
            x = Random.Range(0.0f, 3.0f);
            z = Portal.transform.position.z + (distance - x);
        }
        else if (XZ == "NegativeX_PositiveZ")
        {
            x = Random.Range(-3.0f, 0.0f);
            z = Portal.transform.position.z + (distance + x);
        }
        else if (XZ == "NegativeX_NegativeZ")
        {
            x = Random.Range(-3.0f, 0.0f);
            z = Portal.transform.position.z + ((distance * -1) - x);
        }
        else if (XZ == "PositiveX_NegativeZ")
        {
            x = Random.Range(0.0f, 3.0f);
            z = Portal.transform.position.z + ((distance * -1) + x);
        }

        x += Portal.transform.position.x;
        Vector3 robotPosition = new Vector3(x, y, z);
        GameObject robot = Instantiate(prefab, robotPosition, Quaternion.identity);
        Robots.Add(robot);

    }

    public void SpawnHealthPotion(Transform transform)
    {
        if (Random.Range(1, 100) <= HealthPotionSpawnPercentage)
        {
            Vector3 healthPotionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject healthPotion = Instantiate(HealthPotionPrefab, healthPotionPosition, Quaternion.identity);
        }
    }

    private IEnumerator StartSpawningRobots(float waitTime, int MaxNumofEnemies, int spawnAtATime)
    {
        // NumofEnemis Sayısı kadar total robot spawnlar
        // spawnAtATime her waitTime da spawnlanacak robot sayısı

        int loopNum = MaxNumofEnemies / spawnAtATime;

        int mod = MaxNumofEnemies % spawnAtATime;

        for (int i = 0; i < loopNum; i++)
        {
            SpawnAtATimeFunc(spawnAtATime);
            yield return new WaitForSeconds(waitTime);
        }

        if(mod > 0)
        {
            SpawnAtATimeFunc(mod);
        }
    }

    private void SpawnAtATimeFunc(int spawnAtATime)
    {
        for (int j = 0; j < spawnAtATime; j++)
        {
            //get random position
            var list = new List<string> { "PositiveX_PositiveZ", "NegativeX_PositiveZ", "NegativeX_NegativeZ", "PositiveX_NegativeZ" };
            int randindex = Random.Range(0, list.Count);

            SpawnRobot(list[randindex], Distance, RobotPrefab);
        }
    }

    private IEnumerator SpawnBoss(float waitTime, int bossIndex)
    {
        yield return new WaitForSeconds(waitTime);

        var list = new List<string> { "PositiveX_PositiveZ", "NegativeX_PositiveZ", "NegativeX_NegativeZ", "PositiveX_NegativeZ" };
        int randindex = Random.Range(0, list.Count);

        SpawnRobot(list[randindex], Distance, BossPrefabs[bossIndex]);
    }

    public void StartSpawnCoroutineRobots(int MaxNumofEnemies, float waitTime, int spawnAtATime)
    {
        currentNumberOfEnemies = MaxNumofEnemies;
        StartCoroutine(StartSpawningRobots(waitTime, currentNumberOfEnemies, spawnAtATime));
    }

    public void StartSpawnCoroutineBoss(float waitTime, int bossIndex)
    {
        StartCoroutine(SpawnBoss(waitTime, bossIndex));
    }

    public static void KillTheRobotsCheatingButton()
    {
        for (int i = 0; i < Robots.Count; i++)
        {
            Destroy(Robots[i].gameObject);
            GameControllerScript.EnemiesKilled++;
        }
        Robots.Clear();
    }

    public void StopCoroutines()
    {
        StopCoroutine("StartSpawningRobots");
    }

}
