using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class healthTextUpdate : MonoBehaviour
{

    public GameObject subject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(subject.name == "Escort Object")
        {
            gameObject.GetComponent<TextMeshProUGUI>().SetText(subject.gameObject.GetComponent<Escort_State>().getCurrentEscortHealth().ToString());
        }
        else 
            gameObject.GetComponent<TextMeshProUGUI>().SetText(subject.gameObject.GetComponent<Health>().currentHealth.ToString());

    }
}
