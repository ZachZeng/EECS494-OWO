using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ggController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject impactParticle;
    public Vector3 impactNormal;
    public int ATK;
    bool isDead;
    BoxCollider bx;
    NavMeshAgent nv;
    Animator am;

    SkinnedMeshRenderer srd;
    public GameObject mRender;
    public Material flash;
    Material original;

    void Start()
    {
        nv = GetComponent<NavMeshAgent>();
        bx = GetComponent<BoxCollider>();
        bx.enabled = false;
        am = GetComponent<Animator>();
        am.SetBool("Run", true);
        target = GameObject.Find("Escort Object");
        srd = mRender.GetComponent<SkinnedMeshRenderer>();
        original = srd.material;
        isDead = false;
    }
    
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag.Contains("Player") || other.gameObject.tag.Contains("Escort_Object"))
    //    {
    //        Debug.Log(other.gameObject.tag);
    //        GameObject ip = Instantiate(impactParticle, gameObject.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
    //        ip.transform.parent = gameObject.transform;
    //        Destroy(ip, 3);
    //        bx.enabled = true;
    //        other.gameObject.GetComponent<Health>().ModifyHealth(-ATK);
    //        Destroy(gameObject, 1f);
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, target.transform.position));
        if (Vector3.Distance(transform.position, target.transform.position) <= 5 && !isDead)
        {
            GameObject ip = Instantiate(impactParticle, gameObject.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            ip.transform.parent = gameObject.transform;
            Destroy(ip, 3);
            bx.enabled = true;
            target.gameObject.GetComponent<Escort_State>().decreaseCurrentEscortHealth(ATK);
            isDead = true;
            Destroy(gameObject, 1f);    
        }

        StartCoroutine(Flash());
        nv.SetDestination(target.transform.position);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nv.isStopped = true;
            Destroy(gameObject, 0.5f);
            GameObject ip = Instantiate(impactParticle, gameObject.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            ip.transform.parent = gameObject.transform;
            Destroy(ip, 3);
            bx.enabled = true;

        }
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
