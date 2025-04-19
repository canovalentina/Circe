using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{      
    public void StartGame()
    {
        PlaytestingLogger.LogAction("Start game");
        SceneManager.LoadScene(SceneNames.Tutorial, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
