using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public float force;
    public float gravityScale = 1;
    public static float globalGravity = -9.81f;
    Rigidbody rb;
    void Start()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb = GetComponent<Rigidbody>();
        rb.velocity = force *  new Vector3(0, 1, 1) ;
        rb.AddForce(gravity, ForceMode.Acceleration);

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
}
