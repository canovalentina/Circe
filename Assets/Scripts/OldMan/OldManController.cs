using System.Collections;
using UnityEngine;

public class OldManController : MonoBehaviour
{
    private Animator animator;
    private Transform circe;
    private float screamDistance = 15f;
    private bool hasScreamed = false;
    private float rotationSpeed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        circe = GameObject.FindWithTag("Player")?.transform;

        if (circe == null)
        {
            Debug.LogError("Circe (Player) not found! Make sure she has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (circe == null) return;

        float distanceToCirce = Vector3.Distance(transform.position, circe.position);

        StartCoroutine(RotateTowardsCirce());
        if (distanceToCirce <= screamDistance && !hasScreamed)
        {
            hasScreamed = true;
            animator.SetBool("Scream", true);
        }
        else if (distanceToCirce > screamDistance)
        {
            hasScreamed = false;
            animator.SetBool("Scream", false);
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
}
