using UnityEngine;

public abstract class BruteState
{
    protected BruteStateMachine brute;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected Animator animator;
    protected Transform circe;

    protected static readonly int IsWalking = Animator.StringToHash("isWalking");
    protected static readonly int IsAttack = Animator.StringToHash("isAttack");


    public BruteState(BruteStateMachine brute)
    {
        this.brute = brute;
        this.agent = brute.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.animator = brute.GetComponent<Animator>();
        this.circe = brute.circe;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

    protected bool IsFacingTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - brute.transform.position).normalized;
        float angle = Vector3.Angle(brute.transform.forward, directionToTarget);
        return angle < 10f;
    }

    protected void RotateTowardsCirce()
    {
        Vector3 directionToCirce = (brute.circe.position - brute.transform.position).normalized;
        directionToCirce.y = 0;
        directionToCirce.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToCirce);
        brute.transform.rotation = Quaternion.Slerp(brute.transform.rotation, targetRotation, Time.deltaTime * brute.rotationSpeed);
    }

    protected bool CanSeeCirce()
    {
        Vector3 directionToTarget = (brute.circe.position - brute.transform.position).normalized;

        if (Vector3.Angle(brute.transform.forward, directionToTarget) < brute.viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(brute.transform.position, brute.circe.position);

            if (distanceToTarget <= brute.viewDistance)
            {
                if (!Physics.Raycast(brute.transform.position, directionToTarget, distanceToTarget, brute.obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
