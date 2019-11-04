using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class simpleMovement : MonoBehaviour
{
    public int playerNum;
    Rigidbody rb;
    public float movementSpeed;

    public GameObject char1;
    public GameObject char2;
    startScreenKnight char1logic;
    startScreenKnight char2logic;
    // Start is called before the first frame update
    void Start()
    {
        char1logic = char1.GetComponent<startScreenKnight>();
        char2logic = char2.GetComponent<startScreenKnight>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad active = Gamepad.all[playerNum];
        rb.velocity = GetInput();

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
    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            char1logic.choose(playerNum);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            char2logic.choose(playerNum);
            Destroy(gameObject);
        }

    }

}
