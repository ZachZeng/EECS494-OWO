using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    // Start is called before the first frame update

    public void wobbleEffect()
    {
        StartCoroutine(wobble());
    }
    IEnumerator wobble()
    {
        for(int i = 0; i < 10; ++i)
        {
            transform.eulerAngles = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
