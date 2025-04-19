using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Circe stepped on the cloud.");
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Circe stepped off the cloud.");
            other.transform.SetParent(null);
        }
    }
}
