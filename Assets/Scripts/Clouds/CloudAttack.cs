using UnityEngine;

public class CloudAttack : MonoBehaviour
{
    private Transform circe;
    public float speed = 5f;
    public float destroyTime = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        circe = GameObject.FindWithTag("Player")?.transform;
        if (circe != null)
        {
            targetPosition = circe.position;
            isMoving = true;
        }

        Destroy(gameObject, destroyTime);
    }

    public void LaunchCloud()
    {
        if (circe == null) return;

        targetPosition = circe.position;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            // Stop moving when close enough
            if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                isMoving = false;
            }
        }
    }

    public float damageAmount = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaytestingLogger.LogAction("Cloud hit player", "Cloud");

            CirceHitReaction hitReaction = other.GetComponent<CirceHitReaction>();
            if (hitReaction != null)
            {
                hitReaction.TriggerHitReaction();
            }

            CharacterHealthBarCollisionHandler healthHandler = other.GetComponent<CharacterHealthBarCollisionHandler>();
            if (healthHandler != null)
            {
                healthHandler.DecreaseHealth(damageAmount);
                Destroy(gameObject);
            }
        }
    }
}
