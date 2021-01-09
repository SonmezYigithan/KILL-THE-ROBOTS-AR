using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_attack : StateMachineBehaviour
{
    private static float HP;
    public static int DamageAmount = 20;
    private GameObject hitVignette;
    private Animator hitVignetteAnimator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerScript.HP -= DamageAmount;
        hitVignette = GameObject.Find("PlayerTakingHitVignette");
        hitVignetteAnimator = hitVignette.GetComponent<Animator>();
        hitVignetteAnimator.Play("PlayerTakeHitVignette");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
