using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class IngredientAmount
{
    public IngredientType ingredientType;
    public int amount;
}

public class CollectableMulti : MonoBehaviour
{
    private Inventory inventory;
    public GameObject flowers;
    private bool isBeingCollected = false;
    public float regrowTime = 30f;
    public TextMeshProUGUI regrowTimer;
    public bool canRegrow = true;
    public List<IngredientAmount> ingredientAmounts;

    private void Start()
    {
        inventory = PotionCraftingManager.Instance.Inventory;
        if (ingredientAmounts == null || ingredientAmounts.Count == 0)
        {
            ingredientAmounts = new List<IngredientAmount>();

            foreach (IngredientType ingredientType in Enum.GetValues(typeof(IngredientType)))
            {
                ingredientAmounts.Add(new IngredientAmount
                {
                    ingredientType = ingredientType,
                    amount = 10 // give 10 of each by default
                });
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null && flowers.activeInHierarchy)
        {
            PlantCollector pc = c.attachedRigidbody.gameObject.GetComponent<PlantCollector>();

            if (pc != null && c.CompareTag("Player"))
            {
                pc.PlantDetected(this);
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            PlantCollector pc = c.attachedRigidbody.gameObject.GetComponent<PlantCollector>();

            if (pc != null)
            {
                pc.ClearPlantDetected();
            }
        }
    }

    public void CollectPlant()
    {
        if (!isBeingCollected)
        {
            isBeingCollected = true;

            foreach (var ingredientAmount in ingredientAmounts)
            {
                inventory.AddIngredient(ingredientAmount.ingredientType, ingredientAmount.amount);
            }

            flowers.SetActive(false);
            if (canRegrow)
            {
                StartCoroutine(RegrowBush());
            }

            isBeingCollected = false;
        }
    }

    IEnumerator RegrowBush()
    {
        regrowTimer.gameObject.SetActive(true);
        float remainingTime = regrowTime;

        while (remainingTime > 0)
        {
            regrowTimer.text = $"<size=60%>Can reharvest in</size>\n {Mathf.CeilToInt(remainingTime)} s";
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        flowers.SetActive(true);
        regrowTimer.gameObject.SetActive(false);
    }
}

