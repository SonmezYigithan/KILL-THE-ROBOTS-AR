using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    //Enemy kullanıcı pozisyonunu alıp ona doğru yönelicek

    private GameObject Player;
    private NavMeshAgent enemyNavMeshAgent;
    private Animator enemyAnimator;
    public float speed = 5f;
    public float HP;

    //HP BAR
    public float maxHP;
    public GameObject healthBarUI;
    public Slider slider;
   
    private EnemyScript enemyScript;

    //spawn
    public string type;

    GameObject spawnScriptObj;
    SpawnTheRobots SpawnScript;
    

    void Start()
    {
        //find gameobject with tag player
        Player = GameObject.FindGameObjectWithTag("Player");
        spawnScriptObj = GameObject.FindGameObjectWithTag("SpawnEdge");
        SpawnScript = spawnScriptObj.GetComponent<SpawnTheRobots>();

        enemyScript = GetComponent<EnemyScript>();
    }

    void Update()
    {
        MoveTowardsToThePlayer();
        if (healthBarUI)
        {
            EnemyHealthBarHandle();
        }
        
    }

    private void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);

        if (type == "robot")
        {
            SpawnScript.SpawnHealthPotion(this.gameObject.transform);
        }
        else if (type == "health potion")
        {
            GameControllerScript.HitHealthPotion();
        }
    }

    public void Die()
    {
        enemyNavMeshAgent.speed = 0f;
        enemyAnimator.SetTrigger("Die");
    }

    public void MakeDie()
    {
        enemyScript.Die();
        Destroy(gameObject, 3f);
    }

    void MoveTowardsToThePlayer()
    {
        
        float step = speed * Time.deltaTime; // calculate distance to move
        Vector3 relativePos = Player.transform.position - transform.position;
        //move towards to the player
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        //turn towards to the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relativePos, Vector3.up), 0.2f);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        //delete enemy object
        //OKAN, tam burda destroy edildikten sonra can paketi Instantiate edecek
        //random 0-100 random atıp 0 ile 30 arasında ise paket düşür
        if (collision.collider.name == "Player")
        {   

            Destroy(gameObject);
        }
        

        //play Player get hit animation

    }

    float CalculateHealth()
    {
        return HP / maxHP;
    }

    void EnemyHealthBarHandle()
    {
        slider.value = CalculateHealth();

        if (HP < maxHP)
        {
            healthBarUI.SetActive(true);
        }
        if (HP <= 0)
        {
            MakeDie();
            Destroy(gameObject);
        }
        if (HP > maxHP)
        {
            HP = maxHP;
        }
    }
}
