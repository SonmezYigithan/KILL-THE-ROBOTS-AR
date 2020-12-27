using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject Player;
    private static float HP;

    public Text HPbarText; //Change it later
    public Text LevelTxt;

    int currentLevel;
    void Start()
    {
        //LOAD SYSTEM
        if (!PlayerPrefs.HasKey("Level"))
        {
            saveLevel(0); //INITIALLY SET LEVEL TO 0 WHEN BEGINING OF THE GAME
            Debug.Log("Level initially is set to 0");
            LevelTxt.text = "Level 0";
        }
        else
        {
            currentLevel = getLevel();
            Debug.Log("Level is set to " + currentLevel);
        }
        
    }

    void Update()
    {
        //Handle Player HP Bar
        HP = PlayerScript.HP; //get HP value from PlayerScript
        HPbarText.text = HP.ToString();




    }

    public void saveLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        Debug.Log("Level:" + level);
    }

    public int getLevel()
    {
        return PlayerPrefs.GetInt("Level");
    }

}
