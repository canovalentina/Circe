using TMPro;
using UnityEngine;
using System.Collections;

public class UIManagerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject circeStoryPanel;
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private float typeSpeed = 0.01f;
    private bool hasTyped = false;
    private string fullStoryText;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        circeStoryPanel.SetActive(false);
        
        fullStoryText = storyText.text;
    }

    public void ShowCirceStory()
    {
        circeStoryPanel.SetActive(true);
        
        if (!hasTyped)
        {
            StartCoroutine(TypewriterEffect());
            hasTyped = true;
        }
    }

    public void CloseCirceStory()
    {
        circeStoryPanel.SetActive(false);
    }
    
    private IEnumerator TypewriterEffect()
    {
        storyText.text = "";

        foreach (char c in fullStoryText)
        {
            storyText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
  
}
