using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class startScreenKnight : MonoBehaviour
{
    public bool hasBeenSelected = false;
    // public int playerNum = 0;
    Animator anim;
    Transform startPosition;
    public int num = 0;
    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform;
        anim = GetComponent<Animator>();
        loopAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition.position;
        if (GameController.instance.playerChosen[0] == num)
        {
            myText.color = new Color32(219, 50, 54, 200);
            myText.text = "Red Player!";
        }
        else if (GameController.instance.playerChosen[1] == num)
        {
            myText.color = new Color32(72, 133, 237, 200); ;
            myText.text = "Blue Player!";
        }
    }
    void loopAnimation()
    {
        anim.Play("attack10");
    }
    public void choose(int playerNum)
    {
        if (num == 0 && !hasBeenSelected)
        {
            hasBeenSelected = true;
            anim.SetBool("Attack2", true);
            GameController.instance.playerChosen[playerNum] = 0;
        }
        if (num == 1 && !hasBeenSelected)
        {
            hasBeenSelected = true;
            anim.SetBool("Attack2", true);
            GameController.instance.playerChosen[playerNum] = 1;
        }
    }
}
