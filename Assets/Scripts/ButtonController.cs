using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public AudioClip clickAudio;

    public GameObject fader;

    public void PlayAgian()
    {
        AudioSource.PlayClipAtPoint(clickAudio, Camera.main.transform.position);
        GameController.instance.isGameOver = false;
        GameController.instance.isGameBegin = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ToMenu()
    {
        GameController.instance.isGameBegin = false;
        GameController.instance.isGameOver = false;
        GameController.instance.playerChosen = new int[2] { -1, -1 };
        fader.GetComponent<LevelChanger>().FadeToLevel(0);
    }
}
