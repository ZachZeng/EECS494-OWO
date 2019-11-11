using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseHealthEffect : MonoBehaviour
{
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healthEffect()
    {
        ps.Stop();
        ps.Play();
    }
}
