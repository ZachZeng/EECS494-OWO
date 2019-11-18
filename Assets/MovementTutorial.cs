using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public ParticleSystem ps;
    public bool isMage = false;
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
        if (!isMage)
        {
            ToastManager.instance.toasts.Enqueue("Nice work!");
            ToastManager.instance.count += 1;
        }
        else
        {
            ToastManager.instance.magetoasts.Enqueue("Nice work!");
            ToastManager.instance.MageCount += 1;
        }
        ps.Stop();
        gameObject.SetActive(false);
    }
    public void playEffect()
    {
        ps.Play();
    }
}
