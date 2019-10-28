using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableBoxCollider : MonoBehaviour
{
    BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.attacking)
        {
            bc.enabled = true;
        }
        else
        {
            bc.enabled = false;
        }
    }
}
