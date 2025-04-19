using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckState : AttackState
{
    public StuckState(BruteStateMachine brute) : base(brute) { }

    public override void Enter()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        animator.applyRootMotion = true;
        brute.bruteAnimator.SetBool("isAttack", false);
        brute.bruteAnimator.SetBool("isWalking", false);
        brute.bruteAnimator.SetBool("isConfused", true);
    }

    public override void Update()
    {

        //if (Time.time - lastAttackTime > attackCooldown)
        //{
        //    brute.bruteAnimator.SetBool("isConfused", true);
        //    brute.StartStateCoroutine(AttackCirce());
        //}
    }

    public override void Exit()
    {
        // once stuck - can't be unstuck
    }
}
