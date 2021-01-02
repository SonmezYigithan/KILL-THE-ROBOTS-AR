using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRadarObject : MonoBehaviour
{
    public Image image;
    public string type;
    SpawnTheRobots spawnTheRobots;

    // Start is called before the first frame update
    void Start()
    {
        Radar.RegisterRadarObjects(this.gameObject, image);
        spawnTheRobots = GetComponent<SpawnTheRobots>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);

        if (type == "robot")
        {
            spawnTheRobots.SpawnHealthPotion(this.gameObject.transform);
        }
        else if(type == "health potion")
        {
            GameControllerScript.HitHealthPotion();
        }
    }
}
