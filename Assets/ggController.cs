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

    BoxCollider bx;
    NavMeshAgent nv;
    Animator am;
    void Start()
    {
        nv = GetComponent<NavMeshAgent>();
        bx = GetComponent<BoxCollider>();
        bx.enabled = false;
        am = GetComponent<Animator>();
        am.SetBool("Run", true);
        target = GameObject.Find("Escort Object");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") || other.gameObject.tag.Contains("Escort_Object"))
        {
            Debug.Log(other.gameObject.tag);
            GameObject ip = Instantiate(impactParticle, gameObject.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            ip.transform.parent = gameObject.transform;
            Destroy(ip, 3);
            bx.enabled = true;
            Destroy(gameObject, 1f);
        }

    }
    // Update is called once per frame
    void Update()
    {
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


}
