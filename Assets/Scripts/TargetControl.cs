using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Vector3 destination;
    void Start()
    {
        destination = new Vector3(transform.position.x, transform.position.y, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
