using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToastManager : MonoBehaviour
{
    public static ToastManager instance;
    // for knight
    public Queue<string> toasts = new Queue<string>();
    public int count = 0;
    // for mage
    public Queue<string> magetoasts = new Queue<string>();
    public int MageCount = 0;
    public bool frozen = false;
    public bool MageFinished = false;
    public bool KnightFinished = false;
    public GameObject Fader;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MageFinished && KnightFinished)
        {
            StartCoroutine(switchScenes());
        }
    }
    IEnumerator switchScenes()
    {
        yield return new WaitForSeconds(5f);
        Fader.GetComponent<LevelChanger>().FadeToLevel(3);
        
    }
}
