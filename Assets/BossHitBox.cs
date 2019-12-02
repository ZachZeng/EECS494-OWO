using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider bc;
    bool spATK;
    GameObject boss;

    void Start()
    {
        bc = GetComponent<BoxCollider>();
        boss = GameObject.FindWithTag("Enemy_Boss");
    }

    // Update is called once per frame
    void Update()
    {
        spATK = boss.GetComponent<BossControl>().spATK;
        if (spATK)
        {
            bc.enabled = true;
        }
        else
        {
            bc.enabled = false;
        }
    }
}
