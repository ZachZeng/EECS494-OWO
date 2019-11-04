using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    // Start is called before the fir
    public Transform[] targetTrans;
    public GameObject ar;
    public GameObject spawnPos;
    int randomIdx;
    float timer1 = 0;
    float timer2 = 0;
    public float refreshPosTime = 10f;
    public float refreshArrTime = 0.5f;
    void Start()
    {

        randomIdx = Random.Range(0, targetTrans.Length);
    }

    // Update is called once per frame
    void Update()
    {
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer2 >= refreshArrTime)
        {
            GameObject arrow = Instantiate(ar, spawnPos.transform.position, Quaternion.identity);
            randomIdx = Random.Range(0, targetTrans.Length);
            arrow.GetComponent<ArrowControl>().target = targetTrans[randomIdx];
            timer2 = 0;
        }

    }
}
