using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wobble());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator wobble()
    {
        while(true)
        {
            transform.eulerAngles = new Vector3(Random.Range(-10, 10), Random.Range(80, 100), Random.Range(-10, 10));
            yield return new WaitForSeconds(0.025f);
        }
    }
}
