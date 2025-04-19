using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PotionColorConfig", menuName = "Potion/Potion Color Config")]
public class PotionColorConfig : ScriptableObject
{
    [Serializable]
    public struct PotionColorMapping
    {
        public PotionType potionType;
        public Color color;
    }

    public List<PotionColorMapping> mappings;

    /// <summary>
    /// Returns the Color for the given potionType, or Color.white if not found.
    /// </summary>
    public Color GetColor(PotionType potionType)
    {
        foreach (var mapping in mappings)
        {
            if (mapping.potionType == potionType)
            {
                return mapping.color;
            }
        }
        return Color.white;
    }
}
