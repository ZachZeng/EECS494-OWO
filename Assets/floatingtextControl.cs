using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingtextControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float destoryTime = 3f;
    public Vector3 offset = new Vector3(0, 2, 0);
    public Vector3 random = new Vector3(0.5f, 0, 0);
    void Start()
    {
        Destroy(gameObject, destoryTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3((Random.Range(-random.x, random.x)), 0, 0);
    }
}
