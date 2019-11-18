using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialControl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject knight;
    GameObject mage;
    public GameObject teleport;
    public Transform spwan;
    public GameObject enemy;
    public GameObject escort;
    public GameObject[] players;
    public float lastestTime;
    public int threshold;
    public GameObject Rabbit;
    public int enemyCount;
    public int knightHealth;
    DialogSystem ds;
    NPC npc;
    float timer;
    int canGenerate;
    Vector3 spawnPos;
    bool firstTime;
    bool firstInitial;

    [TextArea(5, 10)]
    public string[] initailSentences;
    public string[] secondSentences;
    public string[] thirdSentences;
    bool knightReady = false;
    bool mageReady = false;


    void Start()
    {
        knight = GameObject.Find("Knight");
        mage = GameObject.Find("Mage");
        spawnPos = this.transform.position;
        canGenerate = 0;
        threshold = 50;
        knight.GetComponent<Health>().ModifyHealth(-90);
        ds = GetComponent<DialogSystem>();
        npc = Rabbit.GetComponent<NPC>();
        timer = 0;
        lastestTime = 4f;
        firstTime = true;
        firstInitial = true;

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        knightHealth = knight.GetComponent<Health>().currentHealth;
        if (knightHealth <= threshold && firstInitial)
        {
            npc.presentDialog(initailSentences);
            firstInitial = false;
        }
        if (knightHealth >= 100 && firstTime)
        {
            npc.presentDialog(secondSentences);
            canGenerate = 1;
            firstTime = false;
            Instantiate(escort, spwan.position, Quaternion.identity);
        }
        if (canGenerate == 1)
        {
            if (enemyCount > 10)
            {
                canGenerate = 2;
            }
            timer += Time.deltaTime;
            if (timer >= lastestTime)
            {
                int index = Random.Range(0, 2);
                GameObject newEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
                newEnemy.GetComponent<NavMeshAgent>().Warp(spawnPos);
                if (index != 0)
                {
                    newEnemy.GetComponent<EnemyControl>().target = GameObject.Find("Knight");
                }
                else
                {
                    newEnemy.GetComponent<EnemyControl>().target = GameObject.Find("Mage");
                }

                timer = 0;
            }
        }

        if (enemyCount <= 0 && canGenerate == 2)
        {
            npc.presentDialog(thirdSentences);
            Instantiate(teleport, this.transform.position, Quaternion.identity);
        }

        if (mageReady && knightReady)
        {
            SceneManager.LoadScene("mainScene3");
        }


    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Knight")
        {
            knightReady = true;
        }

        if (other.gameObject.name == "Mage")
        {
            mageReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Knight")
        {
            knightReady = false;
        }

        if (other.gameObject.name == "Mage")
        {
            mageReady = false;
        }
    }
}
