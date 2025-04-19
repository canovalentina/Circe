using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterHealthBarCollisionHandler healthController = other.gameObject.GetComponent<CharacterHealthBarCollisionHandler>();
            TutorialController tc = other.gameObject.GetComponent<TutorialController>();
            healthController.DecreaseHealth(100f);
            tc.CompleteStage(2);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ClearGreyStoneAndRemoveLava(other.gameObject));
        }
    }

    private IEnumerator ClearGreyStoneAndRemoveLava(GameObject circe)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        PotionsActionsController pac = circe.GetComponent<PotionsActionsController>();
        if (pac != null)
        {
            pac.CleanUpStoneSkin();
        }
        Destroy(this.gameObject);
    }
}
