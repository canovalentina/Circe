using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject exitPortal;
    public GameObject odysseus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Key collected!");

            if (exitPortal != null)
            {
                exitPortal.SetActive(true);
            }

            if (odysseus != null)
            {
                odysseus.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}

