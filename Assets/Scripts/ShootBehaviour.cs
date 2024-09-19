using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Shoot");
    }
}
