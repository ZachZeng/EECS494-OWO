using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be.
    public float m_MaxSize = 20f;
    /*[HideInInspector]*/
    public Transform[] m_Targets; // All the targets the camera needs to encompass.

    public Transform[] wayPonts;

    public GameObject topPanel;
    public GameObject bottomPanel;

    Animator animator1;
    Animator animator2;

    public float overviewSpeed;
    public float backSpeed;

    public GameObject playerUI;



    private Camera m_Camera;                        // Used for referencing the camera.
    private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
    private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 m_DesiredPosition;              // The position the camera is moving towards.

    private bool eventHappen;

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
        
        animator1 = topPanel.GetComponent<Animator>();
        animator2 = bottomPanel.GetComponent<Animator>();

        eventHappen = false;



    }

    private void Start()
    {
        GameController.instance.isGameOver = false;
        if (GameController.instance.isGameBegin == false)
        {
            StartCoroutine(overviewMap());
        }
        else
        {
            playerUI.GetComponent<Animator>().SetBool("beginUI", true);
        }

    }


    private void FixedUpdate()
    {
        
        if (GameController.instance.isGameBegin && !eventHappen)
        {
            // Move the camera towards a desired position.
            Move();

            // Change the size of the camera based.
            Zoom();

        }

    }


    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();

        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();

        // Go through all the targets and add their positions together.
        for (int i = 0; i < m_Targets.Length; i++)
        {
            // If the target isn't active, go on to the next one.
            if (!m_Targets[i].gameObject.activeSelf || m_Targets[i].tag != "Escort_Object")
                continue;

            // Add to the average and increment the number of targets in the average.
            averagePos = m_Targets[i].position;
        }

        // Keep the same y value.
        averagePos.y = transform.position.y;

        // The desired position is the average position;
        m_DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < m_Targets.Length; i++)
        {
            // ... and if they aren't active continue on to the next target.
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        // Add the edge buffer to the size.
        size += m_ScreenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, m_MinSize);

        size = Mathf.Min(size, m_MaxSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        // Find the desired position.
        FindAveragePosition();

        // Set the camera's position to the desired position without damping.
        transform.position = m_DesiredPosition;

        // Find and set the required size of the camera.
        m_Camera.orthographicSize = FindRequiredSize();
    }


    IEnumerator overviewMap()
    {
        m_Camera.orthographicSize = 12;

        if(animator1 != null)
        {
            animator1.SetBool("beginCutScene", true);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", true);
        }
        yield return new WaitForSeconds(1f);

        for (int i = 1; i < wayPonts.Length; i++)
        {
            while (transform.position != wayPonts[i].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPonts[i].position, Time.deltaTime * overviewSpeed);
                yield return transform.position;
            }
            yield return new WaitForSeconds(0.5f);
        }


        while (transform.position != wayPonts[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPonts[0].position, Time.deltaTime * backSpeed);
            yield return transform.position;
        }

        yield return new WaitForSeconds(1);

        if (animator1 != null)
        {
            animator1.SetBool("beginCutScene", false);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", false);
        }

        GameController.instance.isGameBegin = true;
        Move();
        Zoom();
        playerUI.GetComponent<Animator>().SetBool("beginUI", true);
    }



    public void GameOverCamListener()
    {
        Debug.Log("EventCam");
        StartCoroutine(GameOverCam());
    }

    public void GameWinCamListener()
    {
        Debug.Log("EventCam");
        StartCoroutine(GameWinCam());
    }

    public void EventCamListener(GameObject item)
    {
        Debug.Log("EventCam");
        StartCoroutine(EventCam(item));
    }



    IEnumerator EventCam(GameObject item)
    {
        eventHappen = true;
        if (animator1 != null)
        {
            animator1.SetBool("beginCutScene", true);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", true);
        }
        while (transform.position != item.transform.position && m_Camera.orthographicSize != 5)
        {
            transform.position = Vector3.SmoothDamp(transform.position, item.transform.position, ref m_MoveVelocity, m_DampTime);
            m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, 5, ref m_ZoomSpeed, m_DampTime);
            yield return null;
        }
            
        yield return new WaitForSeconds(0.5f);
        if (animator1 != null)
        {
            animator1.SetBool("beginCutScene", false);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", false);
        }
        eventHappen = false;
    }



    IEnumerator GameOverCam()
    {
        if (animator1 != null)
        {
            animator1.SetBool("beginCutScene", true);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", true);
        }
        eventHappen = true;
        while( transform.position != m_Targets[0].position && m_Camera.orthographicSize != 5)
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_Targets[0].position, ref m_MoveVelocity, m_DampTime);
            m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, 5, ref m_ZoomSpeed, m_DampTime);
            yield return null;
        }
        yield return new WaitWhile(() => true);
        eventHappen = false;
        if (animator1 != null)
        {
            animator1.SetBool("beginCutScene", false);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", false);
        }

    }

    IEnumerator GameWinCam()
    {
        if (animator1 != null)
        {
            animator1.SetBool("beginCutScene", true);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", true);
        }
        eventHappen = true;
        yield return null;
        eventHappen = false;
        if (animator1 != null)
        {
            animator1.SetBool("beginCutScene", false);
        }
        if (animator2 != null)
        {
            animator2.SetBool("beginCutScene", false);
        }
    }

}
