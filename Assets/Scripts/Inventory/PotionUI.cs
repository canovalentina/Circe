using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

public class PotionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PotionType potionType;
    [SerializeField] private Image potionFill;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button craftButton;
    [SerializeField] private PotionColorConfig colorConfig;
    [SerializeField] private GameObject tooltipPanel;

    private PotionCraftingManager craftingManager;
    private List<IngredientUI> allIngredients;

    private void Start()
    {
        if (craftingManager == null) {
            OnEnable();
        }

        allIngredients = FindObjectsOfType<IngredientUI>().ToList();
    }


    private void OnEnable()
    {
        craftingManager = PotionCraftingManager.Instance;
        if (craftingManager != null && craftingManager.Inventory != null)
        {
            craftingManager.Inventory.OnInventoryChanged += UpdateUI;
        }
        if (craftButton != null)
        {
            craftButton.onClick.AddListener(OnClickCraft);
        }
        restoreState();
        UpdateUI();
    }

    private void OnDisable()
    {
        if (craftingManager != null && craftingManager.Inventory != null)
        {
            craftingManager.Inventory.OnInventoryChanged -= UpdateUI;
        }
        if (craftButton != null)
        {
            craftButton.onClick.RemoveListener(OnClickCraft);
        }
    }

    private void UpdateUI()
    {

        if (craftingManager != null && craftingManager.Inventory != null)
        {
            int currentCount;
            craftingManager.Inventory.Potions.TryGetValue(potionType, out currentCount);
            if (countText != null)
            {
                countText.text = currentCount.ToString();
            }
        }

        if (craftButton != null)
        {
            int craftableCount = craftingManager.GetCraftableCount(potionType);
            craftButton.interactable = (craftableCount > 0);
        }

        ApplyColorFromConfig();
    }

    private void OnClickCraft()
    {
        Debug.Log("crafitg");
        Debug.Log(craftingManager);
        if (craftingManager == null) return;

        bool success = craftingManager.CraftPotion(potionType);
        if (!success)
        {
            Debug.LogWarning($"Not enough ingredients to craft {potionType} potion!");
        }
    }

    private void OnValidate()
    {
        ApplyColorFromConfig();
    }

    private void ApplyColorFromConfig()
    {
        if (potionFill != null && colorConfig != null)
        {
            Color c = colorConfig.GetColor(potionType);
            potionFill.color = c;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipPanel != null)
        {
            EventManager.TriggerEvent<PressInventoryKeyEvent>();
            List<IngredientType> requiredIngredients = RecipeConfig.Recipes[potionType];
            foreach (var ingredientUi in allIngredients)
            {
                bool isNeeded = requiredIngredients.Contains(ingredientUi.GetIngredientType());
                ingredientUi.Highlight(isNeeded);
            }
            tooltipPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        restoreState();
    }

    private void restoreState()
    {  
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
            if (allIngredients != null)
            {
                foreach (var ingredientUi in allIngredients)
                {
                    ingredientUi.Highlight(true);
                }
            }
        }
    }
}
