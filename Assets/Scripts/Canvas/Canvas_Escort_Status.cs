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
    [SerializeField]
    GameObject panel;

    public GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth.GetComponent<Image>().fillAmount = 
            Mathf.Lerp( 0 , 1 , (float)Escort_State.instance.getCurrentEscortHealth()
            / Escort_State.instance.getMaxEscortHealth());
        if (!Escort_State.instance.getStatus() && !gameController.isGameOver) {
            gameOverNotify.SetActive(true);
            panel.SetActive(true);
            gameController.isGameOver = true;
        }
        if (Escort_State.instance.getGoalState() && !gameController.isGameOver)
        {
            winNotify.SetActive(true);
            panel.SetActive(true);
            gameController.isGameOver = true;
        }
    }
}
