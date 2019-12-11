using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayLoadWobble : MonoBehaviour
{
    // Start is called before the first frame update
    public void payloadWobble()
    {
        StartCoroutine(attackedWobble());
    }
    IEnumerator attackedWobble()
    {
        
        for(float i = 0; i < 1; ++i)
            {
                i += 2*Time.deltaTime;
                transform.eulerAngles = new Vector3(Random.Range(-10, 10), 90, Random.Range(-10, 10));
                yield return new WaitForSeconds(0.05f);
            }
        transform.eulerAngles = new Vector3(0, 90, 0);
    }
}
