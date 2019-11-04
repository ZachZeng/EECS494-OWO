using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouding : MonoBehaviour
{
    // Start is called before the first frame update
    float original_y;
    void Start()
    {
        original_y = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        Vector3 pos0 = transform.position;
        pos0.y = original_y;
        transform.position = pos0;
    }
}
