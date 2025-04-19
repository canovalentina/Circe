using UnityEngine;

public class Collectible : MonoBehaviour
{
    // The ingredient type for this collectible instance.
    public IngredientType ingredientType;
    private Inventory inventory;

    // Reference to the ScriptableObject that holds the sprite mapping.
    public IngredientSpriteConfig spriteConfig;

    [Header("Animation Settings")]
    public float rotationSpeed = 90f;          // Degrees per second for rotation.
    public float floatAmplitude = 0.25f;         // Amplitude of the floating effect.
    public float floatFrequency = 1f;            // Frequency of the floating effect.
    public float bounceAmplitude = 0.2f;
    // Frequency of the bounce effect.
    public float bounceFrequency = 1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;

    void Awake()
    {
        // Get or add a SpriteRenderer component.
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
    }

    void OnEnable()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null && c.CompareTag("Player"))
        {
        	playCollectsSinglePlant();
            inventory.AddIngredient(ingredientType, 1);
            PlaytestingLogger.LogAction("Collected ingredient", ingredientType.ToString());
            Destroy(this.gameObject, 0.05f);
        }
    }

    void Start()
    {
        inventory = PotionCraftingManager.Instance.Inventory;
        SetSprite();
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Floating effect remains the same
        Vector3 pos = startPosition;
        pos.y += Mathf.Sin(Time.time * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = pos;
    }

    // Sets the sprite based on the current ingredient type.
    void SetSprite()
    {
        if (spriteConfig != null && spriteRenderer != null)
        {
            Sprite sprite = spriteConfig.GetSprite(ingredientType);
            if (sprite != null)
            {
                spriteRenderer.sprite = sprite;
            }
            else
            {
                Debug.LogWarning("No sprite found for ingredient type: " + ingredientType, this);
            }
        }
    }
    
    private void playCollectsSinglePlant()
    {
        EventManager.TriggerEvent<PlayerCollectsSinglePlantEvent, GameObject>(this.gameObject);
    }
 
#if UNITY_EDITOR
    void OnValidate()
    {
        // Ensure the SpriteRenderer exists.
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
        }
        SetSprite();
    }
#endif
}
