using UnityEngine;
using System.Collections;

public class BruteStateMachine : MonoBehaviour
{
    private BruteState currentState;

    public Transform[] waypoints;
    public Transform circe;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 3f;
    public float chaseRange = 10f;
    public float seeRange = 20f;
    public float attackDistance = 1.5f;
    public float rotationSpeed = 5f;
    public float viewAngle = 60f;
    public float viewDistance = 20f;
    public LayerMask obstacleMask;

    public Animator bruteAnimator { get; private set; }
    private BruteHealthBarHandler bruteHealthHandler;

    private void Start()
    {
        bruteAnimator = GetComponent<Animator>();
        bruteHealthHandler = GetComponent<BruteHealthBarHandler>(); 

        // VC: Added this to avoid the constant referencing when creating new brutes
        if (circe == null) 
        {
          circe = GameObject.FindWithTag("Player")?.transform;
        }
        
        PotionsActionsController pac = circe.GetComponent<PotionsActionsController>();
        ChangeState(new PatrolState(this));
        pac.checkinBrute(transform);
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(BruteState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    void OnDrawGizmos()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent == null || agent.path == null) return;

        Gizmos.color = Color.red;
        Vector3[] corners = agent.path.corners;

        for (int i = 0; i < corners.Length - 1; i++)
        {
            Gizmos.DrawLine(corners[i], corners[i + 1]);
        }
    }


    public void TakeDamage(float damage)
    {
        // If doesn't have a health bar handler, just kill it
        if (bruteHealthHandler == null) 
        {
            Debug.Log("Trigger dead state");
            ChangeState(new DeadState(this));
        }
        else 
        {
            bruteHealthHandler.DecreaseHealth(damage);

            if (bruteHealthHandler.health <= 0)
            {
                Debug.Log("Brute died from damage.");
                ChangeState(new DeadState(this));
            }
        }
    }
    
    public float GetHealth() 
    {
        if (bruteHealthHandler != null) 
        {
            return bruteHealthHandler.health;
        }
        return 0;
    } 

    public void Stuck()
    {
        ChangeState(new StuckState(this));
    }

    public void StartStateCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopStateCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
    
}
