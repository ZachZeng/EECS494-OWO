using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateEnemy : MonoBehaviour
{

    public GameObject enemy;
    public float lastestTime;
    public GameObject[] targets;
    public bool canGenerate;


    Vector3 spawnPos;
    int index;
    int targetsNum;
    int enemyCount;
    // Start is called before the first frame update
    private void Start()
    {
        enemyCount = 0;
        targetsNum = targets.Length;
        spawnPos = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        canGenerate &= enemyCount < 10;

        if (Time.time >= lastestTime && canGenerate)
        {
            index = Random.Range(0, targetsNum - 1);
            GameObject newEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
            newEnemy.GetComponent<NavMeshAgent>().Warp(spawnPos);
            newEnemy.GetComponent<EnemyControl>().target = targets[index];
            lastestTime = Time.time + lastestTime;
        }
    }
}
