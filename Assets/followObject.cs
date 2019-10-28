using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObject : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    private void Awake()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
