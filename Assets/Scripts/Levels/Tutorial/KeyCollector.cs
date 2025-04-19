using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    public GameObject circe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialController tc = circe.gameObject.GetComponent<TutorialController>();
            tc.CompleteStage(4);
            Destroy(gameObject);
        }
    }
}
