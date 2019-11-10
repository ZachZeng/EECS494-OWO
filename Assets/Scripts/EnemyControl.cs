using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public NavMeshAgent agent;
    public bool isTrapped;

    Animator am;
    NavMeshAgent na;
    Material original;
    float speed;
    public bool isAttacking;
    public bool getAttacked;
    SkinnedMeshRenderer srd;
    public GameObject mRender;
    public Material pureRed;
    public Material frozen;
    public bool knocked_back;

    private float frozenTimer;
    public float frozenTime;
 
    // Update is called once per frame
    private void Start()
    {
        am = GetComponent<Animator>();
        isAttacking = false;
        na = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        srd = mRender.GetComponent<SkinnedMeshRenderer>();
        original = srd.material;
        frozenTimer = 0;
        na.stoppingDistance = 2.0f;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Escort_Object")
        {
            am.SetBool("Walk Forward", false);
            na.isStopped = true;
            isAttacking = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "CastRange(Clone)")
        {
            isTrapped = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Escort_Object")
        {

            isAttacking = false;
            am.ResetTrigger("Stab Attack");
            na.isStopped = false;
        }
    }


    void Update()
    {
        if (target != null && !isAttacking && !getAttacked)
        {
            am.SetBool("Walk Forward", true);
            agent.SetDestination(target.transform.position);

        }

        if (isTrapped)
        {
            agent.speed *= 0.1f;
            srd.material = frozen;
            frozenTimer += Time.deltaTime;
        }
        else
        {
            agent.speed = speed;
            srd.material = original;
        }

        if(frozenTimer >= frozenTime)
        {
            isTrapped = false;
            frozenTimer = 0;
        }



        if (isAttacking && am.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            am.SetTrigger("Stab Attack");
        }

        if (!isAttacking)
        {
            am.ResetTrigger("Stab Attack");
        }

        if (getAttacked)
        {
      
            am.SetBool("Walk Forward", false);
            //am.ResetTrigger("Stab Attack");
            am.SetTrigger("Take Damage");
            StartCoroutine(InvincibleFrame());
            StartCoroutine(Flash());
            getAttacked = false;
        }
    }

    IEnumerator InvincibleFrame()
    {
        knocked_back = true;
        yield return new WaitForSeconds(1f);
        knocked_back = false;
    }

    IEnumerator Flash()
    {
        while (knocked_back)
        {
            srd.material = pureRed;
            yield return new WaitForSeconds(0.2f);
            srd.material = original;
            yield return new WaitForSeconds(0.2f);
        }
        srd.material = original;
    }

}
