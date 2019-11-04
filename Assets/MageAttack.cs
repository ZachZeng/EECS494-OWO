using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MageAttack : MonoBehaviour
{
    Animator anim;
    MagePlayerMovement magePlayerMovement;
    public GameObject CastCDUI;
    public GameObject HealCDUI;
    public GameObject fireball;
    public GameObject healball;
    public GameObject castRange;

    private GameObject castRangeClone;

    public bool isAttack;
    public float attackCD = 0.3f;

    public bool isCasting;
    private float castTimer;
    public float castTime;
    public float castCD;
    private float cdTimer;
    public bool isCD;

    public bool isHeal;
    public float healCD;
    private float healCDTimer;
    public bool isHealCD;


    void Start()
    {
        anim = GetComponent<Animator>();
        magePlayerMovement = GetComponent<MagePlayerMovement>();
        isAttack = false;
        castTimer = 0;
        cdTimer = 0;
        healCDTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.all[magePlayerMovement.playerNum];



        if (gamepad.rightTrigger.wasPressedThisFrame && !isAttack && magePlayerMovement.canMove)
        {
            StartCoroutine(attack());
            anim.SetTrigger("attack");
            Instantiate(fireball, transform.position + new Vector3(0,0.6f,0), Quaternion.LookRotation(magePlayerMovement.direction));
        }

        //Start Heal
        if (!isHeal && gamepad.yButton.wasPressedThisFrame && !isHealCD && magePlayerMovement.canMove)
        {
            healCDTimer = 0f;
            isHeal = true;
            magePlayerMovement.canMove = false;
            StartCoroutine(castHeal());
            isHealCD = true;
        }

        if(isHeal)
        {
            healCDTimer += Time.deltaTime;
            HealCDUI.GetComponent<Text>().text = Mathf.RoundToInt(healCD - healCDTimer).ToString();
        }

        if(!isHealCD)
        {
            HealCDUI.GetComponent<Text>().text = "Y";
        }

        if(healCDTimer >= healCD)
        {
            isHeal = false;
            healCDTimer = 0;
            isHealCD = false;
        }



        //Finish Cast
        if (isCasting && gamepad.aButton.wasPressedThisFrame && castTimer >= castTime)
        {
            FinishCast();
        }

        //Stop Cast
        if(isCasting && gamepad.bButton.wasPressedThisFrame)
        {
            StopCast();
        }

        //Start Cast
        if(!isCasting && gamepad.aButton.wasPressedThisFrame && !isCD && magePlayerMovement.canMove)
        {
            StartCast();
        }

        //cast time caculate
        if(isCasting)
        {
            castTimer += Time.deltaTime;
            Vector3 castScale = new Vector3(castTimer / castTime, castTimer / castTime, castTimer / castTime);
            //Debug.Log(castTimer / castTime);
            castRangeClone.transform.GetChild(0).localScale = castScale;
        }

        //ready to cast
        if(castTimer >= castTime)
        {
            castTimer = castTime;
        }

        if(isCD)
        {
            cdTimer += Time.deltaTime;
            //Update CastCD
            CastCDUI.GetComponent<Text>().text = Mathf.RoundToInt(castCD - cdTimer).ToString();
        }

        if(!isCD)
        {
            CastCDUI.GetComponent<Text>().text = "A";
        }

        if (cdTimer >= castCD)
        {
            isCD = false;
            cdTimer = 0;
        }

        

        
    }

    public void StopCast()
    {
        if (castRangeClone)
            Destroy(castRangeClone);
        magePlayerMovement.canMove = true;
        isCasting = false;
        isCD = false;
        anim.SetBool("isCasting", false);
    }

    public void StartCast()
    {
        magePlayerMovement.canMove = false;
        castTimer = 0f;
        isCasting = true;
        castRangeClone = Instantiate(castRange, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        anim.SetBool("isCasting", true);
    }

    public void FinishCast()
    {
        isCD = true;
        anim.SetBool("isCasting", false);
        castRangeClone.GetComponent<RangeController>().canMove = false;
        StartCoroutine(castAnim());
    }

    IEnumerator attack()
    {
        anim.SetTrigger("attack");
        isAttack = true;
        yield return new WaitForSeconds(attackCD);
        //DealDamage();
        //yield return new WaitForSeconds(0.1f);
        isAttack = false;
        //dash_attack_started = false;

    }

    IEnumerator castAnim()
    {
        castRangeClone.GetComponent<CapsuleCollider>().enabled = true;
        anim.SetTrigger("finishCast");
        castRangeClone.transform.GetChild(3).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        castRangeClone.GetComponent<CapsuleCollider>().enabled = false;
        if (castRangeClone)
            Destroy(castRangeClone);
        magePlayerMovement.canMove = true;
        isCasting = false;
    }

    IEnumerator castHeal()
    {
        anim.SetTrigger("heal");
        yield return new WaitForSeconds(1f);
        Instantiate(healball, transform.position + new Vector3(0, 0.5f, 0) + magePlayerMovement.direction * 0.1f, Quaternion.LookRotation(magePlayerMovement.direction));
        yield return new WaitForSeconds(1f);
        magePlayerMovement.canMove = true;
    }
}
