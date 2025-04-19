using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PotionSpriteConfig", menuName = "Potion/Potion Sprite Config")]
public class PotionSpriteConfig : ScriptableObject
{
    [Serializable]
    public struct PotionSpriteMapping
    {
        public PotionType potionType;
        public Sprite sprite;
    }

    public List<PotionSpriteMapping> mappings;

    public Sprite GetSprite(PotionType potionType)
    {
        foreach (var mapping in mappings)
        {
            if (mapping.potionType == potionType)
            {
                return mapping.sprite;
            }
        }
        return null;  
    }
}