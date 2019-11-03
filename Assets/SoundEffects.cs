using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip attack0nohit;
    public AudioClip attack0hit;
    public AudioClip attack1nohit;
    public AudioClip attack1hit;
    public AudioClip attack2;
    public AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack0nohit()
    {
        AS.clip = attack0nohit;
        AS.Play();
    }
    public void Attack0hit()
    {
        AS.clip = attack0hit;
        AS.Play();
    }

    public void Attack1nohit()
    {
        AS.clip = attack1nohit;
        AS.Play();
    }
    // not used 
    public void Attack1hit()
    {
        AS.clip = attack1hit;
        AS.Play();
    }

    public void Attack2()
    {
        AS.clip = attack2;
        AS.Play();
    }
}
