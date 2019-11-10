using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Escort_Obj_Movement : MonoBehaviour
{
    // Need to import transform of targets in editor
    [SerializeField]
    Transform[] EscortNavigationTargetTrans;
    //NavMeshAgent, use it to navigate our obj
    //private NavMeshAgent nav;
    //For future animator
    private Animator animator;
    private Rigidbody rb;
    //The current target we need to navigate
    public Transform target;
    public float speed;
    Vector3 stopPosition;
    void Start()
    {
        //nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GetTarget();
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    void Update()
    {
        if (Escort_State.instance.getBlockState()) {
            transform.position = stopPosition;
        }
        else if (target == null)
        {
            if (GetTarget() == null)
            {
                return;
            }
            else {
                target = GetTarget();
            }
        }
        //nav.SetDestination(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target);
        transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);

    }

    //if the escort object reaches the previous target, then get a new target
    Transform GetTarget()
    {
        for (int i = 0; i < EscortNavigationTargetTrans.Length; i++)
        {
            if (EscortNavigationTargetTrans[i] != null)
            {
                return EscortNavigationTargetTrans[i];
            }
        }
        return null;
    }
    /*
     * if tag = final, then set the goal state of escort object true
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Escort_Navigation_Target") {
            Destroy(other.gameObject);
            target = null;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            //Debug.Log("triggered!");
        }
        if (other.tag == "Escort_Navigation_Target_Final") {
            Debug.Log("Get Final target!");
            Escort_State.instance.setGoalState();
            Destroy(other.gameObject);
        }
        if (other.name == "Obstacle_Road") {
            stopPosition = gameObject.transform.position;
            Escort_State.instance.setBlockState(true);
        }
    }
}
