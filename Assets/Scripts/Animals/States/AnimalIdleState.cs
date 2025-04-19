using System.Collections;
using UnityEngine;

public class AnimalIdleState : AnimalBaseState
{
    public AnimalIdleState(AnimalStateMachine animal) : base(animal) { }
    private float idleDuration;
    private bool isHowling = false;
    private Coroutine idleRoutine;

    public override void EnterState()
    {
        idleDuration = Random.Range(animal.minIdleDuration, animal.maxIdleDuration);
        animal.animator.SetBool("Idle", true);
        animal.animator.SetFloat("Speed", 0f);
        
        // Wolf howls randomly
        if (animal.animalType == AnimalStateMachine.AnimalType.Wolf && Random.value < 0.7f)
        {
            isHowling = true;
            animal.animator.SetBool("Howl", true);
        }
        else
        {
            idleRoutine = animal.StartCoroutine(IdleThenWalk());
        }
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        if (idleRoutine != null) 
        {
            animal.StopCoroutine(idleRoutine);
        }
        
        animal.animator.SetBool("Idle", false);
        animal.animator.SetBool("Howl", false);
        isHowling = false;
    }

    private IEnumerator IdleThenWalk()
    {
        yield return new WaitForSeconds(idleDuration);
        animal.TransitionToState(new AnimalWalkState(animal));
    }
    
    public void OnHowlFinished()
    {
        isHowling = false;
        animal.animator.SetBool("Howl", false);
        idleRoutine = animal.StartCoroutine(IdleThenWalk());
    }
    
    public bool IsHowling() 
    {
      return isHowling;
    }
}
