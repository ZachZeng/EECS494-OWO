using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject impactParticle;
    public Vector3 impactNormal;
    public bool isTrapped;
    public int ATK;
    Animator am;
    NavMeshAgent na;
    Material original;
    float speed;
    public bool isAttacking;
    public bool getAttacked;
    SkinnedMeshRenderer srd;
    public GameObject mRender;
    public Material flash;
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
        speed = na.speed;
        srd = mRender.GetComponent<SkinnedMeshRenderer>();
        original = srd.material;
        frozenTimer = 0;
        ATK = 10;
    }

    private void OnCollisionStay(Collision collision)
    {
        isAttacking |= (collision.gameObject.tag.Contains("Player") || collision.gameObject.tag == "Escort_Object");
    }


    private void OnTriggerStay(Collider other)
    {
        isTrapped |= other.gameObject.name == "CastRange(Clone)";
    }

    private void OnCollisionEnter(Collision collision)
    {
        getAttacked |= collision.gameObject.tag == "Weapon";
    }
    private void OnCollisionExit(Collision collision)
    {
        isAttacking &= (collision.gameObject.tag.Contains("Player") && collision.gameObject.tag != "Escort_Object");
        getAttacked &= collision.gameObject.tag.Contains("Weapon");
    }


    void JudgeAttack()
    {
        if (isAttacking)
        {
            am.SetTrigger("Attack 01");
        }
    }

    void JudageGetAttacked()
    {
        if (getAttacked)
        {
            am.ResetTrigger("Attack 01");
            am.SetTrigger("Take Damage");
            GameObject ip = Instantiate(impactParticle, gameObject.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            ip.transform.parent = gameObject.transform;
            Destroy(ip, 3);
            StartCoroutine(Flash());
        }
    }

    void JudgeTrapped()
    {
        if (isTrapped)
        {
            StartCoroutine(Frozen());
        }
        else
        {
            na.speed = speed;
        }
    }

    IEnumerator Frozen()
    {
        na.speed *= 0.1f;
        srd.material = frozen;
        yield return new WaitForSeconds(frozenTime);
        isTrapped = false;
    }

    void Update()
    {
        if (target != null && !isAttacking && !getAttacked && !isTrapped)
        {
            srd.material = original;
            am.SetBool("Walk Forward", true);
            na.SetDestination(target.transform.position);
        }
        JudgeAttack();
        JudageGetAttacked();
        JudgeTrapped();

    }

    IEnumerator Flash()
    {
        srd.material = flash;
        yield return new WaitForSeconds(0.2f);
        srd.material = original;
        yield return new WaitForSeconds(0.2f);
        srd.material = original;
    }

}
