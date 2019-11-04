using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthControl : MonoBehaviour
{
    public Material pureRed;
    public Material originalMaterial;
    public bool knocked_back = false;
    public GameObject model;

    Rigidbody rb;
    Health myHealth;
    Renderer model_mr;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myHealth = GetComponent<Health>();
        model_mr = model.GetComponent<Renderer>();
        originalMaterial = model_mr.material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!knocked_back)
            {
                //Debug.Log("enemy collided with me");
                myHealth.ModifyHealth(-10);
                StartCoroutine(InvincibleFrame());
                StartCoroutine(Flash());
            }
        }
    }

    IEnumerator InvincibleFrame()
    {
        knocked_back = true;
        yield return new WaitForSeconds(2f);
        knocked_back = false;
    }
    IEnumerator Flash()
    {
        while (knocked_back)
        {
            //Debug.Log("hello");
            model_mr.material = pureRed;
            yield return new WaitForSeconds(0.2f);
            model_mr.material = originalMaterial;
            yield return new WaitForSeconds(0.2f);
        }
        model_mr.material = originalMaterial;
    }
}
