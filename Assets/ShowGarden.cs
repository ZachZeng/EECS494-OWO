using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGarden : MonoBehaviour
{
    Camera mainCamera;
    public GameObject playerUI;
    public GameObject payLoadHealthUI;
    public GameObject princess;
    public GameObject cart;
    public ParticleSystem ps;
    public GameObject cameraRig;
    public GameObject celeb;
    QuerySDMecanimController anim;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        anim = princess.GetComponent<QuerySDMecanimController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LerpToGarden()
    {
        ps.Stop();
        ps.Play();
        mainCamera.orthographicSize = 10f;
        StartCoroutine(disappearandlerp());
        
    }
    IEnumerator disappearandlerp()
    {
        cameraRig.GetComponent<CameraController>().enabled = false;
        yield return new WaitForSeconds(0.75f);
        cart.SetActive(false);
        yield return new WaitForSeconds(1f);
        ps.Stop();
        playerUI.SetActive(false);
        payLoadHealthUI.SetActive(false);
        StartCoroutine(smoothTransition(0f, 26f));

    }
    IEnumerator smoothTransition(float dirX, float dirY)
    {
        Vector3 startPos = mainCamera.transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y + 40.25f, startPos.z);

        float changeTime = 0.0f;
        while (changeTime <= 3.0f)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, changeTime / 3.0f);
            //mainCamera.orthographicSize = (targetSize - startSize)
            changeTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(movePrincess());
        StartCoroutine(princessWalk());
    }

    IEnumerator movePrincess()
    {
        Vector3 startPos = princess.transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y + 3f, startPos.z);
        float changeTime = 0.0f;
        float targetSize = 6.24f;
        Vector3 startPos_cam = mainCamera.transform.position;
        Vector3 endPos_cam = new Vector3(startPos_cam.x +7.22f, startPos_cam.y - 3f, startPos_cam.z);
        while (changeTime <= 2.0f)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos_cam, endPos_cam, changeTime / 2.0f);
            princess.transform.position = Vector3.Lerp(startPos, endPos, changeTime / 2.0f);
            mainCamera.orthographicSize = mainCamera.orthographicSize + (targetSize - mainCamera.orthographicSize) * 0.02f;
            changeTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator princessWalk()
    {
        anim.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_FLY_DOWN);
        Vector3 startPos = princess.transform.position;
        Vector3 startRot = princess.transform.eulerAngles;
        Vector3 endRot = new Vector3(-25f, startRot.y, startRot.z);
        Vector3 endPos = new Vector3(startPos.x - 3f, startPos.y - 1f, startPos.z);
        float changeTime = 0f;
        while (changeTime <= 2.0f)
        {
            princess.transform.position = Vector3.Lerp(startPos, endPos, changeTime / 2.0f);
            princess.transform.eulerAngles = Vector3.Lerp(startRot, endRot, changeTime / 2f);
            changeTime += Time.deltaTime;
            yield return null;
        }
        anim.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_WIN);
        celeb.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Escort_State.instance.setGoalState();
    }
}
