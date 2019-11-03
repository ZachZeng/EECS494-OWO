using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTrigger : StateMachineBehaviour
{
    float time;
    AttackLogic attack;
    SoundEffects audio;
    bool audioPlayed = false;
    bool effectPlayed = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        time = Time.time;
        attack = animator.GetComponent<AttackLogic>();
        attack.encountered.Clear();
        Rigidbody rb = animator.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 500, 0), ForceMode.Impulse);
        GameController.instance.attacking = true;
        audio = animator.GetComponent<SoundEffects>();

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (audioPlayed == false && Time.time - time > 0.1f)
        {
            audio.Attack2();
            audioPlayed = true;

        }
        if (Time.time - time > 0.35f)
        {
            attack.LaunchAttack3();
            if (effectPlayed == false)
            {
                Debug.LogWarning("called crack");
                attack.StopGroundCrack();
                attack.PlayGroundCrack();
                effectPlayed = true;
            }
        }


    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");
        GameController.instance.attacking = false;
        audioPlayed = false;
        effectPlayed = false;

    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
