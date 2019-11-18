using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        ToastManager.instance.toasts.Enqueue("Nice work!");
        ToastManager.instance.count += 1;
        ps.Stop();
        gameObject.SetActive(false);
    }
    public void playEffect()
    {
        ps.Play();
    }
}
