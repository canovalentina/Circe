using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{       
    public void RestartGame()
    {
        PlaytestingLogger.LogAction("Restart game");
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        PotionCraftingManager.Instance.ClearInventory();

        if (sceneName == SceneNames.Tutorial)
        {
            SceneManager.LoadScene(SceneNames.Tutorial, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(SceneNames.Level1, LoadSceneMode.Single);
        }

        Time.timeScale = 1f;
    } 
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.RestartMusic();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
