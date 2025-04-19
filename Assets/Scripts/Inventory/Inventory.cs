using System.Collections.Generic;
using System;

public class Inventory
{
    public Dictionary<IngredientType, int> Ingredients { get; private set; } = new Dictionary<IngredientType, int>();

    public Dictionary<PotionType, int> Potions { get; private set; } = new Dictionary<PotionType, int>();

    public event Action OnInventoryChanged;
    private bool isGodeMode;


    public void AddIngredient(IngredientType ingredient, int amount)
    {
        if (Ingredients.ContainsKey(ingredient))
            Ingredients[ingredient] += amount;
        else
            Ingredients[ingredient] = amount;

        OnInventoryChanged?.Invoke();
    }

    public bool RemoveIngredient(IngredientType ingredient, int amount)
    {
        if (isGodeMode) return false;

        if (Ingredients.ContainsKey(ingredient) && Ingredients[ingredient] >= amount)
        {
            Ingredients[ingredient] -= amount;
            OnInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void AddPotion(PotionType potion, int amount)
    {
        if (Potions.ContainsKey(potion))
            Potions[potion] += amount;
        else
            Potions[potion] = amount;
        OnInventoryChanged?.Invoke();
    }

    public bool RemovePotion(PotionType potion, int amount)
    {
        if (isGodeMode)
        {
            OnInventoryChanged?.Invoke();
            return false;
        }

        if (Potions.ContainsKey(potion) && Potions[potion] >= amount)
        {
            Potions[potion] -= amount;
            OnInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void setGodMode(bool isGodModeVal)
    {
        isGodeMode = isGodModeVal;
        if (isGodeMode)
        {
            // since in god mode we do not remove anything, it's enough to give 1 of everything
            foreach (PotionType value in Enum.GetValues(typeof(PotionType)))
            {
                AddPotion(value, 1);
            }

            foreach (IngredientType value in Enum.GetValues(typeof(IngredientType)))
            {
                AddIngredient(value, UnityEngine.Random.Range(5, 11));
            }
        }
    }

    public void ClearInventory()
    {
        Ingredients = new Dictionary<IngredientType, int>();
        Potions = new Dictionary<PotionType, int>();
        OnInventoryChanged?.Invoke();
    }
}
