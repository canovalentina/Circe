using Unity.VisualScripting;
using UnityEngine;

public class DeadState : BruteState
{
    private static readonly int IsDead = Animator.StringToHash("isDead");

    public DeadState(BruteStateMachine brute) : base(brute) { }

    public override void Enter()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        PlayDeathMaleSound();

        animator.SetBool(IsDead, true);

        DisableBruteColliders();
        PlaytestingLogger.LogAction("Enemy dead", "Brute");
    }

    public override void Update() {
    
    }

    public override void Exit() {
        // no exit from being dead, I'm sorry
    }


    private void DisableBruteColliders()
    {
        Transform sword = FindChildByName(brute.transform, "BattleAxe_GEO");

        if (sword != null)
        {
            Collider swordCollider = sword.GetComponent<Collider>();
            if (swordCollider != null)
            {
                swordCollider.enabled = false;
                Debug.Log("Brute's sword collider disabled.");
            }
            else
            {
                Debug.LogWarning("No collider found on Brute's sword.");
            }
        }
        else
        {
            Debug.LogWarning("Sword object not found on Brute.");
        }
    }

    private Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            if (child.name == name) return child;
        }
        return null;
    }
    
    private void PlayDeathMaleSound() 
    {
        //Debug.Log("Playing Death Male Sound for Brute");
        EventManager.TriggerEvent<DeathMaleEvent, Vector3>(brute.transform.position);
    }
}
