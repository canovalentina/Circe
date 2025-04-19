using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject inventoryCraftingPanel;
    [SerializeField] private GameObject potionsCounterPanel;
    [SerializeField] private GameObject gameLogPanel;
    [SerializeField] private GameObject gameWonPanel;

    private GameObject activePanel = null;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        inventoryCraftingPanel.SetActive(false);
        potionsCounterPanel.SetActive(true);
        gameLogPanel.SetActive(true);  
        gameWonPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlaytestingLogger.LogAction("Inventory panel toggled");
            PlayInventoryKeySound();
            ToggleInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlaytestingLogger.LogAction("Pause menu panel toggled");
            PlayPauseKeySound();
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        TogglePanel(pauseMenuPanel);
    }

    public void ToggleInventory()
    {
        TogglePanel(inventoryCraftingPanel);
    }
    
    public void ShowGameOver()
    {
        PlaytestingLogger.LogAction("Game over panel activated");
        
        if (activePanel != null && activePanel != gameOverPanel)
        {
            activePanel.SetActive(false);
            activePanel = null;
        }
        
        gameOverPanel.SetActive(true);
        activePanel = gameOverPanel;        
        Time.timeScale = 0f;
        
        UpdatePotionsCounterVisibility();
        UpdateGameLogVisibility();
        HandleBackgroundMusic();
        
        PlayGameOverSound();
    }
    
    public void ShowGameWon()
    {
        PlaytestingLogger.LogAction("Game won panel activated");
        PlaytestingLogger.LogAction("Game won!");
        
        if (activePanel != null && activePanel != gameWonPanel)
        {
            activePanel.SetActive(false);
            activePanel = null;
        }
        
        gameWonPanel.SetActive(true);
        activePanel = gameWonPanel;        
        Time.timeScale = 0f;
        
        UpdatePotionsCounterVisibility();
        UpdateGameLogVisibility();
        HandleBackgroundMusic();
        
        PlayGameWonSound();
    }

    private void TogglePanel(GameObject panel)
    {
        if (panel == null || panel == gameOverPanel) return;

        bool newState = !panel.activeSelf;

        if (activePanel != null && activePanel != panel && activePanel != potionsCounterPanel)
        {
            activePanel.SetActive(false);
        }

        panel.SetActive(newState);
        // EventSystem.current.SetSelectedGameObject(null);
        // Input.ResetInputAxes();
        if (newState) 
        {
          activePanel = panel;
        }
        else 
        {
          activePanel = null;
        }
        
        UpdatePotionsCounterVisibility();
        UpdateGameLogVisibility();
        HandleBackgroundMusic();

        // Pause/unpause the game
        if (activePanel != null) 
        {
          Time.timeScale = 0f;
        }
        else 
        {
          Time.timeScale = 1f;
        }
    }
    
    private void UpdateGameLogVisibility()
    {
        if (pauseMenuPanel.activeSelf || inventoryCraftingPanel.activeSelf || gameOverPanel.activeSelf || gameWonPanel.activeSelf) 
        {
          gameLogPanel.SetActive(false);
        }
        else 
        {
          gameLogPanel.SetActive(true);
        }
    }
    
    private void HandleBackgroundMusic() 
    {
        if (pauseMenuPanel.activeSelf) 
        {
            Debug.Log("Pausing background music."); 
            PauseBackgroundMusic();
        }
        else if (gameOverPanel.activeSelf || gameWonPanel.activeSelf) 
        {
            Debug.Log("Stoping background music."); 
            StopBackgroundMusic();
        }
        else 
        {
            Debug.Log("Resuming background music.");
            ResumeBackgroundMusic();
        }
    }
    
    private void UpdatePotionsCounterVisibility()
    {

        if (gameOverPanel.activeSelf || gameWonPanel.activeSelf) 
        {
          potionsCounterPanel.SetActive(false);
        }
        else 
        {
          potionsCounterPanel.SetActive(true);
        }
    }
    
    private void PlayInventoryKeySound()
    {
        EventManager.TriggerEvent<PressInventoryKeyEvent>();
    }

    private void PlayPauseKeySound()
    {
        EventManager.TriggerEvent<PressPauseKeyEvent>();
    }
    
    private void PlayGameOverSound() 
    {
        Debug.Log("Playing Game Over sound");
        EventManager.TriggerEvent<GameOverEvent>();
    }
    
    private void PlayGameWonSound() 
    {
        Debug.Log("Playing Game Won sound");
        EventManager.TriggerEvent<GameWonEvent>();
    }
    
    private void PauseBackgroundMusic() 
    {
        EventManager.TriggerEvent<BackgroundMusicEvents.PauseMusicEvent>();
    }
    
    private void ResumeBackgroundMusic() 
    {
        EventManager.TriggerEvent<BackgroundMusicEvents.ResumeMusicEvent>();
    }
    
    private void StopBackgroundMusic() 
    {
        EventManager.TriggerEvent<BackgroundMusicEvents.StopMusicEvent>();
    }
}

