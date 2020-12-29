using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image Icon { get; set; }
    public GameObject Owner { get; set; }
}

public class Radar : MonoBehaviour
{
    public Transform PlayerPosition;
    [SerializeField] float MapScale;

    public static List<RadarObject> RadarObjects = new List<RadarObject>();

    public static void RegisterRadarObjects(GameObject gameObject, Image image)
    {
        Image icon = Instantiate(image);
        RadarObjects.Add(new RadarObject() { Owner = gameObject, Icon = icon });
    }

    public static void RemoveRadarObject(GameObject gameObject)
    {
        List<RadarObject> newList = new List<RadarObject>();

        for (int i = 0; i < RadarObjects.Count; i++)
        {
            if(RadarObjects[i].Owner == gameObject)
            {
                Destroy(RadarObjects[i].Icon);
                continue;
            }
            else
            {
                newList.Add(RadarObjects[i]);
            }

            RadarObjects.RemoveRange(0, RadarObjects.Count);
            RadarObjects.AddRange(newList);
        }
    }

    private void DrawRadarDots()
    {
        foreach (RadarObject radarObject in RadarObjects)
        {
            Vector3 radarPosition = (radarObject.Owner.transform.position - PlayerPosition.position);
            float distanceToObject = Vector3.Distance(PlayerPosition.position, radarObject.Owner.transform.position) * MapScale;
            float deltaY = Mathf.Atan2(radarPosition.x, radarPosition.z) * Mathf.Rad2Deg - 270 - PlayerPosition.eulerAngles.y;
            radarPosition.x = distanceToObject * Mathf.Cos(deltaY * Mathf.Deg2Rad) * -1;
            radarPosition.z = distanceToObject * Mathf.Sin(deltaY * Mathf.Deg2Rad);

            radarObject.Icon.transform.SetParent(this.transform);
            radarObject.Icon.transform.position = new Vector3(radarPosition.x, radarPosition.z, 0) + this.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawRadarDots();
    }
}
