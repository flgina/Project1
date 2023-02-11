using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntroBehavior : StateMachineBehaviour
{

    private int rand;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0,2);
        if (rand == 0)
        {
            animator.SetTrigger("Idle");
            Debug.Log("j chillin");
        }
        else
        {
            animator.SetBool("Jump", true);
            Debug.Log("jompjompjomp");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
