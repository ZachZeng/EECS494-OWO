using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public GameObject coach;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<QuerySDMecanimController>().ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_WALK);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, coach.transform.position, Time.deltaTime * speed);
        if(Vector3.Distance(transform.position,coach.transform.position) <= 1)
        {
            Destroy(gameObject);
        }
    }
}
