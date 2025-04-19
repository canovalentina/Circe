using UnityEngine;
using System.Collections;

public class ChaseState : BruteState
{
    private float updateRate = 0.25f;

    public ChaseState(BruteStateMachine brute) : base(brute) { }

    public override void Enter()
    {
        agent.stoppingDistance = 1f;
        agent.autoRepath = true;
        agent.updateRotation = true;
        agent.acceleration = 10f;
        agent.angularSpeed = 500f;
        agent.speed = brute.chaseSpeed;
        agent.autoBraking = false;

        if (agent.isStopped) agent.isStopped = false;

        if (!IsFacingTarget(circe.position))
        {
            RotateTowardsCirce();
        }
        animator.SetBool(IsWalking, true);
        brute.StartStateCoroutine(UpdatePathRoutine());
    }

    private IEnumerator UpdatePathRoutine()
    {
        while (true)
        {
            UnityEngine.AI.NavMesh.SamplePosition(circe.position, out UnityEngine.AI.NavMeshHit hit, 1f, UnityEngine.AI.NavMesh.AllAreas);

            if (!agent.pathPending && Vector3.Distance(agent.destination, hit.position) > 1f)
            {

                agent.SetDestination(hit.position);
            }
            yield return new WaitForSeconds(updateRate);
        }
    }

    public override void Update()
    {
        if (Vector3.Distance(brute.transform.position, brute.circe.position) > brute.chaseRange || !CanSeeCirce())
        {
            brute.ChangeState(new PatrolState(brute));
            return;
        }

        if (Vector3.Distance(brute.transform.position, brute.circe.position) < brute.attackDistance)
        {
            brute.ChangeState(new AttackState(brute));
            return;
        }
    }


    public override void Exit()
    {
        animator.SetBool(IsWalking, false);
        brute.StopStateCoroutine(UpdatePathRoutine());
    }
}
