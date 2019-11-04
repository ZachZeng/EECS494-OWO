using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public EventSystem myEventSystem;
    public GameObject player1;
    public GameObject player2;
    public GameObject fader;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad active0 = Gamepad.all[0];
        Gamepad active1 = Gamepad.all[1];
        if (active0.startButton.wasPressedThisFrame || active1.startButton.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if(player1.gameObject.name == "Knight")
        {
            player1.GetComponent<PlayerMovement>().enabled = true;
            player2.GetComponent<MagePlayerMovement>().enabled = true;
        }
        else
        {
            player1.GetComponent<MagePlayerMovement>().enabled = true;
            player2.GetComponent<PlayerMovement>().enabled = true;
        }

        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        myEventSystem.SetSelectedGameObject(null);
        myEventSystem.SetSelectedGameObject(myEventSystem.firstSelectedGameObject);
        if (player1.gameObject.name == "Knight")
        {
            player1.GetComponent<PlayerMovement>().enabled = false;
            player2.GetComponent<MagePlayerMovement>().enabled = false;
        }
        else
        {
            player1.GetComponent<MagePlayerMovement>().enabled = false;
            player2.GetComponent<PlayerMovement>().enabled = false;
        }
        Time.timeScale = 0f;
        isPaused = true;
    }
    // not used 
    public void Menu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        GameController.instance.playerChosen = new int[2] { -1, -1 };
        fader.GetComponent<LevelChanger>().FadeToLevel(0);
    }
}
