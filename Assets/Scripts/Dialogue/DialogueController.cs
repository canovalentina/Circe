using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public void Start()
    {
        if (dialoguePanel == null) 
        {
          Debug.LogError("Panel for dialogue must be initialized.");
        }
        
        dialogueText = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        closeButton = dialoguePanel.GetComponentInChildren<Button>();
        if (closeButton != null) 
        {
          closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
    }

    public void ShowDialogueText(string text)
    {
        if (!string.IsNullOrEmpty(text)) 
        {
            Time.timeScale = 0;
            dialoguePanel.SetActive(true);
            dialogueText.text = text;
        }
    }

    public void OnCloseButtonClicked()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
    }
    
    public bool IsDialogueActive()
    {
        return dialoguePanel.activeSelf; // Checks if the dialogue is open
    }
}