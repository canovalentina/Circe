using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksCollider : MonoBehaviour
{
    public static bool hasEverHitCirce;

    public void Awake()
    {
        hasEverHitCirce = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // it's enough to hit circe once with a brick, otherwise there's a risk of killing her during tutorial
        if (collision.gameObject.CompareTag("Player") && !hasEverHitCirce)
        {
            CharacterHealthBarCollisionHandler healthController = collision.gameObject.GetComponent<CharacterHealthBarCollisionHandler>();
            TutorialController tc = collision.gameObject.GetComponent<TutorialController>();
            hasEverHitCirce = true;
            healthController.DecreaseHealth(3f);
            tc.ShowTutorialText("Ouch! That hit cost you some health! If it drops to zero, you're done for. Craft a <b><color=red>Healing (red)</color></b> potion and press <b>Num1</b> to restore your strength.");
            tc.CompleteStage(3);
        }
    }
}
