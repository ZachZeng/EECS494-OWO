using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public Transform target;
    public float speed = 10;
    Rigidbody rb;
    public int ATK;
    Vector3 targetPos;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().ModifyHealth(-ATK);
        }

        else if(collision.gameObject.tag == "Escort_Object")
        {
            collision.gameObject.GetComponent<Escort_State>().decreaseCurrentEscortHealth(ATK);
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        targetPos = target.position;
        Vector3 tDir = transform.position - target.position;
        Quaternion rot = Quaternion.LookRotation(tDir);
        transform.rotation = rot;
    }

    private void Update()
    {

        float dist = Vector3.Distance(transform.position, targetPos);
        if (dist <= 0.1f)
        {
            //This will destroy the arrow when it is within .1 units
            //of the target location. You can set this to whatever
            //distance you're comfortable with.
            Destroy(gameObject);

        }
        else
        { 
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
    }

}
