using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePotionCollisionHandler : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // Brute
        BruteStateMachine bsm = other.gameObject.GetComponent<BruteStateMachine>();
        
        if (other.gameObject.CompareTag("Brute") && bsm != null) // we hit some brute
        {
            PlaytestingLogger.LogAction("Explosive potion hit enemy", "Brute");
            bsm.TakeDamage(1f);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        // Athena
        else if (other.CompareTag("Athena"))
        {
            Debug.Log("Orange potion hit Athena");
            PlaytestingLogger.LogAction("Explosive potion hit enemy", "Athena");
            AthenaHealthBarHandler athenaHealthHandler = other.GetComponent<AthenaHealthBarHandler>();
            if (athenaHealthHandler != null)
            {
                athenaHealthHandler.DecreaseHealth(1f);
                if (athenaHealthHandler.health <= 0) 
                {
                  WinGameManager.Instance.TriggerGameWon();
                }
            }
        }
        
        // Pottery
        PotteryController pt = other.gameObject.GetComponent<PotteryController>();
        
        if (pt != null)
        {
            PlaytestingLogger.LogAction("Explosive potion hit pottery");
            pt.BreakPottery();
        }

        // Ground
        if (other.gameObject.CompareTag("ground"))
        {
            PlaytestingLogger.LogAction("Orange potion hit ground");
            PlayExplosionSound();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    void OnDisable()
    {
        GetComponent<Collider>().enabled = false;
    }

    void OnEnable()
    {
        GetComponent<Collider>().enabled = true;
    }
    
    private void PlayExplosionSound() 
    {
        EventManager.TriggerEvent<ExplosionEvent, Vector3>(transform.position);
    }
}
