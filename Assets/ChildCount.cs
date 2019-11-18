using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCount : MonoBehaviour
{
    public bool isMage = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentsInChildren<Transform>().Length == 1)
        {
            if (!isMage)
            {
                ToastManager.instance.count += 1;
                ToastManager.instance.toasts.Enqueue("Hooray! You have learned the movement and\ncombo attack system. Now let's move on to the skills.");
            }
            else
            {
                ToastManager.instance.MageCount += 1;
                ToastManager.instance.magetoasts.Enqueue("Hooray! You have learned the movement and\naiming system. Now let's move on to the skills.");
            }
            gameObject.SetActive(false);
        }
    }
}
