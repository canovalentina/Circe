using System.Collections;
using UnityEngine;

public class AnimalAttackState : AnimalBaseState
{
    public AnimalAttackState(AnimalStateMachine animal) : base(animal) { }
    
    private bool isAttacking = false;
    private Coroutine attackRoutine;

    public override void EnterState()
    {
        if (!animal.canAttack)
        {
            animal.TransitionToState(new AnimalIdleState(animal));
            return;
        }
    
        animal.animator.SetBool("Attack", true);
        animal.animator.SetFloat("Speed", 0f);
        
        if (!isAttacking) 
        {
            isAttacking = true;
            attackRoutine = animal.StartCoroutine(AttackLoopThenIdle());
        }  
    }

    public override void UpdateState()
    {
        if (!animal.IsPlayerNearby())
        {
            animal.TransitionToState(new AnimalIdleState(animal));
        }
    }

    public override void ExitState()
    {
        if (attackRoutine != null) 
        {
            animal.StopCoroutine(attackRoutine);
        }
        
        animal.animator.SetBool("Attack", false);
        isAttacking = false;
    }
    
    private IEnumerator AttackLoopThenIdle()
    {
        isAttacking = true;
        
        while (animal.IsPlayerNearby())
        {
            yield return new WaitForSeconds(animal.waitBetweenAttacksDuration);
        }

        animal.TransitionToState(new AnimalIdleState(animal));
        isAttacking = false;
    }
}
