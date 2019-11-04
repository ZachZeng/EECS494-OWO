using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealballController : MonoBehaviour
{
    public float speed;
    public int healAmount;
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
        if(collision.gameObject.CompareTag("Player") && !(collision.gameObject.GetComponent<Health>().currentHealth == 100))
        {
            collision.gameObject.GetComponent<Health>().ModifyHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
