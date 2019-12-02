using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToastManager : MonoBehaviour
{
    public bool startedHealTutorial = false;
    public static ToastManager instance;
    // for knight
    public Queue<string> toasts = new Queue<string>();
    public int count = 0;
    // for mage
    public Queue<string> magetoasts = new Queue<string>();
    public int MageCount = 0;
    public int togetherCount = 0;
    public bool frozen = false;
    public bool MageFinished = false;
    public bool KnightFinished = false;
    public GameObject Fader;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject togetherCamera;
    Camera together;
    public GameObject mage;
    public GameObject knight;
    public GameObject canvasUpper;
    public GameObject canvasLower;
    public GameObject splitPlayerUI;
    public GameObject togetherCanvas;
    public GameObject togetherUI;
    public GameObject yellowLine;
    public GameObject knightui;
    public GameObject mageui;
    public bool healed = false;

    public Queue<string> togethertoasts = new Queue<string>();
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
        together = togetherCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MageFinished && KnightFinished)
        {
            StartCoroutine(healTutorial());
        }
        if(instance.togetherCount == 9)
        {
            StartCoroutine(switchScenes());
        }
    }

    IEnumerator healTutorial()
    {
        yield return new WaitForSeconds(3f);
        canvasUpper.SetActive(false);
        canvasLower.SetActive(false);
        if(!startedHealTutorial)
            splitPlayerUI.SetActive(false);
        camera1.SetActive(false);
        camera2.SetActive(false);
        yellowLine.SetActive(false);
        togetherCamera.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            together.orthographicSize += 0.0005f * (4.57f - together.orthographicSize);
            yield return new WaitForSeconds(0.25f);
        }
        if (!startedHealTutorial)
        {
            startedHealTutorial = true;
            knightui.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            knightui.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            mageui.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            mageui.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            mage.transform.position = new Vector3(2.63f, -0.03f, -0.850f);
            instance.togethertoasts.Enqueue("Healing Skill time!!!");

        }
        togetherCanvas.SetActive(true);
        splitPlayerUI.SetActive(true);
        //GameController.instance.isGameBegin = false;
        //Fader.GetComponent<LevelChanger>().FadeToLevel(1);

    }
    IEnumerator switchScenes()
    {
        yield return new WaitForSeconds(2f);
        GameController.instance.isGameBegin = false;
        Fader.GetComponent<LevelChanger>().FadeToLevel(1);
        
    }

}
