using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb_boss;

    public float speed = 0.2f;
    public float attackRange = 0.46f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb_boss = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float step = speed * Time.fixedDeltaTime;
        Vector3 relativePos = player.transform.position - rb_boss.position;
        Vector3 newPos = Vector3.MoveTowards(rb_boss.position, player.transform.position, step);
        rb_boss.MovePosition(newPos);

        //turn towards to the player
        rb_boss.rotation = Quaternion.Slerp(rb_boss.rotation, Quaternion.LookRotation(relativePos, Vector3.up), 0.2f);
        //rb_boss.MoveRotation(Quaternion.LookRotation(relativePos, Vector3.up));

        if( Vector3.Distance(player.position, rb_boss.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
