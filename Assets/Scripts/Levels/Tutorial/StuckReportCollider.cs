using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckReportCollider : MonoBehaviour
{
    private GameObject circe;
    private bool hasShownHint = false;

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Brute"))
        {
            if (!hasShownHint)
            {
                GameObject circe = GameObject.FindWithTag("Player");
                TutorialController tc = circe.gameObject.GetComponent<TutorialController>();
                hasShownHint = true;
                tc.ShowTutorialText("Great job, the enemy is trapped! You can finish it off with an <b><color=orange>orange potion</color></b>, or press <b>Left Shift</b> to throw a <b><color=purple>purple potion</color></b> and transform it into a harmless animal once it's weak. After, proceed to the statue at the end of the room.");
            }
        }
    }

}
