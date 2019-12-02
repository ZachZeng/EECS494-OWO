using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealballController : MonoBehaviour
{
    public float speed;
    public int healAmount;

    private float timer;
    public float healTime;
    public float healDis;

    private GameObject mage;
    private GameObject knight;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        mage = GameObject.Find("Mage");
        knight = GameObject.Find("Knight");
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        
        if (timer > healTime)
        {
            float dis_mage = Vector3.Distance(mage.transform.position, transform.position);
            float dis_knight = Vector3.Distance(knight.transform.position, transform.position);
            if (dis_knight < healDis && !(knight.GetComponent<Health>().currentHealth == 100))
            {
                transform.position = Vector3.MoveTowards(transform.position, knight.transform.position, 1);
            }
            else if(dis_mage < healDis && !(mage.GetComponent<Health>().currentHealth == 100))
            {
                transform.position = Vector3.MoveTowards(transform.position, mage.transform.position, 1);
            }
            else
            {
                transform.Translate(Vector3.forward * speed);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * speed);
        }
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
