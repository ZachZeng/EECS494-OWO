
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class attack0 : StateMachineBehaviour
{
    float time;
    AttackLogic attack;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        time = Time.time;
        attack = animator.GetComponent<AttackLogic>();
        attack.encountered.Clear();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Time.time - time > 0.5f)
        {
            animator.SetBool("attack1finished", true);
        }
        else
        {
            attack.LaunchAttack();
        }
    }
}

