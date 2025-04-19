using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public bool hasFirstIngredient;
    public bool hasSecondIngredient;
    public GameObject panel;
    public Button closeBtn;
    public TextMeshProUGUI tutorialText;
    public GameObject[] gates;

    public void Start()
    {
        PlaytestingLogger.LogAction("Start Tutorial");
        closeBtn.onClick.AddListener(OnCloseButtonClicked);
    }

    public void ShowTutorialText(string text)
    {
        StartCoroutine(ShowTutorialWithDelay(text));
    }

    private IEnumerator ShowTutorialWithDelay(string text)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 0;
        panel.SetActive(true);
        tutorialText.text = text;
    }

    public void OnCloseButtonClicked()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void CompleteStage(int stageIndex)
    {
        PlaytestingLogger.LogAction($"Completed stage {stageIndex} of the tutorial");
        gates[stageIndex].GetComponent<GateScript>().Open();
    }
}
