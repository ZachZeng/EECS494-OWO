using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePS : MonoBehaviour
{
    // Start is called before the first frame update
    float maxhealth;
    Health myhealth;
    public GameObject ps;
    bool started = false;

    void Start()
    {
        myhealth = GetComponent<Health>();
        maxhealth = myhealth.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxhealth != myhealth.currentHealth && !started)
        {
            started = true;
            startps();

        }
    }
    void startps()
    {
        StartCoroutine(translate());
    }
    IEnumerator translate()
    {
        ParticleSystem myps = ps.GetComponent<ParticleSystem>();
        myps.Stop();
        myps.Play();
        while(true)
        {
            ps.transform.localPosition = new Vector3(0, Random.Range(0.25f, 1.8f), 0f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
