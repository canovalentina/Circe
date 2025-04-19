using System.Collections;
using UnityEngine;

public class CirceHitReaction : MonoBehaviour
{
    private Animator animator;
    private static readonly int IsHit = Animator.StringToHash("isHit");

    private bool isHit = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerHitReaction()
    {

        if (isHit) return;


        Debug.Log("Circe got hit!");
        isHit = true;

        if (animator == null)
        {
            Debug.LogError("Animator not found on Circe!");
            return;
        }

        animator.SetBool(IsHit, true);

        StartCoroutine(ResetShakeAnimation());
    }

    private IEnumerator ResetShakeAnimation()
    {
        yield return new WaitForSeconds(1.0f);

        animator.SetBool(IsHit, false); 
        isHit = false; 
    }
    
}
