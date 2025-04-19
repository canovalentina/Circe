using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostLavaJumpCollider : MonoBehaviour
{
    public GameObject lava;
    private int stage = 1;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            TutorialController tc = collider.gameObject.GetComponent<TutorialController>();
            StartCoroutine(Complete(tc));
        }
    }

    private IEnumerator Complete(TutorialController tc)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        if (tc != null)
        {
            tc.CompleteStage(this.stage);
        }

        Destroy(this.lava);
    }
}
