using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRadarObject : MonoBehaviour
{
    public Image image;
    public bool IsBoss;

    // Start is called before the first frame update
    void Start()
    {
        Radar.RegisterRadarObjects(this.gameObject, image);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);
        if(IsBoss)
        {
            GameControllerScript.IsBossDead = true;
        }
    }
}
