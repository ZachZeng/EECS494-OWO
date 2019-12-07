using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePrincessSprite : MonoBehaviour
{
    // Start is called before the first frame update
    float startTime;
    Image img;
    public Sprite intro;
    public Sprite normal;
    public Sprite fly;
    void Start()
    {
        startTime = Time.time;
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 8.8f)
        {
            img.sprite = normal;
        }
        else if (Time.time - startTime > 5.5f)
        {
            img.sprite = fly;
        }
        else if (Time.time - startTime > 2.8f)
        {
            img.sprite = intro;
        }
    }
}
