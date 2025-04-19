using System.Collections;
using UnityEngine;

public class AnimalWalkState : AnimalBaseState
{
    public AnimalWalkState(AnimalStateMachine animal) : base(animal) { }
    private float walkDuration;
    private Vector3 targetPosition;
    private Coroutine walkRoutine;
    private Vector3 currentVelocity = Vector3.zero; 
    
    public override void EnterState()
    {
        walkDuration = Random.Range(animal.minWalkDuration, animal.maxWalkDuration);
        targetPosition = PickDestination();
        animal.animator.SetBool("Walk", true);
        walkRoutine = animal.StartCoroutine(WalkThenIdle());
    }

    public override void UpdateState()
    {        
        MoveTowardTarget();

        if (Vector3.Distance(animal.transform.position, targetPosition) < 0.1f)
        {
            animal.TransitionToState(new AnimalIdleState(animal));
        }
    }
    
    public override void ExitState()
    {
        if (walkRoutine != null) 
        {
            animal.StopCoroutine(walkRoutine);
        }
        animal.animator.SetBool("Walk", false);
    }
    
    private IEnumerator WalkThenIdle()
    {
        yield return new WaitForSeconds(walkDuration);
        animal.TransitionToState(new AnimalIdleState(animal));
    }
    
    
    public Vector3 PickDestination()
    {
        //Find random destination
        Vector2 randomCircle = Random.insideUnitCircle * animal.wanderRadius;
        return animal.originPosition + new Vector3(randomCircle.x, 0, randomCircle.y);
    }
    private void MoveTowardTarget()
    {
        Vector3 toTarget = targetPosition - animal.transform.position;
        toTarget.y = 0f;

        if (toTarget.magnitude < 0.1f)
        {
            currentVelocity = Vector3.zero;
            animal.animator.SetFloat("Speed", 0f);
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        animal.transform.rotation = Quaternion.Slerp(
            animal.transform.rotation,
            targetRotation,
            Time.deltaTime * 1.5f
        );
        
        Vector3 moveDir = animal.transform.forward;
        currentVelocity = Vector3.Lerp(currentVelocity, moveDir * animal.moveSpeed, Time.deltaTime * 3f);
        animal.transform.position += currentVelocity * Time.deltaTime;

        animal.animator.SetFloat("Speed", currentVelocity.magnitude);
    }
}
