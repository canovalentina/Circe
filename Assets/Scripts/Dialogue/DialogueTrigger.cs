using UnityEngine;
using System.Collections;
using TMPro;
public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TMP_Text dialogueText;
    public float detectionRange = 3f; // How close to Player for dialogue to appears
    public float typeTextDelay = 0.05f; // Delay between each letter (seconds)
    private Transform player;
    private CanvasGroup canvasGroup;
    private bool isFading = false;
    private bool isTyping = false;
    private bool hasTyped = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (dialogueCanvas == null)
        {
            Debug.LogError("DialogueCanvas is not assigned.");
            return;
        }

        if (dialogueText == null)
        {
            Debug.LogError("TextMeshProText is not assigned.");
            return;
        }

        if (dialogueCanvas.GetComponent<CanvasGroup>() == null)
        {
            dialogueCanvas.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup = dialogueCanvas.GetComponent<CanvasGroup>();
        
        // Enable script only if text is not empty
        if (string.IsNullOrWhiteSpace(dialogueText.text))
        {
            //Debug.LogWarning("Dialogue text is empty. Disabling dialogue system.");
            dialogueCanvas.SetActive(false);
            enabled = false;
            return;
        }

        canvasGroup.alpha = 0f; // Hidden
        dialogueCanvas.SetActive(true);
    }

    void Update()
    {
        if (!enabled) return;
        
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange && !isFading && !hasTyped)
        {
            string originalText = dialogueText.text.Trim();
            if (string.IsNullOrEmpty(originalText))
            {
                Debug.LogWarning("Dialogue Text is empty");
                return;
            }
            StartCoroutine(ShowDialogueCanvas(originalText));
        }
        else if (distance >= detectionRange && !isFading && hasTyped)
        {
            StartCoroutine(FadeOut());
        }
        // Debug.Log("IsFading: " + isFading + ", IsTyping: " + isTyping + ", hasTyped: " + hasTyped);
    }

    IEnumerator ShowDialogueCanvas(string message)
    {
    
        if (isFading || isTyping) {
            yield break;
        }

        isFading = true;
        dialogueText.text = "";
        yield return StartCoroutine(FadeIn());
        isFading = false;

        isTyping = true;
        yield return StartCoroutine(TypeText(message));
        isTyping = false;

        hasTyped = true;
    }

    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * 2;
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        hasTyped = false;
    }
    IEnumerator TypeText(string message)
    {
        foreach (char letter in message)
        {
            dialogueText.text += letter; // Typewriter effect
            yield return new WaitForSeconds(typeTextDelay);
        }
    }
}