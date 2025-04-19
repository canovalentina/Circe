using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitPortalController : MonoBehaviour
{
    public string nextSceneName = "Island Level 1"; 
    public string logText = "Tutorial completed";
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Circe entered exit portal");
        PlaytestingLogger.LogAction(logText);
        if (nextSceneName == "Island Level 1 Extended")
        {
            PotionCraftingManager.Instance.ClearInventory();
        }
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);

    }
}
