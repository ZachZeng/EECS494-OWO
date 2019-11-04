using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public Transform target;
    public float speed = 10;
    Rigidbody rb;
    public int ATK;


    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
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
        rb = GetComponent<Rigidbody>();
        transform.LookAt(target);
        rb.velocity = speed * transform.forward;
    }


    
}
