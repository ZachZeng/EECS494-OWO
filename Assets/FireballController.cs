using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float speed;
    public int ATK;
    public GameObject hitParticle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Contains("Enemy") && gameObject)
        {
            collision.gameObject.GetComponent<Health>().ModifyHealth(-ATK);
            EnemyControl ec = collision.gameObject.GetComponent<EnemyControl>();
            if(ec)
            {
               ec.StartKnockBack(gameObject);
            }
            if (collision.gameObject.name == "Obstacle_Road")
            {
                collision.gameObject.GetComponent<Wobble>().wobbleEffect();
                collision.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
                collision.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            }

        }
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
