using UnityEngine;
using UnityEngine.SceneManagement;

public class PotionCraftingManager : MonoBehaviour
{
    public static PotionCraftingManager Instance { get; private set; }

    public Inventory Inventory { get; private set; } = new Inventory();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetCraftableCount(PotionType potion)
    {
        if (!RecipeConfig.Recipes.ContainsKey(potion))
            return 0;

        int maxCraftable = int.MaxValue;
        foreach (IngredientType ingredient in RecipeConfig.Recipes[potion])
        {
            int available = Inventory.Ingredients.ContainsKey(ingredient) ? Inventory.Ingredients[ingredient] : 0;
            if (available < maxCraftable)
                maxCraftable = available;
        }
        return maxCraftable;
    }

    public bool CraftPotion(PotionType potion)
    {
        PlaytestingLogger.LogAction("Craft potion", potion.ToString());
        if (GetCraftableCount(potion) < 1)
            return false;

        foreach (IngredientType ingredient in RecipeConfig.Recipes[potion])
        {
            Inventory.RemoveIngredient(ingredient, 1);
        }
        Inventory.AddPotion(potion, 1);
        return true;
    }

    public void ClearInventory()
    {
        Inventory.ClearInventory();
    }
}
