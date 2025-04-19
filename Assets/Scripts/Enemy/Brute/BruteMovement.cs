using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BruteMovement : MonoBehaviour
{
    public Transform circe;
    public float followDistance = 10f;
    public float attackDistance = 1f;
    public float attackCooldown = 2f;
    public float rotationSpeed = 5f;

    public float viewAngle = 60f; 
    public float viewDistance = 20f;
    public LayerMask obstacleMask;

    private Animator animator;
    private NavMeshAgent agent;
    private bool isAttacking = false;
    private float lastAttackTime;
    private bool startChasing = false;
    private PotionsActionsController pac;


    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (circe == null)
        {
            Debug.LogError("Circe not assigned in BruteMovement script!");
        } else
        {
            pac = circe.GetComponent<PotionsActionsController>();
        }

        if (pac == null)
        {
            Debug.LogError("PotionActionsController is not found");
        }
    }

    private void Update()
    {
        Debug.Log("we're still in brute movement");
        if (circe == null) return;

        float distance = Vector3.Distance(transform.position, circe.position);

        if (isAttacking) return;

        // Circe is in attacking distance
        if (distance <= attackDistance && Time.time > lastAttackTime + attackCooldown)
        {
            StartCoroutine(AttackCirce());
        }

        // Circe is in following distance
        else if (distance <= followDistance)
        {
            if (CanSeeCirce())
            {
                startChasing = true;
            }
            pac.checkinBrute(transform);
        }

        // Chase Circe
        if (startChasing)
        {
            // Rotate towards Circe if needed
            if (!IsFacingTarget(circe.position))
            {
                RotateTowardsCirce();
            }
            FollowCirce();
        }

        // Circe far away from Brute
        if (distance > attackDistance)
        {
            StopAttack();
        }
    }

    private bool IsFacingTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        return angle < 10f;
    }

    private void FollowCirce()
    {
        if (agent.isStopped) agent.isStopped = false;
        agent.SetDestination(circe.position);
        agent.stoppingDistance = 1.5f;
        agent.acceleration = 8f;
        agent.speed = 4.5f;
        animator.SetBool(IsWalking, true);
        //Debug.Log("Brute starts following Circe.");
    }

    private bool CanSeeCirce()
    {
        Vector3 directionToTarget = (circe.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, circe.position);

            if (distanceToTarget <= viewDistance)
            {
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    return true; 
                }
            }
        }
        return false; 
    }

    private void StopMoving()
    {
        agent.ResetPath();
        animator.SetBool(IsWalking, false);
    }

    private IEnumerator AttackCirce()
    {
        isAttacking = true;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        /*Transform sword = transform.Find("MaleBruteA_Meshes/BattleAxe_GEO");
        if (sword != null)
        {
            Collider swordCollider = sword.GetComponent<Collider>();
            if (swordCollider != null) swordCollider.enabled = true;
        }*/

        if (!IsFacingTarget(circe.position))
        {
            RotateTowardsCirce();
        }

        //Debug.Log("Brute swings his sword at Circe!");
        animator.SetBool(IsAttack, true);

        yield return new WaitForSeconds(2.15f);

        lastAttackTime = Time.time;
        isAttacking = false;

        /*if (sword != null)
        {
            Collider swordCollider = sword.GetComponent<Collider>();
            if (swordCollider != null) swordCollider.enabled = false;
        }*/

        float distance = Vector3.Distance(transform.position, circe.position);
        if (distance > attackDistance)
        {
            //Debug.Log("Circe moved away, following again...");
            agent.isStopped = false;
        }
    }

    /*private IEnumerator RotateTowardsCirce()
    {
        Vector3 directionToCirce = (circe.position - transform.position).normalized;
        directionToCirce.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToCirce);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }*/

    private void RotateTowardsCirce()
    {
        Vector3 directionToCirce = (circe.position - transform.position).normalized;
        directionToCirce.y = 0;
        directionToCirce.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToCirce);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void StopAttack()
    {
        //Debug.Log("Brute stops attacking, Circe moved away!");
        isAttacking = false;
        //animator.ResetTrigger(IsAttack);
        animator.SetBool(IsAttack, false);
        agent.isStopped = false;

        //float distance = Vector3.Distance(transform.position, circe.position);

        // Chase Circe
        if (startChasing)
        {
            // Rotate towards Circe if needed
            if (!IsFacingTarget(circe.position))
            {
                RotateTowardsCirce();
            }
            FollowCirce();
        }

    }
}