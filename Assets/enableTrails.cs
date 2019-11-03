using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableTrails : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem ps;
    void Start()
    {
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.attacking)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }
}
