using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jointLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // not used at the moment
    private void OnCollisionEnter(Collision other)
    {
        GameObject enemy = other.gameObject;
        if(enemy.CompareTag("Enemy"))
        {
            if (enemy.GetComponent<Rigidbody>() == null)
            {
                Debug.LogWarning("enemy rigidbody null!");
            }
            else
            {
                stunEnemy(enemy);
                Debug.Log("not null");

            }
        }
    }

    void stunEnemy(GameObject enemy)
    {
        ParticleSystem ps = enemy.GetComponentInChildren<ParticleSystem>();
        //ps.enableEmission = true;
        ps.Stop();
        ps.Play();
        /*
        Debug.Log("played");
        yield return new WaitForSeconds(2f);
        //ps.enableEmission = false;
        Debug.Log("Stop0");
        ps.Stop();
        Debug.Log("Stop");*/

    }
}
