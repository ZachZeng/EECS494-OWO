using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack10 : StateMachineBehaviour
{
    
    float time;
    AttackLogic attack;
    bool hitEnemy = false;
    SoundEffects audio;
    bool audioPlayed = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        time = Time.time;
        GameController.instance.attacking = true;
        hitEnemy = false;
        audio = animator.GetComponent<SoundEffects>();
        audio.Attack0nohit();


    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        audioPlayed = false;
    }
}


