using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    public GameObject[] activate_spawnpoints;
    public GameObject[] deactive_spawnpoints;

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
        foreach (GameObject spawnpoint in activate_spawnpoints)
        {
            spawnpoint.transform.GetChild(0).gameObject.GetComponent<GenerateEnemy>().enabled = true;
        }
        foreach (GameObject spawnpoint in deactive_spawnpoints)
        {
            spawnpoint.transform.GetChild(0).gameObject.GetComponent<GenerateEnemy>().enabled = false;
        }

    }
}
