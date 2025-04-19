using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCollider : MonoBehaviour
{
    private bool hasShownHint = false;
    public string hintText;

    public void OnTriggerEnter(Collider collider)
    {
        if (!hasShownHint && collider.gameObject.CompareTag("Player"))
        {
            TutorialController tc = collider.gameObject.GetComponent<TutorialController>();
            tc.ShowTutorialText(hintText);
            hasShownHint = true;
        }
    }
}
