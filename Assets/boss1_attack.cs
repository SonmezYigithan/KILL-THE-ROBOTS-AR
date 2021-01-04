using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_attack : StateMachineBehaviour
{
    private static float HP;
    public static int DamageAmount = 10;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HP = PlayerScript.HP;
        HP -= DamageAmount;

        Debug.LogError("HP form bs1_attack: " + HP);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
