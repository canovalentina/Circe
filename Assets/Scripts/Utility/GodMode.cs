using UnityEngine;

public class GodMode : MonoBehaviour
{
    public static bool isGodMode = false;
    private Inventory inventory;

    private void Start()
    {
        inventory = PotionCraftingManager.Instance.Inventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGodMode = !isGodMode;
            inventory.setGodMode(isGodMode);
            EventManager.TriggerEvent<GodModeToggledEvent>();
        } 
    }
}