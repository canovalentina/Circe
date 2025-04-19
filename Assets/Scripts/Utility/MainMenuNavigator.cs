using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenuNavigator : MonoBehaviour
{   
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
        Time.timeScale = 1f;
    }
}
