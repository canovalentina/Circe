using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientUI : MonoBehaviour
{
    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private Image ingredientIcon;        
    [SerializeField] private TextMeshProUGUI countText;   
    [SerializeField] private IngredientSpriteConfig spriteConfig;
    [SerializeField] private CanvasGroup overlay;

    private Inventory inventory;

    private void OnEnable()
    {
        inventory = PotionCraftingManager.Instance.Inventory;
        if (inventory != null)
        {
            inventory.OnInventoryChanged += UpdateUI;
        }

        UpdateUI();
    }

    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnInventoryChanged -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        if (inventory != null && countText != null)
        {
            int currentCount = 0;
            inventory.Ingredients.TryGetValue(ingredientType, out currentCount);
            countText.text = currentCount.ToString();
        }

        ApplySpriteFromConfig();
    }

    private void ApplySpriteFromConfig()
    {
        if (ingredientIcon != null && spriteConfig != null)
        {
            Sprite s = spriteConfig.GetSprite(ingredientType);
            ingredientIcon.sprite = s;
        }
    }

    private void OnValidate()
    {
        ApplySpriteFromConfig();
    }

    public IngredientType GetIngredientType()
    {
        return ingredientType;
    }

    public void Highlight(bool toggle)
    {
        if (toggle)
        {
            overlay.alpha = 1f;
        }
        else
        {
            overlay.alpha = 0.5f;
        }
    }
}
