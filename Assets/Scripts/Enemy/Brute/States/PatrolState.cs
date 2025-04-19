using UnityEngine;

public class PatrolState : BruteState
{
    private int currentWaypoint = 0;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private float waitTime = 4f; 
    private float rotationSpeed = 30f; 
    private float maxRotationAngle = 45f;
    private float originalRotationY;
    private bool rotating = false;

    public PatrolState(BruteStateMachine brute) : base(brute) { }

    public override void Enter()
    {
        if (agent.isStopped) agent.isStopped = false;

        agent.stoppingDistance = 0.1f;
        agent.autoRepath = true;
        agent.updateRotation = true;
        agent.acceleration = 10f;
        agent.angularSpeed = 500f;
        agent.speed = brute.patrolSpeed;
        agent.autoBraking = false;

        WalkToCurrentWaypoint();
    }

    public override void Update()
    {
        float distance = Vector3.Distance(brute.transform.position, brute.circe.position);
        if (distance < brute.chaseRange || (distance < brute.seeRange && CanSeeCirce()))
        {
            brute.ChangeState(new ChaseState(brute));
            return;
        }

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (rotating)
            {
                LookAround();
            }

            if (waitTimer <= 0)
            {
                StopWaiting();
            }
            return; 
        }

        if (brute.waypoints.Length > 0)
        {
            if (Vector3.Distance(brute.transform.position, brute.waypoints[currentWaypoint].position) < 1f)
            {
                StartWaiting();
            }
        }
    }

    private void StartWaiting()
    {
        animator.SetBool(IsWalking, false);
        isWaiting = true;
        waitTimer = waitTime;
        agent.isStopped = true;
        rotating = true;
    }

    private void StopWaiting()
    {
        isWaiting = false;
        rotating = false;
        agent.isStopped = false;

        currentWaypoint = (currentWaypoint + 1) % brute.waypoints.Length;
        WalkToCurrentWaypoint();
    }

    private void LookAround()
    {
        float angleOffset = Mathf.PingPong(Time.time * rotationSpeed, maxRotationAngle * 2) - maxRotationAngle;
        float newRotationY = originalRotationY + angleOffset;

        brute.transform.rotation = Quaternion.Euler(0, newRotationY, 0);
    }

    private void WalkToCurrentWaypoint()
    {
        if (currentWaypoint >= 0 && currentWaypoint < brute.waypoints.Length)
        {
            agent.SetDestination(brute.waypoints[currentWaypoint].position);
            animator.SetBool(IsWalking, true);
        }
    }

    public override void Exit()
    {
        agent.isStopped = true;
    }
}
