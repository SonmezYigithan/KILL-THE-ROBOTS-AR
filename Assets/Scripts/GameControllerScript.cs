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
    public int TotalLevelNumber;
    int currentLevel;
    int bossIndex = 0;
    private int MaxNumofEnemies;
    public static bool IsBossDead = false;

    public GameObject PanelGameOver;
    public GameObject PanelWinLevel;
    public GameObject PanelWon;
    public GameObject PanelMenu;
    public string[] GameObjectTags;

    /***** SPAWN SCRIPT ******/
    public GameObject spawnScriptObj;
    SpawnTheRobots SpawnScript;
    [Tooltip("Her Spawn arası bekleme süresi")]
    [SerializeField] private float waitTime = 3;
    [Tooltip("Toplam Enemy Sayısı")]
    [SerializeField] private int MaxNumofEnemiesFirst;
    [Tooltip("Her waitTime değeri arasındaki spawnlanacak enemy sayısı")]
    [SerializeField] private int spawnAtATime; // mesela üst üste 4 enemy doğacak sonra belli bir saniye bekleyecek




    void Start()
    {
        //spawnScript Instance oluştur
        SpawnScript = spawnScriptObj.GetComponent<SpawnTheRobots>();
        MaxNumofEnemies = MaxNumofEnemiesFirst;
    }

    public void StartTheLevel()
    {
        ResumeGame();
        ShootingScript.EnemiesKilled = 0;
        PlayerScript.DamagedCount = 0;
        PlayerScript.HP = 100;

        //LOAD SYSTEM
        if (!PlayerPrefs.HasKey("Level") || GetLevel() == 1)
        {
            currentLevel = SetLevel(1); //INITIALLY SET LEVEL TO 1 WHEN BEGINING OF THE GAME
            LevelTxt.text = "Level 1";
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
        /***** Handle Player HP Bar ******/
        HP = PlayerScript.HP;

        /***** Handle Enemies Left Text FOR DEBUG ******/
        EnemiesLeftText.text = "ENEMIES LEFT " + (MaxNumofEnemies - ShootingScript.EnemiesKilled - PlayerScript.DamagedCount).ToString();

        /**** Kalan Enemy Sayısı Hesaplanıyor 0 dan küçükse win ekranı geliyor ya da boss *****/
        if ((MaxNumofEnemies - ShootingScript.EnemiesKilled - PlayerScript.DamagedCount) <= 0)
        {
            if(currentLevel == 1)
            {
                WinLevelMenu();
            }
            else
            {
                SpawnBoss();
            }
        }

        if(IsBossDead)
        {
            IsBossDead = false;
            if (currentLevel < TotalLevelNumber)
            {
                bossIndex++;
                WinLevelMenu();
            }
            else
            {
                PanelWonMenu();
            }

        }

        if (HP <= 0)
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
        for (int i = 0; i < currentLevel - 1; i++)
        {
            MaxNumofEnemies = MaxNumofEnemiesFirst;
            MaxNumofEnemies = (int) (MaxNumofEnemies * ((float)(100 + RobotIncreasePercentage) / 100));
        }
    }

    private void GameOverMenu()
    {
        currentLevel = SetLevel(1);
        SetLevel(1);
        PanelGameOver.SetActive(true);
        //show Score
        //oyunu dondur
    }

    private void WinLevelMenu()
    {
        PauseGame();
        DestroyGameObjects();
        SetLevel(++currentLevel);
        PanelWinLevel.SetActive(true);
    }

    private void PanelWonMenu()
    {
        currentLevel = SetLevel(1);
        PanelWon.SetActive(true);
    }

    public void LevelContinueButton()
    {
        PanelWinLevel.SetActive(false);
        ResumeGame();
        StartTheLevel();
        //oyunun time scale başlat
    }

    public void PlayAgainButton()
    {
        currentLevel = SetLevel(1);
        bossIndex = 0;
        DestroyGameObjects();
        PanelGameOver.SetActive(false);
        ResumeGame();
        StartTheLevel();
    }

    public void ResumeButton()
    {
        currentLevel = GetLevel();
        bossIndex = currentLevel - 1;
        DestroyGameObjects();
        PanelMenu.SetActive(false);
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
