using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleInventory : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    //private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayInventoryKeySound();
            TogglePanelAndPause();
        }
    }

    public void TogglePanelAndPause()
    {
        bool newState = !panel.activeSelf;
        panel.SetActive(newState);
        EventSystem.current.SetSelectedGameObject(null);
        Input.ResetInputAxes();
        PlaytestingLogger.LogAction("Toggle inventory");

        if (newState)
        {
            Time.timeScale = 0f;
            //isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            //isPaused = false;
        }
    }
    
    private void PlayInventoryKeySound() 
    {
        EventManager.TriggerEvent<PressInventoryKeyEvent>();
    }
}
