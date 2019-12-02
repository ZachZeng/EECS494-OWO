using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jointLogic : MonoBehaviour
{
    // Start is called before the first frame update
    //Vector3 position;
    //Quaternion rotation;
    
    void Start()
    {
        //position = transform.localPosition;
        //rotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = position;
        //transform.rotation = rotation;
    }
    // not used at the moment

    private void OnCollisionEnter(Collision other)
    {
        GameObject enemy = other.gameObject;
        Debug.Log(enemy.tag);
        if(enemy.CompareTag("Enemy"))
        {
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogWarning("enemy rigidbody null!");
            }
            else
            {
                if (ToastManager.instance != null && ToastManager.instance.count == 9)
                {
                    ToastManager.instance.toasts.Enqueue("The enemies will be stunned for 3 seconds.\n Congratulations, you have finished the Knight Tutorial.");
                    ToastManager.instance.count += 1;
                }
                //stunEnemy(enemy);
                Vector3 dir = enemy.transform.position - other.contacts[0].point;
                dir = dir.normalized;
                dir = new Vector3(dir.x, 0, dir.z);
                DashImpact di = enemy.GetComponent<DashImpact>();
                di.SetImpact(dir);
                stunEnemy(enemy);
                Debug.Log("not null");

            }
        }
    }

    void stunEnemy(GameObject enemy)
    {
        ParticleSystem ps = enemy.GetComponentsInChildren<ParticleSystem>()[1];
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
