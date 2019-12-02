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
    GameObject doorControl;
    ArenaWall aw;
    float originSpeed;

    void Start()
    {
        //nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GetTarget();
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        doorControl = GameObject.Find("DoorControl");
        aw = doorControl.GetComponent<ArenaWall>();
        originSpeed = speed;

    }

    void Update()
    {
        if(GameController.instance.isGameBegin)
        {
            if (Escort_State.instance.getBlockState())
            {
                transform.position = stopPosition;
            }
            else if (target == null)
            {
                if (GetTarget() == null)
                {
                    return;
                }
                else
                {
                    target = GetTarget();
                }
            }
            //nav.SetDestination(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(target);
            transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z);
        }

        if (speed < originSpeed && GameObject.FindWithTag("Enemy_Boss").GetComponent<BossControl>().isBossDead)
        {
            speed = originSpeed;
        }
    }

    //if the escort object reaches the previous target, then get a new target
    Transform GetTarget()
    {
        for (int i = 0; i < EscortNavigationTargetTrans.Length; i++)
        {
            if (EscortNavigationTargetTrans[i] != null)
            {
                if (i == 6)
                {
                    aw.letWallUp();
                    GameObject.FindWithTag("Enemy_Boss").GetComponent<BossControl>().meetBoss = true;
                    speed = 0.0f;
                }
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
        if (other.gameObject.tag == "Escort_Navigation_Target")
        {
            Destroy(other.gameObject);
            target = null;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            //Debug.Log("triggered!");
        }
        if (other.gameObject.tag == "Escort_Navigation_Target_Final")
        {
            Debug.Log("Get Final target!");
            Escort_State.instance.setGoalState();
            Destroy(other.gameObject);
        }
        //if (other.gameObject.name == "BlockCube") {
        //    ArenaWall.instance.letWallUp();
        //    stopPosition = gameObject.transform.position;
        //    Escort_State.instance.setBlockState(true);

        //}
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Obstacle_Road") {
            if (!Escort_State.instance.getBlockState()) {
                stopPosition = gameObject.transform.position;
                Escort_State.instance.setBlockState(true);
            }
            
        }
    }
}
