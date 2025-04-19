using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AthenaHealthBarHandler : MonoBehaviour
{
    public float maxHealth = 8f;
    public float health = 8f;
    [SerializeField] private Slider healthSlider;
    private Animator animator;
    private bool isDead = false;
    public bool shouldTransformOnDeath = false;

    void Start()
    {

        animator = GetComponent<Animator>();
        
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
        else
        {
            Debug.LogWarning("Athena health bar slider reference is missing.");
        }
    }
    
    public void DecreaseHealth(float damage)
    {
        if (isDead) return;
        Debug.Log("Decreasing Athena health");
        
        //Decrease health  
        health -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        if (health <= 0)
        {
            HandleAthenaDeath();
        }
    }

    public void RestoreHealth()
    {
        health = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void HandleAthenaDeath()
    {
        isDead = true;
        Debug.Log("Athena is dead!");
        PlaytestingLogger.LogAction("Athena dead");
        
        if (animator != null) 
        {
            animator.SetBool("Dead", true);
        }
        
    }

}
