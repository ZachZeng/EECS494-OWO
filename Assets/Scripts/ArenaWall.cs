using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour
{
    // Start is called before the first frame update
    public static ArenaWall instance;
    //isUping:
    public GameObject leftWall;
    public Transform leftWallUpTarget;
    public Transform leftWallDownTarget;
    public GameObject rightWall;
    public Transform rightWallUpTarget;
    public Transform rightWallDownTarget;
    public int processingTime = 3;
    public bool isUping;
    public bool isDowning;
    private Vector3 leftWallposition;
    private Vector3 rightWallposition;
    float time;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        isUping = false;
        isDowning = false;
        time = 0;
        leftWallposition = leftWall.transform.position;
        rightWallposition = rightWall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUping) {
            time += Time.deltaTime;
            if (time <= processingTime) {
                leftWallposition.y = Mathf.Lerp(leftWall.transform.position.y, leftWallUpTarget.position.y, (float)time / processingTime);
                rightWallposition.y = Mathf.Lerp(rightWall.transform.position.y, rightWallUpTarget.position.y, (float)time / processingTime);
                leftWall.transform.position = leftWallposition;
                rightWall.transform.position = rightWallposition;
            }
            if (time >= processingTime) {
                isUping = false;
                time = 0;
            }
        }
        if (isDowning)
        {
            time += Time.deltaTime;
            if (time <= processingTime)
            {
                leftWallposition.y = Mathf.Lerp(leftWall.transform.position.y, leftWallDownTarget.position.y, (float)time / processingTime);
                rightWallposition.y = Mathf.Lerp(rightWall.transform.position.y, rightWallDownTarget.position.y, (float)time / processingTime);
                leftWall.transform.position = leftWallposition;
                rightWall.transform.position = rightWallposition;
            }
            if (time >= processingTime)
            {
                isDowning = false;
                time = 0;
            }
        }
        if (!isUping && !isDowning) {
            time = 0;
        }
    }
    public void letWallUp() {
        isUping = true;
    }
    public void letWallDown()
    {
        isDowning = true;
    }
}
