using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    bool started = false;
    public GameObject player;
    SpriteRenderer sr;
    Color orig;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        orig = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (started == false)
        {
            Vector3 playerPosition = player.transform.position;
            if (playerPosition.x >= 357 && playerPosition.x <= 387 & playerPosition.z <= -719)
            {
                started = true;
                StartCoroutine(initiateGame());
            }
        }
    }

    IEnumerator initiateGame()
    {
        for (int i = 0; i < 3; ++i)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            sr.color = orig;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
