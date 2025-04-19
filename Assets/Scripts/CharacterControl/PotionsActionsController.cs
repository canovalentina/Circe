using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

[RequireComponent(typeof(Animator))]
public class PotionsActionsController : MonoBehaviour
{
    private Transform handHold;
    public Rigidbody explosivePotionPrefab; // Orange
    public Rigidbody transformationPotionPrefab; // Purple
    public Rigidbody healthPotionPrefab; // Red
    public Rigidbody stoneSkinPotionPrefab; // Gray
    public Rigidbody spillPotionPrefab; // Black
    public Rigidbody targetSpillPotionPrefab; // Yellow

    public GameObject puddlePrefab;
    public Transform puddleSpawnPoint;

    public Material materialStone;
    public Material materialBasic;
    private Animator animator;
    private Rigidbody currPotion;
    bool doThrow, doDrink, doSpill, doSpillOnTarget, doStandingJump;
    private PotionType throwablePotion = PotionType.Orange;
    private PotionType drinkingPotion = PotionType.Red;
    private PotionType spillingPotion = PotionType.Black;
    public float throwForce = 20f;
    private CharacterHealthBarCollisionHandler healthController;
    private Renderer circeRenderer;

    private List<Transform> brutes = new List<Transform>();
    public float autoHitDistance = 7f;

    private Inventory inventory;

    //private SpillTrigger spillTrigger;
    private Transform vaseTransform;
    public float rotationSpeed = 1f;
    public AbstractDoor currentDoor { get; private set; }

    private Rigidbody circe;
    private bool isStandingJump = false;
    private bool isRunningJump = false;
    public float circeJumpHeight = 1.5f;

    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    private void Start()
    {
        inventory = PotionCraftingManager.Instance.Inventory;

        if (inventory != null)
        {
            inventory.OnInventoryChanged += LoadPotion;
        }

        LoadPotion();

        /*spillTrigger = FindObjectOfType<SpillTrigger>();

        if (spillTrigger == null)
        {
            Debug.Log("SpillTrigger not found in the scene!");
        }
        else
        {
            Transform parentObject = spillTrigger.transform.parent;
            if (parentObject != null)
            {
                vaseTransform = parentObject.Find("Cauldron Heavy");

                if (vaseTransform == null)
                {
                    Debug.LogError("Cauldron Heavy not found in the scene!");
                }
            }
        }*/
        circe = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        handHold = this.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm/mixamorig:LeftHand/PotionHoldSpot");

        if (handHold == null)
        {
            Debug.LogError("PotionHoldSpot not found in hierarchy.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on ModernCirce!");
        }

        if (explosivePotionPrefab == null || transformationPotionPrefab == null || healthPotionPrefab == null || stoneSkinPotionPrefab == null || spillPotionPrefab == null)
        {
            Debug.LogError("Potion prefabs not assigned in the Inspector.");
        }

        healthController = GetComponent<CharacterHealthBarCollisionHandler>();
        if (healthController == null)
        {
            Debug.LogError("Health controller script is not attached to Circe");
        }

        Transform child = transform.Find("Peasant_girl");
        if (child != null)
        {
            circeRenderer = child.GetComponent<Renderer>();
        }

        if (circeRenderer == null)
        {
            Debug.LogError("moder renderer is not found");
        }
    }

    private void LoadPotion()
    {
        if (currPotion != null)
        {
            Destroy(currPotion.gameObject);
            currPotion = null;
        }

        if (LoadThrowablePotion(PotionType.Orange, explosivePotionPrefab)) return;
        if (LoadThrowablePotion(PotionType.Purple, transformationPotionPrefab)) return;

        // No throwable potions left
        currPotion = null;
    }

    private bool LoadThrowablePotion(PotionType potionType, Rigidbody potion)
    {
        if (isPotionTypeAvailable(potionType))
        {
            throwablePotion = potionType;
            currPotion = Instantiate(potion, handHold);
            currPotion.transform.localPosition = Vector3.zero;
            currPotion.isKinematic = true;
            Debug.Log("Going to set collisionHandler to false now");
            EnableCollisionHandler(false);
            return true;
        }
        return false;
    }

