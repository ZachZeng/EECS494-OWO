using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void PlayAgian()
    {
        GameController.instance.isGameOver = false;
        GameController.instance.isGameBegin = false;
        GameController.instance.playerChosen = new int[2] { -1, -1 };
        SceneManager.LoadScene("StartScene");
    }
}
