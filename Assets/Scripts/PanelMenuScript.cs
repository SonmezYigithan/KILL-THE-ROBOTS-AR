using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenuScript : MonoBehaviour
{
    public Button ButtonResume;
    public Button ButtonTutorial;
    public Button ButtonQuit;
    public Text TextPlay;
    private Vector3 ButtonTutorialPosition;
    private Vector3 ButtonQuitPosition;

    void Start()
    {
        ButtonTutorialPosition = new Vector3(ButtonTutorial.transform.position.x, ButtonTutorial.transform.position.y, ButtonTutorial.transform.position.z);
        ButtonQuitPosition = new Vector3(ButtonQuit.transform.position.x, ButtonQuit.transform.position.y, ButtonQuit.transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        if(!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") == 1)
        {
            TextPlay.text = "PLAY";
            ButtonTutorial.transform.position = new Vector3(ButtonResume.transform.position.x, ButtonResume.transform.position.y, ButtonResume.transform.position.z);
            ButtonQuit.transform.position = new Vector3(ButtonTutorialPosition.x, ButtonTutorialPosition.y, ButtonTutorialPosition.z);
            ButtonResume.gameObject.SetActive(false);
        }
        else
        {
            TextPlay.text = "NEW GAME";
            ButtonResume.gameObject.SetActive(true);
            ButtonTutorial.transform.position = new Vector3(ButtonTutorialPosition.x, ButtonTutorialPosition.y, ButtonTutorialPosition.z);
            ButtonQuit.transform.position = new Vector3(ButtonQuitPosition.x, ButtonQuitPosition.y, ButtonQuitPosition.z);
        }
    }
}
