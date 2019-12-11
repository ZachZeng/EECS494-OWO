using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Camera mainCamera;
    float startTime;
    bool first = true;
    public GameObject princess;
    public QuerySDMecanimController anim;
    public GameObject fader;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        startTime = Time.time;
        anim = princess.GetComponent<QuerySDMecanimController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time -startTime> 1f && first)
        {
            StartCoroutine(Cam());
            first = false;
        }
    }
    IEnumerator Cam()
    {
        yield return StartCoroutine(rotateCam());
        StartCoroutine(princessWalk());
    }

    IEnumerator rotateCam()
    {

        Vector3 startPos_cam = mainCamera.transform.position;
        Vector3 endPos_cam = new Vector3(startPos_cam.x, startPos_cam.y, -21.15f);
        Vector3 startRot = mainCamera.transform.localEulerAngles;
        Vector3 endRot = new Vector3(startRot.x,startRot.y + 26.9f, 5.7f);
        float changeTime = 0f;
        bool setY = true;
        while (changeTime <= 2.0f)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos_cam, endPos_cam, changeTime / 2.0f);
            /*if (changeTime >= 1f && setY)
            {
                setY = false;
                mainCamera.transform.localEulerAngles = new Vector3(mainCamera.transform.localEulerAngles.x, -10.9f, mainCamera.transform.localEulerAngles.z);
                endRot.y = -10.9f;
            }*/
            mainCamera.transform.localEulerAngles = Vector3.Lerp(startRot, endRot, changeTime / 2.0f);
            // mainCamera.orthographicSize = mainCamera.orthographicSize + (targetSize - mainCamera.orthographicSize) * 0.02f;
            changeTime += Time.deltaTime;
            yield return null;
        }
        /*float diff =  mainCamera.transform.localEulerAngles.y - 10.9f;
        for (int i = 0; i < 10; ++i)
        {
            mainCamera.transform.localEulerAngles += new Vector3(0, diff/100, 0);
            yield return null;
            
        }*/
    }

    IEnumerator princessWalk()
    {
        anim.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_WALK);
        Vector3 startPos = princess.transform.position;
        Vector3 endPos = new Vector3(startPos.x + 6f, startPos.y, startPos.z - 3.3f);
        float changeTime = 0f;
        while (changeTime <= 2.0f)
        {
            princess.transform.position = Vector3.Lerp(startPos, endPos, changeTime / 2.0f);
            changeTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        fader.GetComponent<LevelChanger>().StartGame();
    }
}
