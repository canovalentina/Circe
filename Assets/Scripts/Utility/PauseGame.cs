using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Game starts in paused mode
    private bool isPaused = true; 

    void Start()
    {
        // Start the game in paused mode
        Time.timeScale = 0f;
    }

    void Update()
    {
        // Toggle pause/unpause on "P" key press
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (isPaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGameAction();
            }
        }
    }

    void PauseGameAction()
    {
        // Stop time
        Time.timeScale = 0f; 
        isPaused = true;
        Debug.Log("Game Paused");
    }

    void UnpauseGame()
    {
        // Resume time
        Time.timeScale = 1f; 
        isPaused = false;
        Debug.Log("Game Unpaused");
    }
}