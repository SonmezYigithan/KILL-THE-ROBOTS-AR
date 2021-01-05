using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Runaway : StateMachineBehaviour
{
    Rigidbody rb_boss;
    Vector3 TargetPos;

    int randindex;

    public float speed = 0.3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb_boss = animator.GetComponent<Rigidbody>();

        //random uzağa kaç
        var list = new List<string> { "z2.27", "x2.91", "z-2.46", "x-2.37" };
        randindex = Random.Range(0, list.Count);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (rb_boss.position == TargetPos)
        {
            animator.SetTrigger("RunTowardsPlayer");
        }

        //eğer noktaya ulaştıysa boss animation değiştir
        switch (randindex)
        {
            case 0:
                TargetPos = new Vector3(0, 0, 2.27f);
                MoveTowards(TargetPos);
                break;
            case 1:
                TargetPos = new Vector3(2.91f, 0, 0);
                MoveTowards(TargetPos);
                break;
            case 2:
                TargetPos = new Vector3(0, 0, -2.46f);
                MoveTowards(TargetPos);
                break;
            case 3:
                TargetPos = new Vector3(-2.37f, 0, 0);
                MoveTowards(TargetPos);
                break;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("RunTowardsPlayer");
    }

    void MoveTowards(Vector3 target)
    {
        float step = speed * Time.fixedDeltaTime;
        Vector3 relativePos = target - rb_boss.position;
        Vector3 newPos = Vector3.MoveTowards(rb_boss.position, target, step);
        rb_boss.MovePosition(newPos);

        //turn towards to the player
        rb_boss.rotation = Quaternion.Slerp(rb_boss.rotation, Quaternion.LookRotation(relativePos, Vector3.up), 0.2f);
    }
}
