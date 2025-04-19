using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Animator))]
public class PlantCollector : MonoBehaviour
{
    private bool isPlantNear = false;
    private Animator animator;
    bool doPick;
    private CollectableMulti currentPlant = null;
    public float rotationSpeed = 1f;

    private Inventory inventory;

    private void Start()
    {
        inventory = PotionCraftingManager.Instance.Inventory;
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on ModernCirce!");
        }
    }

    public void PlantDetected(CollectableMulti plant)
    {
        isPlantNear = true;
        currentPlant = plant;
        PlaytestingLogger.LogAction("Approached plant");
    }

    public void ClearPlantDetected()
    {
        isPlantNear = false;
        currentPlant = null;
    }

    public void OnPickPlantComplete()
    {
        if (currentPlant != null)
        {
            currentPlant.CollectPlant();
            currentPlant = null;
            isPlantNear = false;
            PlaytestingLogger.LogAction("Harvest plant");
        }
    }

    private void Update()
    {
        // Check for potion and if plant is near, allow pick animation on button press
        if (isPlantNear == true && UnityEngine.Input.GetKeyDown(KeyCode.R))
        {
            doPick = true;
        }
    }

    private void FixedUpdate()
    {

        if (doPick)
        {
            doPick = false;
            isPlantNear = false;
            StartCoroutine(RotateAndPick());
            animator.SetBool("Pick", true);
        }
        else
        {
            animator.SetBool("Pick", false);
        }
    }

    private IEnumerator RotateAndPick()
    {
        if (currentPlant == null) yield break;

        Transform plantTransform = currentPlant.transform;

        // Calculate direction to face the plant
        Vector3 directionToPlant = (plantTransform.position - transform.position).normalized;
        directionToPlant.y = 0; // Prevent tilting

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlant);

        // Rotate toward the plant smoothly
        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

    }
    
    public void PlayCollectsPlant() 
    {
        EventManager.TriggerEvent<PlayerCollectsPlantEvent, GameObject>(this.gameObject);
    }

}