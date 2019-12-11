﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    // Start is called before the first frame update
    public HashSet<Collider> encountered = new HashSet<Collider>();
    public float attackFreq;
    public float specialFrq;
    public float speed = 2.0f;
    public GameObject target;
    float specialTimer;
    float timer;
    Animator am;
    public bool isAttacking;
    public bool isTrapped;
    public bool spATK;
    public GameObject hitbox;
    Collider col;
    public int ATK;
    public bool meetBoss;
    public bool isBossDead;
    public ParticleSystem onGround;
    public GameObject bullet;
    Vector3 offset;
    SkinnedMeshRenderer srd;
    public GameObject mRender;
    public float frozenTime;
    public bool getAttacked;
    Material original;
    public Material flash;
    public Material frozen;
    public float attackDistance = 2.0f;
    public GameObject central;
    public float radious;
    public bool isgrounded;
    float oriSpeed;

    void Start()
    {
        gameObject.transform.position = new Vector3(76.5f, 30, 41);
        am = GetComponent<Animator>();
        col = hitbox.GetComponent<BoxCollider>();
        offset = new Vector3(0, 2f, -5f);
        srd = mRender.GetComponent<SkinnedMeshRenderer>();
        original = srd.material;
        target = GameObject.Find("Escort Object");
        isgrounded = false;
        oriSpeed = speed;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isTrapped |= other.gameObject.name == "CastRange(Clone)";
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player") && !isTrapped)
        {
            collision.gameObject.GetComponent<PlayerHealthControl>().getAttack(ATK);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(central.transform.position, transform.position) > radious)
        {
            target = GameObject.Find("Escort Object");
        }
        if (meetBoss)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(76.5f, 0.1f, 41), Time.deltaTime * 20);
            timer += Time.deltaTime;
            if (transform.position.y <= 0.2f)
            {
                isgrounded = true;
            }
            specialTimer += Time.deltaTime;
            if (!isTrapped && isgrounded)
            {
                Debug.Log("Move!");
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
                transform.LookAt(target.transform);
                am.SetBool("Walk Forward", true);
                if (Vector3.Distance(target.transform.position, transform.position) < attackDistance)
                {
                    am.SetBool("Walk Forward", false);
                    if (specialTimer >= specialFrq)
                    {
                        am.SetTrigger("Attack 02");
                        GameObject b = Instantiate(bullet, transform.position + offset, Quaternion.identity);
                        b.GetComponent<TowerBullet>().Curr_target = GameObject.Find("Escort Object").transform;
                        b.GetComponent<TowerBullet>().dmg = ATK;
                        specialTimer = 0f;
                    }
                    else
                    {
                        spATK = true;
                        am.SetTrigger("Attack 01");
                    }
                    timer = 0f;
                    LaunchAttack();
                    StartCoroutine(Wait());
                }
                    
            }
            JudageGetAttacked();
            JudgeTrapped();
        }
    }


    void JudgeTrapped()
    {
        if (isTrapped)
        {
            StartCoroutine(Frozen());
        }
    }


    void JudageGetAttacked()
    {
        if (getAttacked)
        {
            am.ResetTrigger("Attack 01");
            am.ResetTrigger("Attack 02");
            am.SetTrigger("Take Damage");
        }
    }

    IEnumerator Frozen()
    {
        srd.material = frozen;
        speed = 0;
        yield return new WaitForSeconds(frozenTime);
        isTrapped = false;
        srd.material = original;
        speed = oriSpeed;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        spATK = false;
    }



    void LaunchAttack()
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation);
        foreach (Collider c in cols)
        {
            if (c.gameObject.CompareTag("Player"))
            {
                if (!encountered.Contains(c))
                {
                    encountered.Add(c);
                    c.gameObject.GetComponent<PlayerHealthControl>().getAttack(ATK);
                }
            }
        }
    }
}
