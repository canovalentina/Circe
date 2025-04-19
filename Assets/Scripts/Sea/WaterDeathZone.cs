using UnityEngine;

public class WaterDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Circe fell into the water!");

            // Get the health controller and apply instant death
            CharacterHealthBarCollisionHandler healthController = other.GetComponent<CharacterHealthBarCollisionHandler>();
            if (healthController != null)
            {
                healthController.DecreaseHealth(100f);
            }

        }
    }
}
