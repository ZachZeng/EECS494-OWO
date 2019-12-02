using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 3f;
    public float radius = 10f;
    public float force = 8000f;
    //float countdown;
    //bool hasExploded;
    public GameObject explosionEffect;
    /*void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }*/
    public void explode()
    {
        GetComponent<Escort_Obj_Movement>().speed = 0f;
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        //Escort_State.instance.setBlockState(true);
        yield return StartCoroutine(wobble());
        Explode();

    }
    IEnumerator wobble()
    {
        float time = delay;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            transform.eulerAngles = new Vector3(Random.Range(-10, 10), Random.Range(80, 100), Random.Range(-10, 10));
            yield return new WaitForSeconds(0.025f);
        }
    }
    void Explode()
    {
        // show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(force, transform.position, radius);
                rb.AddForce(force * (transform.position - nearbyObject.gameObject.transform.position), ForceMode.Impulse);
            }
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.Destroy();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        Destroy(gameObject);
        // get nearby objects
        // add force
        // damage
        // remove grenade
    }
}
