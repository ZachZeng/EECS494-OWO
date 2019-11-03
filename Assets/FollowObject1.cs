using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform GameObject;
    Vector3 offset;
    void Start()
    {
        offset = GameObject.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = GameObject.position + offset;
    }
}
