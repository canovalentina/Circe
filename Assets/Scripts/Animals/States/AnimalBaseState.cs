using UnityEngine;

public abstract class AnimalBaseState
{
    protected AnimalStateMachine animal;

    public AnimalBaseState(AnimalStateMachine animal)
    {
        this.animal = animal;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
