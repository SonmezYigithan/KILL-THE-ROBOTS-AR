using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    public GameObject visual;

    public GameObject tutorialwaitTxt;
    public GameObject tutorialTxt;

    void Awake()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        if (visual)
        {
            visual.SetActive(false);
            tutorialwaitTxt.SetActive(true);
        }
        else
        {
            visual = Instantiate(visual.transform.gameObject, transform.position, transform.rotation);
            visual.SetActive(false);
            tutorialwaitTxt.SetActive(true);
        }
    }

    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height/2),hits, TrackableType.Planes);
        
        if (hits.Count > 0)
        {
            tutorialwaitTxt.SetActive(false);
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            tutorialTxt.SetActive(true);

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }
                

        }
    }
}
