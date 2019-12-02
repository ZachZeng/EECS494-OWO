using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDashTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ToastManager.instance.MageFinished && ToastManager.instance.KnightFinished)
        {
            gameObject.SetActive(false);
        }
    }
}
