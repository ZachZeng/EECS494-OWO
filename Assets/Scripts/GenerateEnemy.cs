using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateEnemy : MonoBehaviour
{

    public GameObject enemy;
    public float lastestTime;
    public bool canGenerate;
    public float timer;



    Vector3 spawnPos;
    int enemyCount;
    // Start is called before the first frame update
    private void Start()
    {
        enemyCount = 0;
        spawnPos = this.transform.position;
        timer = 0;
        lastestTime = 3f;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.isGameBegin)
        {
            canGenerate &= enemyCount < 10;
            timer += Time.deltaTime;
            if (timer >= lastestTime && canGenerate)
            {
                GameObject newEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
                newEnemy.GetComponent<NavMeshAgent>().Warp(spawnPos);
                enemyCount++;
                timer = 0;
            }
        }
    }
}
