using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationPotionCollisionHandler : MonoBehaviour
{
    public GameObject transformationEffectPrefab;
    public GameObject animalPrefabBrute;
    public GameObject animalPrefabAthena;
    
    
    private void OnTriggerEnter(Collider other)
    {
    
        // Brute
        BruteStateMachine bsm = other.gameObject.GetComponent<BruteStateMachine>();
        
        if (other.gameObject.CompareTag("Brute") && bsm != null) // we hit some brute
        {
            Debug.Log("Transformation potion collision with Brute");
            PlaytestingLogger.LogAction("Transform enemy", "Brute");
            
            // Can only transform if health is 1
            if (bsm.GetHealth() > 1) 
            {
                bsm.TakeDamage(1f);
                PlayExplosionSound();
                Instantiate(transformationEffectPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } 
            else 
            {
                TransformToAnimal(other.gameObject, animalPrefabBrute);
            }
            
        }
        
        // Athena
        else if (other.gameObject.CompareTag("Athena"))
        {
            Debug.Log("Purple potion hit Athena");
            PlaytestingLogger.LogAction("Explosive potion hit enemy", "Athena");
            AthenaHealthBarHandler athenaHealthHandler = other.GetComponent<AthenaHealthBarHandler>();
            if (athenaHealthHandler != null)
            {
                if (athenaHealthHandler.health > 1) 
                {
                  athenaHealthHandler.DecreaseHealth(1f);
                }
                else 
                {
                    athenaHealthHandler.DecreaseHealth(1f);
                    TransformToAnimal(other.gameObject, animalPrefabAthena);
                    WinGameManager.Instance.TriggerGameWon(3f);
                }
            }
        }

        // Ground
        if (other.gameObject.CompareTag("ground"))
        {
            Debug.Log("Transformation potion collision with Ground");
            PlaytestingLogger.LogAction("Transformation potion hit the ground");
            PlayExplosionSound();
            Instantiate(transformationEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void TransformToAnimal(GameObject enemy, GameObject animalPrefab)
    {
        if (transformationEffectPrefab == null) 
        {
          Debug.LogError("Transformation Effect Prefab is not assigned.");
        }
        
        if (animalPrefab == null) 
        {
          Debug.LogError("Animal Prefab is not assigned.");
        }
        
        Vector3 position = enemy.transform.position;
        Quaternion rotation = enemy.transform.rotation;
        
        // Trasnformation particle effect
        Instantiate(transformationEffectPrefab, position, Quaternion.identity);
        Destroy(enemy);
        
        // Create the animal at current position
        position.y += 0.3f;
        GameObject animal = Instantiate(animalPrefab, position, rotation);
        animal.SetActive(true);
    }
    
    private void PlayExplosionSound() 
    {
        EventManager.TriggerEvent<ExplosionEvent, Vector3>(transform.position);
    }
}
