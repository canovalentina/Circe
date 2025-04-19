using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum IngredientType
{
    Herb1,
    Herb2,
    Herb3,
    Herb4,
}

public enum PotionType
{
    Red,
    Grey,
    Black,
    Orange,
    Purple,
    Yellow,
}

public enum PotionFunction
{
    Explosive,
    Transformation,
    Health,
    StoneSkin,
    Spill
}

public enum PotionCategory
{
    Throwable,
    Drinkable,
    Spillable
}

public static class RecipeConfig
{
    public static readonly Dictionary<PotionType, List<IngredientType>> Recipes = new Dictionary<PotionType, List<IngredientType>>()
    {
        { PotionType.Orange, new List<IngredientType> { IngredientType.Herb1, IngredientType.Herb2 } },
        { PotionType.Black, new List<IngredientType> { IngredientType.Herb1, IngredientType.Herb3 } },

        { PotionType.Red, new List<IngredientType> { IngredientType.Herb2, IngredientType.Herb4 } },
        { PotionType.Grey, new List<IngredientType> { IngredientType.Herb2, IngredientType.Herb3 } },
        
        { PotionType.Purple, new List<IngredientType> { IngredientType.Herb3, IngredientType.Herb4 } },
        { PotionType.Yellow, new List<IngredientType> { IngredientType.Herb1, IngredientType.Herb4 } },
    };
}

[System.Serializable]
public class IngredientToPrefabMapping
{
    public IngredientType ingredientType;
    public GameObject prefab;
}

public static class PotionData
{
    public static readonly Dictionary<PotionType, PotionFunction> PotionFunctions = new Dictionary<PotionType, PotionFunction>
    {
        { PotionType.Orange, PotionFunction.Explosive },
        { PotionType.Purple, PotionFunction.Transformation },
        { PotionType.Red, PotionFunction.Health },
        { PotionType.Grey, PotionFunction.StoneSkin },
        { PotionType.Black, PotionFunction.Spill } 
    };
    public static readonly Dictionary<PotionType, PotionCategory> PotionCategories = new Dictionary<PotionType, PotionCategory>
    {
        { PotionType.Orange, PotionCategory.Throwable },
        { PotionType.Purple, PotionCategory.Throwable },
        { PotionType.Red, PotionCategory.Drinkable },
        { PotionType.Grey, PotionCategory.Drinkable },
        { PotionType.Black, PotionCategory.Spillable }
    };
    
    public static PotionFunction? GetPotionFunction(PotionType potionType)
    {
        if (PotionFunctions.TryGetValue(potionType, out PotionFunction function))
        {
            return function;
        }
        return null;
    }
    
    public static PotionCategory? GetPotionCategory(PotionType potionType)
    {
        if (PotionCategories.TryGetValue(potionType, out PotionCategory category))
        {
            return category;
        }
        return null;
    }
}