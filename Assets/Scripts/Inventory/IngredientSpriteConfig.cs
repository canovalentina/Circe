using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IngredientSpriteConfig", menuName = "Ingredients/Ingredient Sprite Config")]
public class IngredientSpriteConfig : ScriptableObject
{
    [Serializable]
    public struct IngredientSpriteMapping
    {
        public IngredientType ingredientType;
        public Sprite sprite;
    }

    [Header("Add each IngredientType -> Sprite pair here")]
    public List<IngredientSpriteMapping> mappings;

    /// <summary>
    /// Returns the Sprite for the given ingredientType, or null if not found.
    /// </summary>
    public Sprite GetSprite(IngredientType type)
    {
        foreach (var mapping in mappings)
        {
            if (mapping.ingredientType == type)
            {
                return mapping.sprite;
            }
        }
        return null; // Fallback if not found
    }
}
