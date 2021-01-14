using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlace : MonoBehaviour
{
    public GameObject objectToSpawn;
    private PlacementIndicator placementIndicator;

    public GameObject ObjectSpawner;
    public GameObject PlacementIndicator;
    public GameObject IngamePanel;
    public GameObject Pistol;
    public GameObject TapToPlacePanel;
    public GameObject GameController;
    private GameControllerScript script;

    private void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        script = GameController.GetComponent<GameControllerScript>();
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            GameObject obj = Instantiate(objectToSpawn,
                placementIndicator.transform.position, placementIndicator.transform.rotation);

            //Start The Game
            //objectSpawnerı kapat
            //placement indicator kapat
            //taptoplace Panel Kapat

            
            ObjectSpawner.SetActive(false);
            PlacementIndicator.SetActive(false);
            TapToPlacePanel.SetActive(false);

            Pistol.SetActive(true);
            IngamePanel.SetActive(true);
            script.StartTheLevel();

        }
    }

    private void StartTheLevel()
    {
        script.StartTheLevel();
    }





}
