using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfmovement : MonoBehaviour
{
    // Start is called before the first frame update
    float initialx;
    float initialy;
    float time = 0;
    Vector3 position;
    void Start()
    {
        initialx = transform.position.x;
        initialy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 20) {
            time = 0;
            if (time > 10)
            {
                position.x = Mathf.Lerp(initialx - 5, initialx, time / 10);
            }
            else {
                position.x = initialx - Mathf.Lerp(0, 5, time / 10);
            }
            gameObject.transform.position = position;
        }
    }
}
