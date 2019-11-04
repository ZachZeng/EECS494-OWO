
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class attack0 : StateMachineBehaviour
{
    float time;
    AttackLogic attack;
    bool hitEnemy = false;
    SoundEffects audio;
    bool audioPlayed = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        time = Time.time;
        attack = animator.GetComponent<AttackLogic>();
        attack.encountered.Clear();
        GameController.instance.attacking = true;
        hitEnemy = false;
        audio = animator.GetComponent<SoundEffects>();
        

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Time.time - time > 0.5f)
        {
            if (hitEnemy)
            {
                animator.GetComponent<PlayerMovement>().comboIndex++;
                animator.SetBool("attack1finished", true);
            }
            else
            {
                animator.GetComponent<PlayerMovement>().comboIndex = 0;
                animator.SetBool("combo", true);
                animator.SetBool("attack1finished", false);
                animator.ResetTrigger("attack2");
                animator.ResetTrigger("attack3");
            }
        }
        else if (Time.time - time > 0.2f)
        {
            Debug.Log("oops");
            bool local_hitEnemy = attack.LaunchAttack();
            if (hitEnemy == false)
            {
                hitEnemy = local_hitEnemy;
            }
            if (!audioPlayed)
            {
                audioPlayed = true;
                if (hitEnemy)
                {
                    audio.Attack0hit();
                }
                else
                {
                    audio.Attack0nohit();
                }
                
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        audioPlayed = false;
    }
}

