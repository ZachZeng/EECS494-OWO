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
    float lastAttackTime = 0;
    float maxDelay = 1f;
    public int comboIndex;
    public string[] comboParams;
    public AttackLogic myAttackStatus;
    public bool canMove;
    // Start is called before the first frame update
    private void Awake()
    {
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
        {
            comboParams = new string[] { "attack1", "attack2", "attack3" };
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        myAttackStatus = GetComponent<AttackLogic>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gamepad active = Gamepad.all[playerNum];
        //if (!myAttackStatus.knocked_back)
        //{
        Vector3 current_input = GetInput();
        anim.SetBool("movement", current_input != Vector3.zero);
        anim.SetFloat("movespeed", movementSpeed);

        if (!canMove)
        {
            anim.SetFloat("movespeed", 0);
        }

            if (current_input.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(current_input);
        }
        rb.velocity = current_input;
        //}
        //Debug.Log(rb.velocity);

        if ((Time.time - lastAttackTime > maxDelay || comboIndex >= 3))
        {

            comboIndex = 0;
            anim.SetTrigger("reset");
            //Debug.Log("reset triggered");
        }

        if (comboIndex > 0)
        {
            anim.SetBool("combo", false);
        }
        else
        {
            anim.SetBool("combo", true);
        }
        if (active.aButton.wasPressedThisFrame && comboIndex < comboParams.Length)
        {
            Debug.Log(comboParams[comboIndex] + " triggered");
            anim.SetTrigger(comboParams[comboIndex]);
            if (comboIndex == 2)
                anim.SetBool("attack2finished", false);
            if (comboIndex == 1)
                anim.SetBool("attack1finished", false);
            comboIndex++;
            lastAttackTime = Time.time;
            //StartCoroutine(attack());
        }

        //Debug.Log(numButtonPressed);

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
        return new Vector3(horizontal_input * movementSpeed *5, 0, vertical_input * movementSpeed*5);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("obstacle!!!");
            rb.AddForce(new Vector3(0, -1000, 0), ForceMode.Impulse);

        }
    }
}
