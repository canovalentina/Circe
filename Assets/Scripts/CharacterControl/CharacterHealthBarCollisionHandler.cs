using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CharacterHealthBarCollisionHandler : MonoBehaviour
{
    public float maxHealth = 5f;
    public float health = 5f;
    private Slider healthSlider;

    private Animator animator;
    private bool isDead = false;
    private bool isStoneSkin = false;
    private static readonly int IsDead = Animator.StringToHash("isDead");

    void Start()
    {

        healthSlider = GameObject.FindWithTag("Healthbar").GetComponent<Slider>();
        animator = GetComponent<Animator>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
        else
        {
            Debug.LogWarning("Health bar slider reference is missing.");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Tag for enemies that damage your health
        if (collision.gameObject.CompareTag("DamageEnemy"))
        {
            // Show the health bar when colliding with DamageEnemy
            if (healthSlider != null)
            {
                healthSlider.gameObject.SetActive(true);
            }
            DecreaseHealth(1f);
        }

        // Tag for enemies that kill you immediately
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            KillPlayerImmediately();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageEnemy"))
        {
            // Hide the health bar when the collision ends
            if (healthSlider != null)
            {
                healthSlider.gameObject.SetActive(false);
            }
        }
    }

    public void DecreaseHealth(float damage)
    {
        if (isDead || isStoneSkin || GodMode.isGodMode) return;
        
        //Decrease health  
        health -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        if (health <= 0)
        {
            StartCoroutine(HandleDeath());
            //GameOver();
        }
    }

    public void RestoreHealth()
    {
        health = 5f;
        healthSlider.value = 5f;
    }

    public void SetStoneSkin(bool val)
    {
        isStoneSkin = val;
    }

    private IEnumerator HandleDeath()
    {
        if (isDead) yield break;
        isDead = true;

        Debug.Log("Circe is dead!");
        PlaytestingLogger.LogAction("Player dead");
        animator.SetBool(IsDead, true);

        yield return new WaitForSeconds(2.0f);

        GameOver();
    }

    private void KillPlayerImmediately()
    {
        health = 0;
        if (healthSlider != null)
        {
            healthSlider.value = 0;
        }
        GameOver();
    }
    
    private void GameOver()
    {
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
        
        UIManager uIManager = FindObjectOfType<UIManager>();
        if (uIManager != null)
        {
          uIManager.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("UIManager not found in the scene.");
            
            // VC: Keeping to avoid errors but all scenes should change to using UI Manager
            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
            else
            {
                Debug.LogWarning("GameOverManager not found in the scene.");
            }
        }

    }

}
