using System.Collections;
using UnityEngine;

public class AthenaAttack : MonoBehaviour
{
    public GameObject deadlyCloudPrefab;
    public Transform spellCastPoint;
    public float attackRange = 25f;
    public float cloudSpeed = 5f;
    public float cloudLifetime = 4f;

    private Animator animator;
    private Transform circe;
    private bool isAttacking = false;
    //private bool isRotating = false;
    public float rotationSpeed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        circe = GameObject.FindWithTag("Player")?.transform;

        if (circe == null)
        {
            Debug.LogError("Circe (Player) not found! Make sure she has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (circe == null) return;

        float distanceToCirce = Vector3.Distance(transform.position, circe.position);

        StartCoroutine(RotateTowardsCirce());
        if (distanceToCirce <= attackRange && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("Throw", true);
        }
        else if (distanceToCirce > attackRange && isAttacking)
        {
            isAttacking = false;
            animator.SetBool("Throw", false);
        }
    }

    private IEnumerator RotateTowardsCirce()
    {
        Vector3 directionToCirce = (circe.position - transform.position).normalized;
        directionToCirce.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToCirce);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 2f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

    }

    // Call this function from the animation event when Athena actually releases the spell
    public void CastDeadlyCloud()
    {
        GameObject cloud = Instantiate(deadlyCloudPrefab, spellCastPoint.position, Quaternion.identity);
        cloud.GetComponent<CloudAttack>().LaunchCloud();
    }
}