    private void EnableCollisionHandler(bool enabled)
    {
        ExplosivePotionCollisionHandler explosiveHandler = currPotion.GetComponent<ExplosivePotionCollisionHandler>();

        TransformationPotionCollisionHandler transformationHandler = currPotion.GetComponent<TransformationPotionCollisionHandler>();

        if (explosiveHandler != null)
        {
            Debug.Log($"Enabled = {enabled}: ExplosivePotionCollisionHandler");
            explosiveHandler.enabled = enabled;
        }

        if (transformationHandler != null)
        {
            Debug.Log($"Enabled = {enabled}: TransformationPotionCollisionHandler");
            transformationHandler.enabled = enabled;
        }

    }

    public void ThrowPotion()
    {
        if (currPotion == null) return;

        PlaytestingLogger.LogAction("Potion thrown", throwablePotion.ToString());
        Transform brute = getClosestAndAliveBrute();

        if (brute == null)
        {
            ThrowWithPhysics();
            currPotion = null;
            inventory.RemovePotion(throwablePotion, 1);
            return;
        }

        float distanceToBrute = brute != null ? Vector3.Distance(transform.position, brute.position) : float.MaxValue;

        // Check if brute is within the auto-hit range
        if (distanceToBrute <= autoHitDistance)
        {
            Debug.Log($"Brute is within {autoHitDistance}! Forcing potion to hit Brute.");
            ForceHitBrute(brute);
        }
        else
        {
            Debug.Log($"Brute is outside {autoHitDistance} - normal throw.");
            ThrowWithPhysics();
        }

        currPotion = null;
        inventory.RemovePotion(throwablePotion, 1);
        LoadPotion();
    }

    private bool IsBruteDead(Transform brute)
    {
        if (brute == null) return true;

        Animator bruteAnimator = brute.GetComponent<Animator>();
        if (bruteAnimator == null) return true;

        return bruteAnimator.GetCurrentAnimatorStateInfo(1).IsName("Dead");
    }

    private Transform getClosestAndAliveBrute()
    {
        return brutes
            .Where(brute => !IsBruteDead(brute))
            .OrderBy(brute => Vector3.Distance(transform.position, brute.position))
            .FirstOrDefault();
    }

    private void ForceHitBrute(Transform brute)
    {
        if (currPotion == null || brute == null) return;

        EnableCollisionHandler(true);

        currPotion.transform.parent = null;
        currPotion.isKinematic = false;
        currPotion.velocity = Vector3.zero;
        currPotion.angularVelocity = Vector3.zero;
        currPotion.gameObject.tag = "Potion";

        Vector3 predictedPosition = PredictBruteFuturePosition(brute);

        StartCoroutine(MovePotionToTarget(currPotion, predictedPosition));
    }

    private Vector3 PredictBruteFuturePosition(Transform brute)
    {
        if (brute == null) return brute.position;

        NavMeshAgent bruteAgent = brute.GetComponent<NavMeshAgent>();
        if (bruteAgent == null) return brute.position;

        Vector3 bruteVelocity = bruteAgent.velocity;
        float bruteSpeed = bruteAgent.speed;

        float distanceToBrute = Vector3.Distance(transform.position, brute.position);
        float estimatedTimeToImpact = distanceToBrute / throwForce;

        Vector3 futurePosition = brute.position + (bruteVelocity * estimatedTimeToImpact);

        return futurePosition;
    }

