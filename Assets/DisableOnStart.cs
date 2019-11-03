using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem ps;
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
