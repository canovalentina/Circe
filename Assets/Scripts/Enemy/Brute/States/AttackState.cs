using UnityEngine;
using System.Collections;


public class AttackState : BruteState
{
    protected float attackCooldown = 2f;
    protected float lastAttackTime;

    public AttackState(BruteStateMachine brute) : base(brute) { }

    public override void Enter()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        animator.applyRootMotion = true;
        brute.StartStateCoroutine(AttackCirce());
        PlaytestingLogger.LogAction("Enemy engaged in attack");
    }

    public override void Update()
    {
        if (Vector3.Distance(brute.transform.position, brute.circe.position) > brute.attackDistance)
        {
            brute.ChangeState(new ChaseState(brute));
            return;
        }

        if (Time.time - lastAttackTime > attackCooldown)
        {
            brute.StartStateCoroutine(AttackCirce());
        }
    }

    protected IEnumerator AttackCirce()
    {
        if (!IsFacingTarget(brute.circe.position))
        {
            RotateTowardsCirce();
        }

        animator.SetBool(IsAttack, true);

        yield return new WaitForSeconds(2.15f);

        lastAttackTime = Time.time;
    }

    public override void Exit()
    {
        brute.StopStateCoroutine(AttackCirce());
        animator.applyRootMotion = false;
        animator.SetBool(IsAttack, false);
        agent.isStopped = false;
    }
}
