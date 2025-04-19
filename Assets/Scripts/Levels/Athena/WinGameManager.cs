using UnityEngine;
using System.Collections;

public class WinGameManager : MonoBehaviour
{
    private static WinGameManager instance;
    private bool gameWonAlready = false;

    public static WinGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WinGameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("WinGameManager");
                    instance = go.AddComponent<WinGameManager>();
                }
            }
            return instance;
        }
    }    

    public void TriggerGameWon(float delay=2f)
    {
        if (!gameWonAlready)
        {
            gameWonAlready = true;
            StartCoroutine(DelayedGameWon(delay));
        }
    }

    private IEnumerator DelayedGameWon(float delay)
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("Game won triggered after delay!");

        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.ShowGameWon();
        }
        else
        {
            Debug.LogWarning("UIManager not found.");
        }
    }
}
