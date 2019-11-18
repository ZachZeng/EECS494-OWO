using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DashImpact : MonoBehaviour
{
    // Start is called before the first frame update
    public bool doImpact;
    Rigidbody rb;
    Vector3 direction;
    public float force;
    bool onImpact = false;
    NavMeshAgent agent;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (doImpact && !onImpact)
        {
            StartCoroutine(impact());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetImpact(Vector3 dir)
    {
        direction = dir;
        doImpact = true;
    }

    IEnumerator impact()
    {
        onImpact = true;
        rb.isKinematic = false;
        agent.enabled = false;
        rb.AddForce(direction * force, ForceMode.Impulse);
        doImpact = false;
        yield return new WaitForSeconds(3f);
        onImpact = false;
        agent.enabled = true;
        rb.isKinematic = true;
    }
}
