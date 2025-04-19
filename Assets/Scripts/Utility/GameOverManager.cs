using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; 

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        PlayGameOverSound();

        gameOverPanel.SetActive(true);


        Time.timeScale = 0f;
    }
    
    private void PlayGameOverSound() 
    {
        //Debug.Log("Playing Game Over sound");
        EventManager.TriggerEvent<GameOverEvent>();
    }
}