using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController controller;
    Animator anim;
    public float Speed = 10;
    public float movementSpeed = 2;
    public bool canMove = true;
    public int playerNum = 0;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = GetInput();

        controller.Move(move * Time.deltaTime * Speed);
        anim.SetBool("movement", move != Vector3.zero);
        anim.SetFloat("movespeed", movementSpeed);

        if (!canMove)
        {
            anim.SetFloat("movespeed", 0);
        }

        if (move.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }
        //rb.velocity = current_input;
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
}
