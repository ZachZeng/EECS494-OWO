using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Camera mainCamera;
    float startTime;
    bool first = true;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time -startTime> 1f && first)
        {
            Cam();
            first = false;
        }
    }
    void Cam()
    {
        StartCoroutine(rotateCam());
    }
    IEnumerator rotateCam()
    {

        Vector3 startPos_cam = mainCamera.transform.position;
        Vector3 endPos_cam = new Vector3(startPos_cam.x, startPos_cam.y, -21.15f);
        Vector3 startRot = mainCamera.transform.localEulerAngles;
        Vector3 endRot = new Vector3(startRot.x,-10.9f, startRot.z);
        float changeTime = 0f;
        while (changeTime <= 2.0f)
        {
            //mainCamera.transform.position = Vector3.Lerp(startPos_cam, endPos_cam, changeTime / 2.0f);
            mainCamera.transform.localEulerAngles = Vector3.Lerp(startRot, endRot, changeTime / 2.0f);
            // mainCamera.orthographicSize = mainCamera.orthographicSize + (targetSize - mainCamera.orthographicSize) * 0.02f;
            changeTime += Time.deltaTime;
            yield return null;
        }
    }
}
