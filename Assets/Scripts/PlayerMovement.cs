using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int playerNum;
    public float movementSpeed;
    public GameController gameController;
    Rigidbody rb;
    Animator anim;
    bool attacking = false;
    float lastAttackTime = 0;
    float maxDelay = 1f;
    public int comboIndex;
    public string[] comboParams;
    public AttackLogic myAttackStatus;
    public bool canMove;
    // dash parameters
    public bool sprinting = false;
    public Image dashImg;
    public Text dashCD;
    public bool dashCDset = false;
    public bool dashOnCD = false;
    public GameObject dashBox;
    // taunt parameters
    public Image tauntImg;
    public Text tauntCD;
    //public bool tauntCDset = false;
    public bool tauntOnCD = false;
    public ParticleSystem ps;
    bool first = true;
    public bool taunt = false;
    public GameObject tauntBox;
    Collider col;
    public ParticleSystem tauntPS;

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
        col = tauntBox.GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        myAttackStatus = GetComponent<AttackLogic>();
        canMove = true;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerNum = gameController.playerChosen[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        Gamepad active = Gamepad.all[playerNum];
        //if (!myAttackStatus.knocked_back)
        //{
        
        Vector3 current_input = GetInput();


        if(GameController.instance.isGameBegin)
        {
            if (active.yButton.wasPressedThisFrame && !sprinting && !dashOnCD)
            {
                Debug.Log("y button pressed");
                rb.velocity = transform.forward.normalized * 20;
                Debug.Log(rb.velocity);
                sprinting = true;
                dashCDset = false;
                dashOnCD = true;
                StartCoroutine(StartSprint());
            }

            if (active.xButton.wasPressedThisFrame && !taunt && !tauntOnCD)
            {
                Debug.Log("x button pressed");
                anim.SetTrigger("taunt");
                taunt = true;
                //tauntCDset = false;
                tauntOnCD = true;
                StartCoroutine(StartTaunt());
            }

            if (sprinting)
            {
                rb.velocity = transform.forward.normalized * 20;
                if (active.aButton.wasPressedThisFrame)
                {
                    comboIndex = 0;
                    Debug.Log("pressed a");
                    anim.SetTrigger("attack1");
                    comboIndex++;
                    lastAttackTime = Time.time;
                }
            }
            if (!canMove && first)
            {
                anim.SetBool("movement", false);
                anim.Play("Idle");
                first = false;
            }
            if (!sprinting && canMove && !taunt)
            {
                first = true;
                anim.SetBool("movement", current_input != Vector3.zero);
                anim.SetFloat("movespeed", movementSpeed);


                if (current_input.magnitude != 0)
                {
                    transform.rotation = Quaternion.LookRotation(current_input);
                }

                if (canMove)
                {
                    rb.velocity = current_input;
                    //Debug.Log("move");
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }

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


            }
        }

        
        
        //Debug.Log(numButtonPressed);

    }
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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
            //Debug.Log("obstacle!!!");
            rb.AddForce(new Vector3(0, -1000, 0), ForceMode.Impulse);

        }
    }
    IEnumerator StartTaunt()
    {
        tauntPS.Stop();
        tauntPS.Play();
        yield return new WaitForSeconds(0.5f);
       
        Collider[] cols = Physics.OverlapSphere(col.bounds.center, 4f);
        foreach (Collider c in cols)
        {
            if (c.gameObject.CompareTag("Enemy") && c.gameObject.name != "Obstacle_Road")
            {
                c.gameObject.GetComponent<AimSystem>().Tank(5f);
            }
            
        }
        yield return new WaitForSeconds(1f);
        taunt = false;

        StartCoroutine(StartTauntCD());

    }
    public IEnumerator StartTauntCD()
    {
        tauntCD.text = "10";
        tauntImg.enabled = true;
        tauntCD.enabled = true;
        for (int i = 10; i > 0; --i)
        {
            tauntCD.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        tauntOnCD = false;
        tauntImg.enabled = false;
        tauntCD.enabled = false;
    }
    public void TauntCD()
    {
        StartCoroutine(StartTauntCD());
    }
    IEnumerator StartSprint()
    {
        anim.SetBool("sprint", true);
        Debug.Log(rb.velocity);
        ps.Stop();
        ps.Play();
        dashBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("sprint", false);
        sprinting = false;
        dashBox.SetActive(false);
        if (!dashCDset)
        {
            StartCoroutine(StartDashCD());
        }

    }

    public IEnumerator StartDashCD()
    {
        dashCD.text = "8";
        ps.Stop();
        dashImg.enabled = true;
        dashCD.enabled = true;
        for (int i = 5; i > 0; --i)
        {
            dashCD.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        dashOnCD = false;
        dashImg.enabled = false;
        dashCD.enabled = false;
    }
    public void DashCD()
    {
        StartCoroutine(StartDashCD());
    }
        
}
