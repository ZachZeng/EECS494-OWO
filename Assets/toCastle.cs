using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toCastle : MonoBehaviour
{
    public GameObject des;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (des.transform.position - transform.position) * 0.05f;
    }
}
