using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject Player;
    private static float HP;

    public Text HPbarText; //Change it later
    void Start()
    {
        //set level and get level
        saveLevel(0); //TEST INITIALLY SET LEVEL TO 0 WHEN BEGINING OF THE GAME
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

}
