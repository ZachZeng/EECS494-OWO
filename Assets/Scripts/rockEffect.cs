using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockEffect : MonoBehaviour
{
    // Start is called before the first frame update
    
    public ParticleSystem effect;
    bool firstTime = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (firstTime) {
            firstTime = false;
            if (effect)
            {
                StartCoroutine(beAttacked());
                Camera.main.GetComponent<shakeController>().enabled = true;
                //Debug.Log(collision.gameObject.name);
            }
        }
        
    }
    IEnumerator beAttacked()
    {
        effect.Play();
        yield return new WaitForSeconds(1f);
        effect.Stop();
    }
}
