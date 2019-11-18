using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovement : MonoBehaviour
{
    Rigidbody rb;
    float time;
    EnemyControl ec;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ec = GetComponent<EnemyControl>();
        StartCoroutine(sideMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(ec.isTrapped)
        {
            rb.velocity = Vector3.zero;
        }
    }
    IEnumerator sideMovement()
    {
        while(true && !ec.isTrapped)
        {
            rb.velocity = new Vector3(Random.Range(0f, 1f), 0, UnityEngine.Random.Range(0f, 1f));
            rb.velocity = new Vector3(1, 0);
            yield return new WaitForSeconds(0.5f);
            rb.velocity = -rb.velocity;
            yield return new WaitForSeconds(0.5f);
        }

    }
}
