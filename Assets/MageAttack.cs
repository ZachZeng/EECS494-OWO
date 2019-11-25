using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MageAttack : MonoBehaviour
{
    Animator anim;
    MagePlayerMovement magePlayerMovement;
    MageArrowControl mageArrowControl;
    public GameObject fireball;
    public GameObject healball;
    public GameObject castRange;

    private GameObject castRangeClone;

    public bool isAttack;
    public float attackCD = 0.3f;

    public bool isHailCasting;
    private float hailCastTimer;
    public float hailCastTime;
    public float hailCD;
    private float hailCDTimer;
    public bool isHailCD;

    public bool isHeal;
    public float healCD;
    private float healCDTimer;
    public bool isHealCD;

    public Image healImg;
    public Text healCDText;
    public Image hailImg;
    public Text hailCDText;

    public AudioClip launchFireballAudio;
    public AudioClip launchHailAudio;



    void Start()
    {
        anim = GetComponent<Animator>();
        magePlayerMovement = GetComponent<MagePlayerMovement>();
        mageArrowControl = GetComponent<MageArrowControl>();
        isAttack = false;
        hailCastTimer = 0;
        hailCDTimer = 0;
        healCDTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.all[magePlayerMovement.playerNum];

        Vector2 arrow_dir = mageArrowControl.direction;

        if (gamepad.rightTrigger.wasPressedThisFrame && !isAttack && magePlayerMovement.canMove)
        {
            StartCoroutine(attack());
            anim.SetTrigger("attack");
            AudioSource.PlayClipAtPoint(launchFireballAudio, Camera.main.transform.position);
            Instantiate(fireball, transform.position + new Vector3(arrow_dir.x, 0.5f, arrow_dir.y), Quaternion.LookRotation(new Vector3(arrow_dir.x, 0, arrow_dir.y)));
        }

        //Start Heal
        if (!isHeal && gamepad.leftTrigger.wasPressedThisFrame && !isHealCD && magePlayerMovement.canMove)
        {
            healCDTimer = 0f;
            isHeal = true;
            magePlayerMovement.canMove = false;
            StartCoroutine(castHeal());
            isHealCD = true;
        }

        if (isHeal)
        {
            healCDTimer += Time.deltaTime;
            healCDText.GetComponent<Text>().enabled = true;
            healImg.GetComponent<Image>().enabled = true;
            healCDText.GetComponent<Text>().text = Mathf.RoundToInt(healCD - healCDTimer).ToString();
        }

        if (!isHealCD)
        {
            healCDText.GetComponent<Text>().enabled = false;
            healImg.GetComponent<Image>().enabled = false;
        }

        if (healCDTimer >= healCD)
        {
            isHeal = false;
            healCDTimer = 0;
            isHealCD = false;
        }



        //Finish Cast
        if (isHailCasting && gamepad.aButton.wasPressedThisFrame && hailCastTimer >= hailCastTime)
        {
            FinishCast();
        }

        //Stop Cast
        if (isHailCasting && gamepad.bButton.wasPressedThisFrame)
        {
            StopCast();
        }

        //Start Cast
        if (!isHailCasting && gamepad.aButton.wasPressedThisFrame && !isHailCD && magePlayerMovement.canMove)
        {
            StartCast();
        }

        //cast time caculate
        if (isHailCasting)
        {
            hailCastTimer += Time.deltaTime;
            Vector3 castScale = new Vector3(hailCastTimer / hailCastTime, hailCastTimer / hailCastTime, hailCastTimer / hailCastTime);
            //Debug.Log(hailCastTimer / hailCastTime);
            castRangeClone.transform.GetChild(0).localScale = castScale;
        }

        //ready to cast
        if (hailCastTimer >= hailCastTime)
        {
            hailCastTimer = hailCastTime;
        }

        if (isHailCD)
        {
            hailCDTimer += Time.deltaTime;
            //Update hailCD
            hailCDText.GetComponent<Text>().enabled = true;
            hailImg.GetComponent<Image>().enabled = true;
            hailCDText.GetComponent<Text>().text = Mathf.RoundToInt(hailCD - hailCDTimer).ToString();
        }

        if (!isHailCD)
        {
            hailCDText.GetComponent<Text>().enabled = false;
            hailImg.GetComponent<Image>().enabled = false;
        }

        if (hailCDTimer >= hailCD)
        {
            isHailCD = false;
            hailCDTimer = 0;
        }




    }

    public void StopCast()
    {
        if (castRangeClone)
            Destroy(castRangeClone);
        magePlayerMovement.canMove = true;
        isHailCasting = false;
        isHailCD = false;
        anim.SetBool("isHailCasting", false);
    }

    public void StartCast()
    {
        magePlayerMovement.canMove = false;
        hailCastTimer = 0f;
        isHailCasting = true;
        castRangeClone = Instantiate(castRange, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        anim.SetBool("isHailCasting", true);
    }

    public void FinishCast()
    {
        isHailCD = true;
        anim.SetBool("isHailCasting", false);
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
        AudioSource.PlayClipAtPoint(launchHailAudio, Camera.main.transform.position);
        castRangeClone.GetComponent<CapsuleCollider>().enabled = true;
        anim.SetTrigger("finishCast");
        castRangeClone.transform.GetChild(3).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        castRangeClone.GetComponent<CapsuleCollider>().enabled = false;
        if (castRangeClone)
            Destroy(castRangeClone);
        magePlayerMovement.canMove = true;
        isHailCasting = false;
    }

    IEnumerator castHeal()
    {
        anim.SetTrigger("heal");
        yield return new WaitForSeconds(1f);
        Vector2 arrow_dir = mageArrowControl.direction;
        Instantiate(healball, transform.position + new Vector3(arrow_dir.x, 0.6f, arrow_dir.y), Quaternion.LookRotation(new Vector3(arrow_dir.x, 0, arrow_dir.y)));
        yield return new WaitForSeconds(1f);
        magePlayerMovement.canMove = true;
    }
}
