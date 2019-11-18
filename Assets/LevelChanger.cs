using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private int levelToLoad = 1;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FadeToLevel(int levelIndex)
    {
        GameController.instance.playerChosen[0] = 0;
        GameController.instance.playerChosen[1] = 1;
        animator.SetTrigger("FadeOut");
        levelToLoad = levelIndex;
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void StartGame()
    {
        GameController.instance.playerChosen[0] = 0;
        GameController.instance.playerChosen[1] = 1;

        FadeToLevel(1);
    }
}
