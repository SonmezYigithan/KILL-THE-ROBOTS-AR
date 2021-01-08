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

    public GameObject PanelGameOver;
    public GameObject PanelWinLevel;

    /**** BOSS *****/
    int currentLevel;
    int currentBOSS;
    public GameObject BOSS1;
    public GameObject BOSS2;
    bool spawnboss;
    
    /***** SPAWN SCRIPT ******/
    public GameObject spawnScriptObj;
    SpawnTheRobots SpawnScript;
    [Tooltip("Her Spawn arası bekleme süresi")]
    [SerializeField] private float waitTime = 3;
    [Tooltip("Toplam Enemy Sayısı")]
    [SerializeField] private int MaxNumofEnemies;
    [Tooltip("Her waitTime değeri arasındaki spawnlanacak enemy sayısı")]
    [SerializeField] private int spawnAtATime; // mesela üst üste 4 enemy doğacak sonra belli bir saniye bekleyecek

    public static int EnemiesKilled = 0; // Level geçildiğinde sıfırla


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
            spawnboss = false;
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
        HPBar.GetComponent<Image>().fillAmount = HP / 100;

        /***** Handle Enemies Left Text FOR DEBUG ******/
        EnemiesLeftText.text = "ENEMIES LEFT " + (MaxNumofEnemies - EnemiesKilled - PlayerScript.DamagedCount).ToString();

        /***** Handle Level Text ******/
        LevelTxt.text = "Level " + currentLevel.ToString();

        /**** Kalan Enemy Sayısı Hesaplanıyor 0 dan küçükse win ekranı geliyor ya da boss *****/
        if ((MaxNumofEnemies - EnemiesKilled - PlayerScript.DamagedCount) <= 0)
        {
            if (spawnboss)
            {
                SpawnBoss();
            }
            else
            {
                WinLevelMenu();
            }

        }

        if (HP <= 0)
        {
            GameOverMenu();
        }

        /***** SPAWN ROBOTS *****/
        if (levelchange)
        {
            SpawnRobots();
        }
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
        if (currentBOSS == 1)
        {
            Instantiate(BOSS1, Player.transform.position + new Vector3(0, 0, 3f), Player.transform.rotation);
            spawnboss = false;
        }
        else if (currentBOSS == 2)
        {
            //Instantiate(BOSS2, Player.transform.position + new Vector3(0, 0, 3f), Player.transform.rotation);
        }
    }

    private void GameOverMenu()
    {
        PanelGameOver.SetActive(true);
        //show Score
        //oyunu dondur
    }

    private void WinLevelMenu()
    {
        PanelWinLevel.SetActive(true);
        //show Score
        //oyunu dondur
    }

    public void LevelContinueButton()
    {
        EnemiesKilled = 0;
        PlayerScript.DamagedCount = 0;

        PanelWinLevel.SetActive(false);
        levelchange = true;
        currentLevel++;
        saveLevel(currentLevel);
        LevelScaling();
        //oyunun time scale başlat
    }

    public void LevelRetryButton()
    {
        PanelGameOver.SetActive(false);
        PlayerScript.HP = 100;
        SceneManager.LoadScene("Level"); // bu kötü bir yöntem
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
        if (PlayerScript.HP <= (100 - HealthAmount))
        {
            PlayerScript.HP += HealthAmount;
        }
        else
        {
            PlayerScript.HP = 100;
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
        saveLevel(1);
        currentLevel = 1;
    }
}
