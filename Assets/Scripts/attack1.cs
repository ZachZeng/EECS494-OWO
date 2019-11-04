using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class attack1 : StateMachineBehaviour
{
    float time;
    AttackLogic attack;
    bool hitEnemy;
    SoundEffects audio;
    bool audioPlayed = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        time = Time.time;
        Debug.Log(time);

        attack = animator.GetComponent<AttackLogic>();
        attack.encountered.Clear();
        GameController.instance.attacking = true;
        audio = animator.GetComponent<SoundEffects>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Time.time - time > 0.5f)
        {
            if (hitEnemy)
            {
                animator.GetComponent<PlayerMovement>().comboIndex++;
                animator.SetBool("attack2finished", true);
            }
            else
            {
                animator.GetComponent<PlayerMovement>().comboIndex = 0;
                animator.SetBool("combo", true);
                animator.SetBool("attack2finished", false);
                animator.ResetTrigger("attack2");
                animator.ResetTrigger("attack3");
            }
        }
        else if (Time.time - time > 0.2f)
        {
            bool local_hitEnemy = attack.LaunchAttack();
            if (hitEnemy == false)
                hitEnemy = local_hitEnemy;
            if (!audioPlayed)
            {
                audio.Attack1nohit();
                audioPlayed = true;
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Debug.Log("Exit: " + Time.time);
        audioPlayed = false;
        animator.SetBool("Attack2", false);

    }
}
