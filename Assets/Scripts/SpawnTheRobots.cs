using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTheRobots : MonoBehaviour
{
    [SerializeField] GameObject RobotPrefab;
    [SerializeField] GameObject ARCamera;
    [SerializeField] float Distance;
    private List<GameObject> Robots;

    // Start is called before the first frame update
    void Start()
    {
        Robots = new List<GameObject>();
        SpawnRobot("PositiveX_PositiveZ", Distance);
        SpawnRobot("NegativeX_PositiveZ", Distance);
        SpawnRobot("NegativeX_NegativeZ", Distance);
        SpawnRobot("PositiveX_NegativeZ", Distance);
    }

    private void SpawnRobot(string XZ, float distance)
    {
        float x = 0;
        float y = ARCamera.transform.position.y;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
