﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject Player;
    private static float HP;

    public GameObject HPBar;
    public Text LevelTxt;
    public Text EnemiesLeftText;
    private bool levelchange = true;
    int currentLevel;
    int currentBOSS;

    public GameObject PanelGameOver;
    public GameObject PanelWinLevel;

    /***** SPAWN SCRIPT ******/
    public GameObject spawnScriptObj;
    SpawnTheRobots SpawnScript;
    [Tooltip("Her Spawn arası bekleme süresi")]
    [SerializeField] private float waitTime = 3;
    [Tooltip("Toplam Enemy Sayısı")]
    [SerializeField] private int MaxNumofEnemies;
    [Tooltip("Her waitTime değeri arasındaki spawnlanacak enemy sayısı")]
    [SerializeField] private int spawnAtATime; // mesela üst üste 4 enemy doğacak sonra belli bir saniye bekleyecek



    
    void Start()
    {
        //spawnScript Instance oluştur
        SpawnScript = spawnScriptObj.GetComponent<SpawnTheRobots>();

        //LOAD SYSTEM
        if (!PlayerPrefs.HasKey("Level"))
        {
            currentLevel = 1;
            saveLevel(1); //INITIALLY SET LEVEL TO 1 WHEN BEGINING OF THE GAME
            Debug.Log("Level initially is set to 1");
            LevelTxt.text = "Level 1";
            currentBOSS = 1;
            SpawnRobots();

        }
        else
        {
            currentLevel = getLevel();
            Debug.Log("Level is set to " + currentLevel);

            SpawnRobots();
        }


    }

    void Update()
    {
        /***** Handle Player HP Bar ******/
        HP = PlayerScript.HP;

        /***** Handle Enemies Left Text FOR DEBUG ******/
        EnemiesLeftText.text = "ENEMIES LEFT "+ (MaxNumofEnemies - ShootingScript.EnemiesKilled - PlayerScript.DamagedCount ).ToString();

        /***** Handle Level Text ******/
        LevelTxt.text = "Level "+currentLevel.ToString();

        if ((MaxNumofEnemies - ShootingScript.EnemiesKilled - PlayerScript.DamagedCount) <= 0)
        {
            SpawnBoss();
            WinLevelMenu();
        }

        if ( HP <= 0)
        {
            GameOverMenu();
        }

        
        HPBar.GetComponent<Image>().fillAmount = HP / 100;

        /***** SPAWN ROBOTS *****/
        if (levelchange)
        {
            SpawnRobots();
        }
    }

    private void LevelScaling()
    {
        if (getLevel() == 2)
        {
            MaxNumofEnemies = 40;
            currentBOSS = 2;
        }
        else if (getLevel() == 3)
        {
            MaxNumofEnemies = 50;
            spawnAtATime = 5;
            currentBOSS = 3;
        }
    }

    private void SpawnBoss()
    {
        if( currentBOSS == 1 )
        {
            //Spawn BOSS1
        }
        else if (currentBOSS == 2 )
        {
            //Spawn BOSS2
        }
    }

    private void GameOverMenu()
    {
        PanelGameOver.SetActive(true);
        //show Score
    }

    private void WinLevelMenu()
    {
        PanelWinLevel.SetActive(true);
        //show Score
    }

    public void LevelContinueButton()
    {
        PanelWinLevel.SetActive(false);
        levelchange = true;
        currentLevel++;
        saveLevel(currentLevel);
        LevelScaling();
    }

    public void LevelRetryButton()
    {
        PanelGameOver.SetActive(false);
        PlayerScript.HP = 100;
        SceneManager.LoadScene("Level");
    }

    private void SpawnRobots()
    {
        levelchange = false; 
        SpawnScript.StartSpawnCoroutine(MaxNumofEnemies, waitTime, spawnAtATime);
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

    /**************** DEBUG FUNCTIONS ***********/


    public void HealEnemyButton()
    {
        //FOR DEBUG
        PlayerScript.HP = 100;
    }

    public void ResetLevelButton()
    {
        //FOR DEBUG
        saveLevel(1);
        currentLevel = 1;
    }
}
