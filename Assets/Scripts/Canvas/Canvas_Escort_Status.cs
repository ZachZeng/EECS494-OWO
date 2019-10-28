using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Escort_Status : MonoBehaviour
{
    [SerializeField]
    GameObject currentHealth;
    [SerializeField]
    GameObject gameOverNotify;
    [SerializeField]
    GameObject winNotify;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth.GetComponent<Image>().fillAmount = 
            Mathf.Lerp( 0 , 1 , (float)Escort_State.instance.getCurrentEscortHealth()
            / Escort_State.instance.getMaxEscortHealth());
        if (!Escort_State.instance.getStatus()) {
            gameOverNotify.SetActive(true);
        }
        if (Escort_State.instance.getGoalState())
        {
            winNotify.SetActive(true);
        }
    }
}
