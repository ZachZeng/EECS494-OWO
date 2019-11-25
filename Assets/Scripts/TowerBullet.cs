using UnityEngine;
using System.Collections;

public class TowerBullet : MonoBehaviour
{

    public float Speed;
    private Transform target;
    public Transform Curr_target;
    public GameObject impactParticle; // bullet impact

    public Vector3 impactNormal;
    Vector3 lastBulletPosition;
    public Tower twr;
    float i = 0.05f; // delay time of bullet destruction
    public int dmg;

    private void Start()
    {
        target = Curr_target.transform;
    }

    void Update()
    {

        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * Speed);
            lastBulletPosition = target.transform.position;
        }
    }

    // Bullet hit
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().ModifyHealth(-dmg);
        }

        if (collision.gameObject.tag == "Escort_Object")
        {
            collision.gameObject.GetComponent<Escort_State>().decreaseCurrentEscortHealth(dmg);
        }

        Destroy(gameObject, i);
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;  // Tower`s hit
        impactParticle.transform.parent = target.transform;
        Destroy(impactParticle, 3);
    }

}