    private IEnumerator MovePotionToTarget(Rigidbody potion, Vector3 targetPosition)
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Vector3 startPosition = potion.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (potion != null)
            {
                potion.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            }
            yield return null;
        }
        if (potion != null)
        {
            potion.transform.position = targetPosition;
        }
    }

    private void ThrowWithPhysics()
    {
        if (currPotion == null) return;

        EnableCollisionHandler(true);
        currPotion.transform.parent = null;
        currPotion.isKinematic = false;
        currPotion.velocity = Vector3.zero;
        currPotion.angularVelocity = Vector3.zero;
        currPotion.gameObject.tag = "Potion";

        currPotion.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    private void Update()
    {

        // Check for potion and allow throw on button press Space
        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlaytestingLogger.LogAction("Throw requested", PotionType.Orange.ToString());
            throwExplosivePotion();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlaytestingLogger.LogAction("Throw requested", PotionType.Purple.ToString());
            throwTransformationPotion();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaytestingLogger.LogAction("Drink requested", PotionType.Red.ToString());
            drinkHealth();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaytestingLogger.LogAction("Drink requested", PotionType.Grey.ToString());
            drinkStoneSkin();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaytestingLogger.LogAction("Spill requested", PotionType.Black.ToString());
            spillPotion();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4) && vaseTransform != null)
        {
            PlaytestingLogger.LogAction("Spill on target requested", PotionType.Yellow.ToString());
            spillPotionOnTarget();
        }

        bool isGrounded = CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && !isStandingJump && isGrounded)
        {
            PlaytestingLogger.LogAction("Standing jump");
            standingJump();
        }
    }

    private bool isPotionTypeAvailable(PotionType potionType)
    {
        return inventory.Potions.ContainsKey(potionType) && inventory.Potions[potionType] > 0;
    }

    private void throwExplosivePotion()
    {
        if (currPotion != null && isPotionTypeAvailable(PotionType.Orange))
        {
            throwablePotion = PotionType.Orange;
            doThrow = true;
        }
    }

    private void throwTransformationPotion()
    {
        if (isPotionTypeAvailable(PotionType.Purple))
        {
            throwablePotion = PotionType.Purple;
            PlaytestingLogger.LogAction("Potion thrown", throwablePotion.ToString());
            if (currPotion != null)
            {
                Destroy(currPotion.gameObject);
                currPotion = null;
            }
            currPotion = Instantiate(transformationPotionPrefab, handHold);
            currPotion.transform.localPosition = Vector3.zero;
            currPotion.isKinematic = true;
            doThrow = true;
        }
    }

    private void drinkHealth()
    {
        if (isPotionTypeAvailable(PotionType.Red))
        {
            drinkingPotion = PotionType.Red;
            PlaytestingLogger.LogAction("Potion drunk", drinkingPotion.ToString());
            if (currPotion != null)
            {
                Destroy(currPotion.gameObject);
                currPotion = null;
            }
            currPotion = Instantiate(healthPotionPrefab, handHold);
            currPotion.transform.localPosition = Vector3.zero;
            currPotion.isKinematic = true;
            doDrink = true;
        }
    }

    private void drinkStoneSkin()
    {
        if (isPotionTypeAvailable(PotionType.Grey))
        {
            drinkingPotion = PotionType.Grey;
            PlaytestingLogger.LogAction("Potion drunk", drinkingPotion.ToString());
            if (currPotion != null)
            {
                Destroy(currPotion.gameObject);
                currPotion = null;
            }
            currPotion = Instantiate(stoneSkinPotionPrefab, handHold);
            currPotion.transform.localPosition = Vector3.zero;
            currPotion.isKinematic = true;
            doDrink = true;
        }
    }

    private void spillPotion()
    {
        if (isPotionTypeAvailable(PotionType.Black))
        {
            spillingPotion = PotionType.Black;
            PlaytestingLogger.LogAction("Potion spilled", spillingPotion.ToString());
            if (currPotion != null)
            {
                Destroy(currPotion.gameObject);
                currPotion = null;
            }
            currPotion = Instantiate(spillPotionPrefab, handHold);
            currPotion.transform.localPosition = Vector3.zero;
            currPotion.isKinematic = true;
            Instantiate(puddlePrefab, puddleSpawnPoint.position, gameObject.transform.rotation);
            doSpill = true;
        }
    }

    private void spillPotionOnTarget()
    {
        if (inventory.Potions.ContainsKey(PotionType.Yellow) && inventory.Potions[PotionType.Yellow] > 0)
        {
            spillingPotion = PotionType.Yellow;
            PlaytestingLogger.LogAction("Potion spilled", spillingPotion.ToString());
            if (currPotion != null)
            {
                Destroy(currPotion.gameObject);
                currPotion = null;
            }
            currPotion = Instantiate(targetSpillPotionPrefab, handHold);
            currPotion.transform.localPosition = Vector3.zero;
            currPotion.isKinematic = true;
            doSpillOnTarget = true;
        }
    }

    private void standingJump()
    {
        doStandingJump = true;
    }

    public void OnDrinkingComplete()
    {
        Destroy(currPotion.gameObject);
        currPotion = null;
        inventory.RemovePotion(drinkingPotion, 1);
        if (drinkingPotion == PotionType.Red)
        {
            healthController.RestoreHealth();
        }

        if (drinkingPotion == PotionType.Grey)
        {
            applyStoneSkin();
        }

        LoadPotion();
    }

    public void onSpillingComplete()
    {
        Destroy(currPotion.gameObject);
        currPotion = null;
        inventory.RemovePotion(spillingPotion, 1);
        LoadPotion();
    }

    public void onSpillingOnTargetComplete()
    {
        Destroy(currPotion.gameObject);
        currPotion = null;
        inventory.RemovePotion(spillingPotion, 1);

        //FenceDoorController doorController = FindObjectOfType<FenceDoorController>();
        if (currentDoor != null)
        {
            currentDoor.OpenDoor();
        }
        else
        {
            Debug.LogError("No FenceDoorController found in the scene!");
        }

        LoadPotion();
    }

    private void applyStoneSkin()
    {
        healthController.SetStoneSkin(true);
        if (circeRenderer != null)
        {
            circeRenderer.material = materialStone; // Apply new material
        }
        Invoke("CleanUpStoneSkin", 10f);
    }

    public void CleanUpStoneSkin()
    {
        healthController.SetStoneSkin(false);
        if (circeRenderer != null)
        {
            circeRenderer.material = materialBasic;
        }
    }

    private void FixedUpdate()
    {
        if (doThrow)
        {
            doThrow = false;
            // only throw what meant to be thrown
            if (currPotion != null && (currPotion.name.StartsWith(explosivePotionPrefab.name) || currPotion.name.StartsWith(transformationPotionPrefab.name)))
            {
                animator.SetBool("Throw", true);
            }
        }
        else
        {
            animator.SetBool("Throw", false);
        }

        if (doDrink)
        {
            doDrink = false;
            animator.SetBool("Drink", true);
        }
        else
        {
            animator.SetBool("Drink", false);
        }

        if (doSpill)
        {
            doSpill = false;
            animator.SetBool("Spill", true);
        }
        else
        {
            animator.SetBool("Spill", false);
        }
        if (doSpillOnTarget)
        {
            doSpillOnTarget = false;
            StartCoroutine(RotateAndSpill());
            animator.SetBool("SpillOnTarget", true);
        }
        else
        {
            animator.SetBool("SpillOnTarget", false);
        }
        bool isGrounded = CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);
        if (doStandingJump && isGrounded)
        {
            doStandingJump = false;
            isStandingJump = true;
            animator.SetBool("RunningJump", true);
            circe.velocity = new Vector3(circe.velocity.x, 0f, circe.velocity.z);
            circe.AddForce(transform.forward * 2f + Vector3.up * 4.5f, ForceMode.VelocityChange);
        }
        else
        {
            isStandingJump = false;
            animator.SetBool("RunningJump", false);
        }
    }

    public void onJumpComplete()
    {
        isStandingJump = false;
    }

    private IEnumerator RotateAndSpill()
    {
        Debug.Log("Circe is rotating toward the vase...");

        Vector3 directionToVase = (vaseTransform.position - transform.position).normalized;
        directionToVase.y = 0; // Prevent tilting

        Quaternion targetRotation = Quaternion.LookRotation(directionToVase);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void PlayDrinkingSound()
    {
        EventManager.TriggerEvent<PlayerDrinksPotionEvent, GameObject>(this.gameObject);
    }

    public void PlayThrowingSound()
    {
        EventManager.TriggerEvent<PlayerThrowsPotionEvent, GameObject>(this.gameObject);
    }

    public void checkinBrute(Transform brute)
    {
        if (!brutes.Contains(brute))
        {
            brutes.Add(brute);
        }
    }

    public void checkoutBrute(Transform brute)
    {
        if (!brutes.Contains(brute))
        {
            brutes.Remove(brute);
        }
    }

    public void SetVaseTransform(Transform newVase)
    {
        vaseTransform = newVase;
    }

    public void SetDoorController(AbstractDoor newDoor)
    {
        currentDoor = newDoor;
    }
}