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
        if(GameController.instance.playerChosen[0] != -1 && GameController.instance.playerChosen[1] != -1)
        {
            FadeToLevel(1);
        }
    }
    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
        levelToLoad = levelIndex;
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
