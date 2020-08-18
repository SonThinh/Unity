using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBehaviour : StateMachineBehaviour
{
    private Vector2 slideSize = new Vector2(0.61f, 1.21f);
    private Vector2 slideOffset = new Vector2(0, -0.27f);
    private Vector2 size;
    private Vector2 offset;
    private BoxCollider2D boxCollider2D;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Slide = true;
        if (boxCollider2D == null)
        {
            boxCollider2D = Player.Instance.GetComponent<BoxCollider2D>();
            size = boxCollider2D.size;
            offset = boxCollider2D.offset;
        }
        boxCollider2D.size = slideSize;
        boxCollider2D.offset = slideOffset;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Slide = false;
        animator.ResetTrigger("slide");
        boxCollider2D.size = size;
        boxCollider2D.offset = offset;
    }

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
