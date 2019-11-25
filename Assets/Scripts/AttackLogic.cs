﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public HashSet<Collider> encountered = new HashSet<Collider>();
    Collider[] enemies;
    public GameObject hitbox;
    Health myHealth;
    Collider col;
    Rigidbody rb;
    public GameObject model;
    Renderer model_mr;
    public Material pureRed;
    public Material originalMaterial;
    public bool knocked_back = false;
    public ParticleSystem groundcrack;

    public int ATK;

    void Start()
    {
        col = hitbox.GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        myHealth = GetComponent<Health>();
        model_mr = model.GetComponent<Renderer>();
        originalMaterial = model_mr.material;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool LaunchAttack()
    {
        bool hitEnemy = false;
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation);
        foreach (Collider c in cols)
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                if (!encountered.Contains(c))
                {
                    encountered.Add(c);
                    c.gameObject.GetComponent<Health>().ModifyHealth(-ATK);
                    if(c.gameObject.name != "Obstacle_Road" && !c.gameObject.name.Contains("HM_cannon_1") && !c.gameObject.name.Contains("Toon") )
                    {
                        c.gameObject.GetComponent<AimSystem>().warrior_aim[0] += c.gameObject.GetComponent<AimSystem>().warrior_aim_change_per;
                        c.gameObject.GetComponent<EnemyControl>().StartKnockBack(gameObject);
                    }

                    if(c.gameObject.name == "Obstacle_Road")
                    {
                        c.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
                        c.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    }

                    hitEnemy = true;

                }
            }

        }
        return hitEnemy;
    }

    public bool LaunchAttack3()
    {
        bool hitEnemy = false;
        Collider[] cols = Physics.OverlapSphere(col.bounds.center, 1.5f);
        foreach (Collider c in cols)
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                if (!encountered.Contains(c))
                {
                    encountered.Add(c);
                    c.gameObject.GetComponent<Health>().ModifyHealth(-ATK - 10);
                    hitEnemy = true;
                    c.gameObject.GetComponent<EnemyControl>().StartKnockBack(gameObject);
                    if (c.gameObject.name == "Obstacle_Road")
                    {
                        c.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
                        c.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    }
                }


            }
        }
        return hitEnemy;
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Collider myCollider = collision.contacts[0].thisCollider;
    //        //Debug.Log(myCollider.tag);
    //        if (!knocked_back && myCollider.CompareTag("Player"))
    //        {
    //            //Debug.Log("enemy collided with me");
    //            //myHealth.ModifyHealth(collision.gameObject.GetComponent<Escort_Enemy_example>().EnemyDamage);
    //            //Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
    //            //Vector3 movedirection = (transform.position - enemyRB.transform.position).normalized;
    //            StartCoroutine(InvincibleFrame());
    //            StartCoroutine(Flash());
    //            //StartCoroutine(Knockback(rb, movedirection));
    //        }
    //    }
    //}

    IEnumerator InvincibleFrame()
    {
        knocked_back = true;
        yield return new WaitForSeconds(2f);
        knocked_back = false;
    }
    IEnumerator Flash()
    {
        while (knocked_back)
        {
            //Debug.Log("hello");
            model_mr.material = pureRed;
            yield return new WaitForSeconds(0.1f);
            model_mr.material = originalMaterial;
            yield return new WaitForSeconds(0.1f);
        }
        model_mr.material = originalMaterial;
    }
    /*IEnumerator Knockback(Rigidbody rigb, Vector3 movedirection)
    {
        Vector3 DesiredLocation = rigb.transform.position + movedirection * 100;
        rigb.velocity = Vector3.zero;
        while(knocked_back)
        {
            Debug.Log(rigb.velocity);
            rigb.velocity = (DesiredLocation - rigb.transform.position) * 10f;
            yield return null;
        }

    }*/
    public void PlayGroundCrack()
    {
        groundcrack.transform.position = gameObject.transform.position;
        groundcrack.Play();
    }
    public void StopGroundCrack()
    {
        groundcrack.Stop();
    }
}
