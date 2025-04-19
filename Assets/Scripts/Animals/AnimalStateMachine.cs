using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalStateMachine : MonoBehaviour
{

    public enum AnimalType { Wolf, Bear, Pig }
    public AnimalType animalType;
    public float moveSpeed = 0.5f;
    public float wanderRadius = 25f;
    public float detectionRange = 10f;
    public float attackDetectionRange = 5f;
    public float minIdleDuration = 2f;
    public float maxIdleDuration = 5f;
    public float minWalkDuration = 3f;
    public float maxWalkDuration = 6f;
    public float waitBetweenAttacksDuration = 0.5f;
    public bool canAttack = false;
    
    public Animator animator { get; private set; }
    private AnimalBaseState currentState;
    private Transform player;
    public Vector3 originPosition { get; private set; }
    

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        originPosition = transform.position;

        if (!animator)
        {
            Debug.LogError("Animal does not have an Animator component.");
            return;
        }

        TransitionToState(new AnimalIdleState(this));
    }

    void Update()
    {
        currentState?.UpdateState();

        if (IsPlayerNearby() && canAttack && !(currentState is AnimalAttackState))
        {
            TransitionToState(new AnimalAttackState(this));
        }
    }

    public void TransitionToState(AnimalBaseState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public bool IsPlayerNearby()
    {
        if (player == null) return false;
        
        float range;
        if (currentState is AnimalAttackState) 
        {
            range = attackDetectionRange;
        }
        else 
        {
            range = detectionRange;
        }
        return Vector3.Distance(transform.position, player.position) < range;
    }
    
    public void PlayWolfHowlSound() 
    {
        if (!IsPlayerNearby() || animalType != AnimalType.Wolf) return;
        
        //Debug.Log("Playing wolf howl.");
        EventManager.TriggerEvent<WolfHowlEvent, GameObject>(this.gameObject);        
    }
    
    public void PlayBearGrowlSound() 
    {   
        if (!IsPlayerNearby() || animalType != AnimalType.Bear) return;
        
        //Debug.Log("Playing bear growl.");
        EventManager.TriggerEvent<BearGrowlEvent, GameObject>(this.gameObject);        
    }
    
    public void PlayPigOinkSound() 
    {   
        if (!IsPlayerNearby() || animalType != AnimalType.Pig) return;
        
        //Debug.Log("Playing pig oink.");
        EventManager.TriggerEvent<PigOinkEvent, GameObject>(this.gameObject);        
    }
    
    public void OnHowlComplete()
    {
        if (currentState is AnimalIdleState idleState)
        {
            idleState.OnHowlFinished();
        }
    }
}