using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoScrollDialogue : MonoBehaviour
{
    public ScrollRect scrollRect; 
    public TextMeshProUGUI dialogueText; 
    public float scrollSpeed = 30f;
    public float initialScrollDelay = 0f;
    void Start()
    {
        if (scrollRect == null)
        {
            Debug.LogError("ScrollRect (Scroll View) is not assigned.");
            return;
        }

        if (dialogueText == null)
        {
            Debug.LogError("Dialogue Text is not assigned.");
            return;
        }

        StartCoroutine(ScrollDialogue());
    }
    

    IEnumerator ScrollDialogue()
    {
        if (initialScrollDelay > 0) 
        {
          yield return new WaitForSeconds(initialScrollDelay); 
        }
        yield return new WaitForEndOfFrame();

        scrollRect.verticalNormalizedPosition = 1f; // Reset to top
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueText.rectTransform);

        float contentHeight = dialogueText.rectTransform.sizeDelta.y;
        float viewportHeight = scrollRect.viewport.rect.height;

        if (contentHeight <= viewportHeight) 
        {
          yield break;
        }

        float startY = 1f; 
        float endY = 0f;   

        float duration = Mathf.Max(contentHeight / scrollSpeed, 3f); 
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(startY, endY, t);
            yield return null;
        }
        
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
