using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelRestarter : MonoBehaviour
{
    public void RestartLevel()
    {   
        PlaytestingLogger.LogAction("Restart level");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        SceneManager.LoadScene(sceneName);
        PotionCraftingManager.Instance.ClearInventory();
        Time.timeScale = 1f;
    }
}
