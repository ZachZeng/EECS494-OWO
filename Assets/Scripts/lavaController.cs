using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rock;
    float time = 0.0f;
    public int range = 5;
    public float duration = 20;
    private GameObject Obj1;
    Vector3 objectPoolPosition;
    Transform trans;
    int xRange = 15;
    int zRange = 14;
    void Start()
    {
        trans = this.transform;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > duration) {
            time = 0;
            for (int i = 0; i < range; i++) {
                int xPosition = Random.Range((int)trans.position.x - xRange, (int)trans.position.x + xRange);
                int yPosition = 10;
                int zPosition = Random.Range((int)trans.position.z - zRange, (int)trans.position.z + zRange);
                Obj1 = (GameObject)Instantiate(rock, objectPoolPosition, Quaternion.identity);
                Obj1.transform.position = new Vector3(xPosition, yPosition, zPosition);
            }
        }
    }
}
