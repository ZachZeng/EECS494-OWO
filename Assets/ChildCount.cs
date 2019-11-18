using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentsInChildren<Transform>().Length == 1)
        {
            ToastManager.instance.count += 1;
            ToastManager.instance.toasts.Enqueue("Hooray! You have learned the movement and\ncombo attack system. Now let's move on to the skills.");
            gameObject.SetActive(false);
        }
    }
}
