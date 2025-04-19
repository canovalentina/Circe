using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brute"))
        {
            BruteStateMachine fsm = other.gameObject.GetComponent<BruteStateMachine>();
            if (fsm != null)
            {
                PlaytestingLogger.LogAction("Brute stuck in trap");
                fsm.Stuck();
            }
        }
    }
}
