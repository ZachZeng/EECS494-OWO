using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int playerNum;
    public float movementSpeed;
    Rigidbody rb;
    Animator anim;
    bool attacking = false;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad active = Gamepad.all[playerNum];
        Vector3 current_input = GetInput();
        anim.SetBool("movement", current_input != Vector3.zero);
        if (current_input.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(current_input);
        }
        rb.velocity = current_input;
        //Debug.Log(rb.velocity);

        if (active.aButton.wasPressedThisFrame)
        {
            anim.SetBool("Attack", true);
            //StartCoroutine(attack());
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }
    IEnumerator attack()
    {
        anim.SetTrigger("attack");
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        //DealDamage();
        //yield return new WaitForSeconds(0.1f);
        attacking = false;
        //dash_attack_started = false;

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
        return new Vector3(horizontal_input * movementSpeed, 0, vertical_input * movementSpeed);
    }
}
