using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject Player;
    private static float HP;
    private static int HealthAmount = 20;

    public GameObject HPBar;
    public Text LevelTxt;
    public Text EnemiesLeftText;
    public int RobotIncreasePercentage;
    public int BossHPIncreasePercentage;
    public int TotalLevelNumber;
    int currentLevel;
    int bossIndex = 0;
    private int MaxNumofEnemies;
    private bool SpawnBossBool = true;

    public GameObject PanelGameOver;
    public GameObject PanelWinLevel;
    public GameObject PanelWon;
    public GameObject PanelMenu;
    public GameObject PanelScan;
    public GameObject PanelIngame;
    public string[] GameObjectTags;
    public GameObject pistol;

    /***** SPAWN SCRIPT ******/
    public GameObject spawnScriptObj;
    SpawnTheRobots SpawnScript;
    [Tooltip("Her Spawn arası bekleme süresi")]
    [SerializeField] private float waitTime = 3;
    [Tooltip("Toplam Enemy Sayısı")]
    [SerializeField] private int MaxNumofEnemiesFirst;
    [Tooltip("Her waitTime değeri arasındaki spawnlanacak enemy sayısı")]
    [SerializeField] private int spawnAtATime; // mesela üst üste 4 enemy doğacak sonra belli bir saniye bekleyecek

    public static int EnemiesKilled = 0;


    void Start()
    {
        //spawnScript Instance oluştur
        SpawnScript = spawnScriptObj.GetComponent<SpawnTheRobots>();
        MaxNumofEnemies = MaxNumofEnemiesFirst;

    }

    public void NewGame()
    {
        currentLevel = SetLevel(1);
        EnemiesKilled = 0;
        PlayerScript.DamagedCount = 0;
        PlayerScript.HP = 100;
        Boss1Script.Boss1_HP = 500;
        DestroyGameObjects();
        DestroyPortal();
        PanelScan.SetActive(true);
    }

    public void StartTheLevel()
    {
        SpawnBossBool = true;
        DestroyGameObjects();
        ResumeGame();
        EnemiesKilled = 0;
        PlayerScript.DamagedCount = 0;
        PlayerScript.HP = 100;

        //LOAD SYSTEM
        if (!PlayerPrefs.HasKey("Level") || GetLevel() == 1)
        {
            currentLevel = SetLevel(1); //INITIALLY SET LEVEL TO 1 WHEN BEGINING OF THE GAME
            LevelTxt.text = "Level 1";
            LevelScaling();
            SpawnRobots();
        }
        else
        {
            currentLevel = GetLevel();
            LevelTxt.text = "Level " + currentLevel.ToString();

            LevelScaling();
            SpawnRobots();
        }
    }

    void Update()
    {
        /****** HANDLE BACK BUTTON ******/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PanelMenu.SetActive(true);
            PanelGameOver.SetActive(false);
            PanelWinLevel.SetActive(false);
            PanelWon.SetActive(false);
            PanelScan.SetActive(false);
            PanelIngame.SetActive(false);
            pistol.SetActive(false);

            EnemiesKilled = 0;
            PlayerScript.DamagedCount = 0;
            PlayerScript.HP = 100;
            Boss1Script.Boss1_HP = 500;
            DestroyPortal();

            SpawnScript.StopCoroutines();

            DestroyGameObjects();
            PauseGame();
            
        }

        /***** Handle Player HP Bar ******/
        HP = PlayerScript.HP;

        /***** Handle Enemies Left Text FOR DEBUG ******/

        if (Time.timeScale == 1)
        {
            EnemiesLeftText.text = "ENEMIES LEFT " + (MaxNumofEnemies - EnemiesKilled - PlayerScript.DamagedCount).ToString();
        }

        /**** Kalan Enemy Sayısı Hesaplanıyor 0 dan küçükse win ekranı geliyor ya da boss *****/
        if ((MaxNumofEnemies - EnemiesKilled - PlayerScript.DamagedCount) <= 0)
        {
            if (currentLevel == 1)
            {
                WinLevelMenu();
            }
            else
            {
                if (SpawnBossBool)
                {
                    MaxNumofEnemies++;
                    SpawnBoss();
                    SpawnBossBool = false;
                }
            }
        }

        if (Boss1Script.Boss1_HP <= 0)
        {
            MaxNumofEnemies--;
            SpawnBossBool = true;
            BossHPScaling();
            WinLevelMenu();
        }

        if (HP <= 0 && Time.timeScale == 1)
        {
            GameOverMenu();
        }

        HPBar.GetComponent<Image>().fillAmount = HP / 100;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void LevelScaling()
    {
        MaxNumofEnemies = MaxNumofEnemiesFirst;
        for (int i = 1; i < currentLevel; i++)
        {
            MaxNumofEnemies = (int)(MaxNumofEnemies * ((float)(100 + RobotIncreasePercentage) / 100));
        }
    }

    private void BossHPScaling()
    {
        Boss1Script.Boss1_HP = Boss1Script.Boss1_HP_First;
        for (int i = 1; i < currentLevel; i++)
        {
            Boss1Script.Boss1_HP = (int)(Boss1Script.Boss1_HP * ((float)(100 + BossHPIncreasePercentage) / 100));
        }
    }
    private void GameOverMenu()
    {
        PauseGame();
        //currentLevel = SetLevel(1);
        PanelGameOver.SetActive(true);
    }

    private void WinLevelMenu()
    {
        PauseGame();
        EnemiesKilled = 0;
        PlayerScript.DamagedCount = 0;
        PlayerScript.HP = 100;
        SetLevel(++currentLevel);
        PanelWinLevel.SetActive(true);
    }

    private void PanelWonMenu()
    {
        //currentLevel = SetLevel(1);
        PanelWon.SetActive(true);
    }

    public void LevelContinueButton()
    {
        PanelWinLevel.SetActive(false);
        ResumeGame();
        StartTheLevel();
    }

    public void PlayAgainButton()
    {
        /*************************** DEVAM ETMEK IÇIN REKLAM IZLE ***************************/
        //currentLevel = SetLevel(1);
        //bossIndex = 0;
        EnemiesKilled = 0;
        PlayerScript.DamagedCount = 0;
        PlayerScript.HP = 100;
        Boss1Script.Boss1_HP = 500;
        PanelGameOver.SetActive(false);
        ResumeGame();
        StartTheLevel();
    }

    public void ResumeButton()
    {
        currentLevel = GetLevel();
        //bossIndex = currentLevel - 1;
        PanelMenu.SetActive(false);
        PanelScan.SetActive(true);
        DestroyPortal();
        PanelScan.SetActive(true);
    }

    public void MenuButton()
    {
        DestroyGameObjects();
    }

    private void DestroyGameObjects()
    {
        foreach (string tag in GameObjectTags)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject gameObject in gameObjects)
            {
                Destroy(gameObject);
            }
        }
    }

    private void SpawnRobots()
    {
        SpawnScript.StartSpawnCoroutineRobots(MaxNumofEnemies, waitTime, spawnAtATime);
    }

    private void SpawnBoss()
    {
        SpawnScript.StartSpawnCoroutineBoss(waitTime, bossIndex);
    }

    public int SetLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        return level;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level");
    }

    public static void HitHealthPotion()
    {
        if (PlayerScript.HP <= (100 - HealthAmount))
        {
            PlayerScript.HP += HealthAmount;
        }
        else
        {
            PlayerScript.HP = 100;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DestroyPortal()
    {
        //search
        GameObject portal = GameObject.FindGameObjectWithTag("Portal");
        if (portal)
        {
            Destroy(portal);
        }
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
        SetLevel(1);
        currentLevel = 1;
    }
}
