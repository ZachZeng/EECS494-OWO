using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(gameObject.name == "Escort_Navigation_Target_No1")
        {
            GameObject.Find("SpawnPoint1").transform.GetChild(0).GetComponent<GenerateEnemy>().enabled = false;
            GameObject.Find("SpawnPoint1").transform.GetChild(1).GetComponent<GenerateEnemy>().enabled = false;
        }
        if(gameObject.name == "Escort_Navigation_Target_No2")
        {
            GameObject.Find("SpawnPoint2").transform.GetChild(0).GetComponent<GenerateEnemy>().enabled = true;
            GameObject.Find("SpawnPoint2").transform.GetChild(1).GetComponent<GenerateEnemy>().enabled = true;
        }
        if (gameObject.name == "Escort_Navigation_Target_No3")
        {
            GameObject.Find("SpawnPoint2").transform.GetChild(0).GetComponent<GenerateEnemy>().enabled = false;
            GameObject.Find("SpawnPoint2").transform.GetChild(1).GetComponent<GenerateEnemy>().enabled = false;
        }
        if (gameObject.name == "Escort_Navigation_Target_No4")
        {
            GameObject.Find("SpawnPoint3").transform.GetChild(0).GetComponent<GenerateEnemy>().enabled = true;
            GameObject.Find("SpawnPoint3").transform.GetChild(1).GetComponent<GenerateEnemy>().enabled = true;
        }
        if (gameObject.name == "Escort_Navigation_Target_No5")
        {
            GameObject.Find("SpawnPoint3").transform.GetChild(0).GetComponent<GenerateEnemy>().enabled = false;
            GameObject.Find("SpawnPoint3").transform.GetChild(1).GetComponent<GenerateEnemy>().enabled = false;
            GameObject.Find("SpawnPoint4").transform.GetChild(0).GetComponent<GenerateEnemy>().enabled = true;
            GameObject.Find("SpawnPoint4").transform.GetChild(1).GetComponent<GenerateEnemy>().enabled = true;
        }

    }
}
