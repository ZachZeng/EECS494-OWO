using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastFirstEnemy : MonoBehaviour
{
    public bool isMage = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if (!isMage)
            ToastManager.instance.count += 1;
        else
            ToastManager.instance.MageCount += 1;
    }
}
