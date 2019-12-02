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
    public int knockBackForce;
    public float knockBackTime;
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
        na.stoppingDistance = 2f;
    }



    private void OnCollisionStay(Collision collision)
    {
        isAttacking |= (collision.gameObject.tag.Contains("Player") || collision.gameObject.tag == "Escort_Object");
        if(collision.gameObject.tag.Contains("Player") && !isTrapped)
        {
            collision.gameObject.GetComponent<PlayerHealthControl>().getAttack(ATK);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        isTrapped |= other.gameObject.name == "CastRange(Clone)";
        if (other.gameObject.name == "CastRange(Clone)")
            gameObject.GetComponent<AimSystem>().mage_aim[0] += 30;
    }


    public void StartKnockBack(GameObject collider)
    {
        getAttacked = true;
        StartCoroutine(KnockBack(collider));
    }

    private void OnCollisionExit(Collision collision)
    {
        isAttacking &= (!collision.gameObject.tag.Contains("Player") && collision.gameObject.tag != "Escort_Object");

        //Debug.Log("exit collision" + isAttacking);
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
            am.ResetTrigger("Attack 02");
            am.SetTrigger("Take Damage");
            GameObject ip = Instantiate(impactParticle, gameObject.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            ip.transform.parent = gameObject.transform;
            na.isStopped = true;
            Destroy(ip, 3);
            StartCoroutine(Flash());
        }
    }

    IEnumerator KnockBack(GameObject attacker)
    {
        na.isStopped = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Vector3 dir = transform.position - attacker.transform.position;
        dir.y = 0;
        Debug.Log(dir);
        gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * knockBackForce);
        yield return new WaitForSeconds(knockBackTime);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        na.isStopped = false;
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
        if (ToastManager.instance != null && ToastManager.instance.MageCount == 7)
        {
            // ToastManager.instance.frozen = true;
            ToastManager.instance.MageCount += 1;
            ToastManager.instance.magetoasts.Enqueue("Good job! Now let's wait for Knight to finish\nand move on to the healing skill.");
        }
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
            if (gameObject.tag == "Enemy")
            {
                am.SetBool("Walk Forward", true);
                na.SetDestination(target.transform.position);
            }
        }

        if (gameObject.tag == "Enemy")
        {
            JudgeAttack();
        }
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
        getAttacked = false;
        na.isStopped = false;
    }


}
