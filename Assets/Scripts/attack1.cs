using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class attack1 : StateMachineBehaviour
{
    float time;
    AttackLogic attack;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        time = Time.time;
        Debug.Log(time);

        attack = animator.GetComponent<AttackLogic>();
        attack.encountered.Clear();
        GameController.instance.attacking = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Time.time - time > 0.5f)
        {
            animator.SetBool("attack2finished", true);
        }
        else
        {
            attack.LaunchAttack();
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Debug.Log("Exit: " + Time.time);

    }
}
