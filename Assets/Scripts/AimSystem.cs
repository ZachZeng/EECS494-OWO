using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject escort_object;
    public GameObject mage;
    public GameObject warrior;
    public int escort_aim = 100;
    public int mage_aim_change_per = 10;
    public int warrior_aim_change_per = 20;
    public int mage_aim_cool_per = 8;
    public int warrior_aim_cool_per = 5;

    public HashSet<string> magesWeapons;
    public HashSet<string> warriorWeapons;

    float cooldownTimer = 0;
    float cooldwon = 1.0f;
    int[] mage_aim = new[] { 0, 0, 0 }; // aim, index, control
    int[] warrior_aim = new[] { 0, 0, 0 };

    EnemyControl ec;
    GameObject target;

    private void OnCollisionEnter(Collision collision)
    {
        string cur_tag = collision.gameObject.tag;
        if (magesWeapons.Contains(cur_tag))
        {
            mage_aim[0] += mage_aim_change_per;
        }
        else if (warriorWeapons.Contains(cur_tag))
        {
            warrior_aim[0] += warrior_aim_change_per;
        }
    }

    private void Start()
    {
        ec = GetComponent<EnemyControl>();
        ec.target = escort_object;
    }

    public void Tank(float time)
    {
        warrior_aim[2] = 1;
        StartCoroutine(Wait(time));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        warrior_aim[2] = 0;
    }


    void UpdateTarget()
    {
        if (mage_aim[0] > warrior_aim[0] && mage_aim[0] > escort_aim)
        {
            ec.target = mage;
        }
        else if (mage_aim[0] <= warrior_aim[0] && warrior_aim[0] > escort_aim)
        {
            ec.target = warrior;
        }
        else
        {
            ec.target = escort_object;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (warrior_aim[2] == 1)
        {
            ec.target = warrior;
        }
        else
        {
            UpdateTarget();
        }

        if (cooldownTimer > cooldwon)
        {
            CoolDown(warrior_aim, warrior_aim_cool_per);
            CoolDown(mage_aim, mage_aim_cool_per);
            cooldownTimer = 0f;
        }
        else
        {
            cooldownTimer += Time.deltaTime;
        }

    }

    void CoolDown(int[] a, int cool)
    {
        if (a[0] >= 0)
        {
            a[0] -= cool;
        }
        else
        {
            a[0] = 0;
        }
    }
}
