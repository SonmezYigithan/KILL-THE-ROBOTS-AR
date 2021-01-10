using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1Script : MonoBehaviour
{
    // BOSS 1
    // Boss canı handle la
    // belli süre aralıklarıyla ataş edecek 
    // random animasyonlar olacak

    public static float Boss1_HP_First = 500;
    public static float Boss1_HP = 500;

    //HP BAR
    private float maxHP;
    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        maxHP = Boss1_HP;
    }

    void Update()
    {
        if (healthBarUI)
        {
            EnemyHealthBarHandle();
        }

        if (Boss1_HP <= 0)
        {
            Debug.Log("Boss1 has Died");
            
            Destroy(gameObject);
        }

    }

    float CalculateHealth()
    {
        return Boss1_HP / maxHP;
    }

    void EnemyHealthBarHandle()
    {
        slider.value = CalculateHealth();

        if (Boss1_HP < maxHP)
        {
            healthBarUI.SetActive(true);
        }
        if (Boss1_HP <= 0)
        {
            Destroy(gameObject);
        }
        if (Boss1_HP > maxHP)
        {
            Boss1_HP = maxHP;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Boss OnDestroy"+ Boss1_HP);
        Boss1_HP = 100;
        GameControllerScript.IsBossDead = true;
    }
}
