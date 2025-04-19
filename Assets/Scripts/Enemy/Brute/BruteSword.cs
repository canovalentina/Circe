using UnityEngine;

public class BruteSword : MonoBehaviour
{
    public float damageAmount = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaytestingLogger.LogAction("Enemy hit player", "Brute");
            PlaySwordSound();

            CirceHitReaction hitReaction = other.GetComponent<CirceHitReaction>();
            if (hitReaction != null)
            {
                hitReaction.TriggerHitReaction();
            }

            CharacterHealthBarCollisionHandler healthHandler = other.GetComponent<CharacterHealthBarCollisionHandler>();
            if (healthHandler != null)
            {
                healthHandler.DecreaseHealth(damageAmount);
            }
        }
    }
    
    private void PlaySwordSound() 
    {
        //Debug.Log("Playing Sword Sound for Brute");
        EventManager.TriggerEvent<SwordEvent, GameObject>(this.gameObject);
    }
}
