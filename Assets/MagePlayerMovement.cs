using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagePlayerMovement : MonoBehaviour
{
    public int playerNum;
    public float movementSpeed;
    public Vector3 direction;
    Rigidbody rb;
    Animator anim;
    public bool canMove;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        canMove = true;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerNum = gameController.playerChosen[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!myAttackStatus.knocked_back)
        //{
        Vector3 current_input = GetInput();
        anim.SetBool("movement", current_input != Vector3.zero);
        anim.SetFloat("movespeed", movementSpeed);

        if (current_input.magnitude != 0 && canMove)
        {
            direction = current_input;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if(canMove)
        {
            rb.velocity = current_input;
        }
        else
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("movement", false);
        }
            

        //}
        //Debug.Log(rb.velocity);


    }

    Vector3 GetInput()
    {
        Gamepad active = Gamepad.all[playerNum];
        float horizontal_input = active.leftStick.x.ReadValue();
        float vertical_input = active.leftStick.y.ReadValue();
        if (Mathf.Abs(horizontal_input) < 0.1)
            horizontal_input = 0;
        if (Mathf.Abs(vertical_input) < 0.1)
            vertical_input = 0;
        return new Vector3(horizontal_input * movementSpeed * 5, 0, vertical_input * movementSpeed * 5);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("obstacle!!!");
            rb.AddForce(new Vector3(0, -1000, 0), ForceMode.Impulse);

        }
    }
}
