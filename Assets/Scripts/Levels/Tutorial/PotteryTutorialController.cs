using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotteryTutorialController : MonoBehaviour
{
    public GameObject circe;
    private static bool hasShownTutorial = false;

    void Start()
    {
        hasShownTutorial = false;
    }

    void OnEnable()
    {
        EventManager.StartListening<PlayerBreaksPotteryEvent, GameObject>(OnPlayerBreaksPottery);
    }

    void OnDisable()
    {
        EventManager.StopListening<PlayerBreaksPotteryEvent, GameObject>(OnPlayerBreaksPottery);
    }

    void OnPlayerBreaksPottery(GameObject obj)
    {
        if (!hasShownTutorial) {
            hasShownTutorial = true;
            TutorialController tc = circe.gameObject.GetComponent<TutorialController>();
            tc.ShowTutorialText("Pottery can be broken with <b><color=orange>orange</color></b> potions. Inside, you might find ingredients for brewing, or rare quest items.");
        }
    }

}
