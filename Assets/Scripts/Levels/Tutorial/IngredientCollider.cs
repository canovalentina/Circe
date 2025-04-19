using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCollider : MonoBehaviour
{
    private static int ingredientsCollected = 0;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ingredientsCollected++;
            TutorialController tc = other.gameObject.GetComponent<TutorialController>();
            if (ingredientsCollected == 1)
            {   
                tc.ShowTutorialText("Congrats! You've collected your first potion ingredient. Press <b>C</b> to open your inventory and view your ingredients and potions. Ingredients are limited; don't craft anything yet. You'll need them soon.");
            }

            if (ingredientsCollected == 4)
            {
                tc.CompleteStage(0);
            }
        }
    }

    void Start()
    {
        ingredientsCollected = 0;
    }
}
