using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenuScript : MonoBehaviour
{
    public Button ButtonResume;
    public Button ButtonQuit;
    private Vector3 ButtonQuitPosition;
    public GameControllerScript GameControllerScript;

    void Start()
    {
        ButtonQuitPosition = new Vector3(ButtonQuit.transform.position.x, ButtonQuit.transform.position.y, ButtonQuit.transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        if(!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") == 1 || PlayerPrefs.GetInt("Level") > GameControllerScript.TotalLevelNumber)
        {
            ButtonQuit.transform.position = new Vector3(ButtonResume.transform.position.x, ButtonResume.transform.position.y, ButtonResume.transform.position.z);
            ButtonResume.gameObject.SetActive(false);
        }
        else
        {
            ButtonResume.gameObject.SetActive(true);
            ButtonQuit.transform.position = new Vector3(ButtonQuitPosition.x, ButtonQuitPosition.y, ButtonQuitPosition.z);
        }
    }
}
