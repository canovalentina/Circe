using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BruteHealthBarHandler : MonoBehaviour
{
    public float maxHealth = 2f;
    public float health = 2f;
    [SerializeField] private Slider healthSlider;
    private Animator animator;
    private bool isDead = false;

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
        Debug.Log("Decreasing Brute health");
        
        //Decrease health  
        health -= damage;
        health = Mathf.Max(health, 0);
        
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    public void RestoreHealth()
    {
        health = maxHealth;
        healthSlider.value = maxHealth;
    }

}
