using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public ParticleSystem respawnEffect;
    public Transform payload;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        respawnEffect.Stop();
        respawnEffect.Play();
        transform.position = payload.position - 2 * payload.forward.normalized;
    }
}
