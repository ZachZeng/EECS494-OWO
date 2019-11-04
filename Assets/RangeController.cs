using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangeController : MonoBehaviour
{

    public int playerNum;
    public float movementSpeed;
    Rigidbody rb;
    public bool canMove;
    public GameObject magePlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
        magePlayer = GameObject.Find("Mage");
        playerNum = magePlayer.GetComponent<MagePlayerMovement>().playerNum; 
    }

    // Update is called once per frame
    void Update()
    {
        //if (!myAttackStatus.knocked_back)
        //{
        Vector3 current_input = GetInput();

        if (canMove)
        {
            rb.velocity = current_input;
        }
        else
        {
            rb.velocity = Vector3.zero;
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
}
