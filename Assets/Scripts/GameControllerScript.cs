using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject Player;
    private static float HP;
    private static int HealthAmount = 30;

    public GameObject HPBar;
    public Text LevelTxt;
    public Text EnemiesLeftText;
    private bool levelchange = false;
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
    }

    public void StartTheGame()
    {
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

        /**** Kalan Enemy Sayısı Hesaplanıyor 0 dan küçükse win ekranı geliyor ya da boss *****/
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

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void LevelScaling()
    {
        /**** DAHA BITMEDI ****/
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
        /**** DAHA BITMEDI ****/
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
        PauseGame();
        //show Score
        //oyunu dondur
    }

    private void WinLevelMenu()
    {
        PanelWinLevel.SetActive(true);
        PauseGame();
        //show Score
        //oyunu dondur
    }

    public void LevelContinueButton()
    {
        PanelWinLevel.SetActive(false);
        levelchange = true;
        currentLevel++;
        saveLevel(currentLevel);
        LevelScaling();
        ResumeGame();
        //oyunun time scale başlat
    }

    public void LevelRetryButton()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in enemies)
        {
            Destroy(gameObject);
        }

        GameObject[] potions = GameObject.FindGameObjectsWithTag("Health Potion");
        foreach (GameObject gameObject in potions)
        {
            Destroy(gameObject);
        }

        PlayerScript.HP = 100;
        EnemiesLeftText.text = "ENEMIES LEFT " + MaxNumofEnemies;
        PanelGameOver.SetActive(false);
        ResumeGame();
        StartTheGame();

        //Sahnedeki o andaki tüm robotlar potionlar yok edilmeli ( Eğer kalıyorsa )
        //oyunun time scale başlat
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

    public static void HitHealthPotion()
    {
        if(PlayerScript.HP <= (100 - HealthAmount))
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
        saveLevel(1);
        currentLevel = 1;
    }
}
