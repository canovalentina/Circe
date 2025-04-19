using System.Collections;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private string dialogue;
    [SerializeField] private float animationDuration = 1f;
    private Animator npcAnimator;
    private DialogueController dialogueController;
    private bool isTalking = false;
    private bool getsDisappointed;
    private bool hasShownDialogue = false;
    
    void Start()
    {        
        dialogueController = FindObjectOfType<DialogueController>();
        if (dialogueController == null) 
        {
          Debug.LogWarning("Missing DialogueController for NPC.");
        }
        
        npcAnimator = GetComponentInChildren<Animator>();
        if (npcAnimator == null) 
        {
            Debug.LogError("Missing animator in NPC!");
        }
        getsDisappointed = HasAnimatorParameter(npcAnimator, "Disappointed");
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered collider");
        Debug.Log(hasShownDialogue);
        if (!isTalking && !hasShownDialogue && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartDialogueSequence());
            hasShownDialogue = true;
        }
    }

    private IEnumerator StartDialogueSequence()
    {
        isTalking = true;
        npcAnimator.SetBool("Talk", true);

        yield return new WaitForSeconds(animationDuration);

        dialogueController.ShowDialogueText(dialogue);
        
        yield return new WaitUntil(() => !dialogueController.IsDialogueActive());

        EndDialogue();
    }

    public void EndDialogue()
    {
        if (!isTalking) return;
        
        isTalking = false;
        npcAnimator.SetBool("Talk", false);
        dialogueController.OnCloseButtonClicked();
        
        if (getsDisappointed) 
        {
          npcAnimator.SetBool("Disappointed", true);
        }
    }
 
    public void OnDisappointedAnimationEnd()
    {
        npcAnimator.SetBool("Disappointed", false); // Reset NPC to idle
    }
    
    private bool HasAnimatorParameter(Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
            {
                return true;
            }
        }
        return false;
    }
}
