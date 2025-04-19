using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;
    
    //Audio Clips
    public AudioClip playerLandsAudio;
    //public AudioClip gruntAudio;
    public AudioClip drinkingPotionAudio;
    public AudioClip throwingPotionAudio;
    public AudioClip collectingPlantAudio;
    public AudioClip collectingSinglePlantAudio;
    public AudioClip[] potteryBreakSounds;
    public AudioClip explosionAudio;
    public AudioClip swordAudio;
    public AudioClip painFemaleAudio;
    public AudioClip painMaleAudio;
    public AudioClip deathFemaleAudio;
    public AudioClip deathMaleAudio;
    public AudioClip pressInventoryKeyAudio;
    public AudioClip pressPauseKeyAudio;
    public AudioClip gameOverAudio;
    public AudioClip gameWonAudio;
    public AudioClip bearGrowlAudio;
    public AudioClip pigOinkAudio;
    public AudioClip wolfHowlAudio;
    public AudioClip oldManScreamAudio;
    
    
    // Event Listeners
    private UnityAction<Vector3, float> playerLandsEventListener;
    private UnityAction<GameObject> playerDrinksPotionEventListener;
    private UnityAction<GameObject> playerThrowsPotionEventListener;
    private UnityAction<GameObject> playerCollectsPlantEventListener;
    private UnityAction<GameObject> playerCollectsSinglePlantEventListener;
    private UnityAction<GameObject> playerBreaksPotteryEventListener;
    private UnityAction<Vector3> explosionEventListener;
    private UnityAction<GameObject> swordEventListener;
    // private UnityAction<Vector3> painFemaleEventListener;
    // private UnityAction<Vector3> painMaleEventListener;
    // private UnityAction<Vector3> deathFemaleEventListener;
    private UnityAction<Vector3> deathMaleEventListener;
    private UnityAction pressInventoryKeyEventListener;
    private UnityAction pressPauseKeyEventListener;
    private UnityAction gameOverEventListener;
    private UnityAction gameWonEventListener;
    private UnityAction<GameObject> bearGrowlEventListener;
    private UnityAction<GameObject> pigOinkEventListener;
    private UnityAction<GameObject> wolfHowlEventListener;
    private UnityAction<GameObject> oldManScreamEventListener;
    
    
    void Awake()
    {   
        playerLandsEventListener = new UnityAction<Vector3, float>(playerLandsEventHandler);
        playerDrinksPotionEventListener = new UnityAction<GameObject>(playerDrinksPotionEventHandler);
        playerThrowsPotionEventListener = new UnityAction<GameObject>(playerThrowsPotionEventHandler);
        playerCollectsPlantEventListener = new UnityAction<GameObject>(playerCollectsPlantEventHandler); 
        playerCollectsSinglePlantEventListener = new UnityAction<GameObject>(playerCollectsSinglePlantEventHandler);
        playerBreaksPotteryEventListener = new UnityAction<GameObject>(playerBreaksPotteryEventHandler);
        explosionEventListener = new UnityAction<Vector3>(explosionEventHandler);
        swordEventListener = new UnityAction<GameObject>(swordEventHandler);
        // painFemaleEventListener = new UnityAction<Vector3>(painFemaleEventHandler);
        // painMaleEventListener = new UnityAction<Vector3>(painMaleEventHandler);
        // deathFemaleEventListener = new UnityAction<Vector3>(deathFemaleEventHandler);
        deathMaleEventListener = new UnityAction<Vector3>(deathMaleEventHandler);
        pressInventoryKeyEventListener = new UnityAction(pressInventoryKeyEventHandler);
        pressPauseKeyEventListener = new UnityAction(pressPauseKeyEventHandler);
        gameOverEventListener = new UnityAction(gameOverEventHandler);
        gameWonEventListener = new UnityAction(gameWonEventHandler);
        bearGrowlEventListener = new UnityAction<GameObject>(bearGrowlEventHandler);
        pigOinkEventListener = new UnityAction<GameObject>(pigOinkEventHandler);
        wolfHowlEventListener = new UnityAction<GameObject>(wolfHowlEventHandler);
        oldManScreamEventListener = new UnityAction<GameObject>(oldManScreamEventHandler);
    }


    // Use this for initialization
    void Start()
    {


        			
    }


    void OnEnable()
    {
        EventManager.StartListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StartListening<PlayerDrinksPotionEvent, GameObject>(playerDrinksPotionEventListener);
        EventManager.StartListening<PlayerThrowsPotionEvent, GameObject>(playerThrowsPotionEventListener);
        EventManager.StartListening<PlayerCollectsPlantEvent, GameObject>(playerCollectsPlantEventListener);
        EventManager.StartListening<PlayerCollectsSinglePlantEvent, GameObject>(playerCollectsSinglePlantEventListener);
        EventManager.StartListening<PlayerBreaksPotteryEvent, GameObject>(playerBreaksPotteryEventListener);
        EventManager.StartListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StartListening<SwordEvent, GameObject>(swordEventListener);
        // EventManager.StartListening<PainFemaleEvent, Vector3>(painFemaleEventListener);
        // EventManager.StartListening<PainMaleEvent, Vector3>(painMaleEventListener);
        // EventManager.StartListening<DeathFemaleEvent, Vector3>(deathFemaleEventListener);
        EventManager.StartListening<DeathMaleEvent, Vector3>(deathMaleEventListener);
        EventManager.StartListening<PressInventoryKeyEvent>(pressInventoryKeyEventListener);
        EventManager.StartListening<PressPauseKeyEvent>(pressPauseKeyEventListener);
        EventManager.StartListening<GameOverEvent>(gameOverEventListener);
        EventManager.StartListening<GameWonEvent>(gameWonEventListener);
        EventManager.StartListening<BearGrowlEvent, GameObject>(bearGrowlEventListener);
        EventManager.StartListening<PigOinkEvent, GameObject>(pigOinkEventListener);
        EventManager.StartListening<WolfHowlEvent, GameObject>(wolfHowlEventListener);
        EventManager.StartListening<OldManScreamEvent, GameObject>(oldManScreamEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StopListening<PlayerDrinksPotionEvent, GameObject>(playerDrinksPotionEventListener);
        EventManager.StopListening<PlayerThrowsPotionEvent, GameObject>(playerThrowsPotionEventListener);
        EventManager.StopListening<PlayerCollectsPlantEvent, GameObject>(playerCollectsPlantEventListener);
        EventManager.StopListening<PlayerCollectsSinglePlantEvent, GameObject>(playerCollectsSinglePlantEventListener);
        EventManager.StopListening<PlayerBreaksPotteryEvent, GameObject>(playerBreaksPotteryEventListener);
        EventManager.StopListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StopListening<SwordEvent, GameObject>(swordEventListener);
        // EventManager.StopListening<PainFemaleEvent, Vector3>(painFemaleEventListener);
        // EventManager.StopListening<PainMaleEvent, Vector3>(painMaleEventListener);
        // EventManager.StopListening<DeathFemaleEvent, Vector3>(deathFemaleEventListener);
        EventManager.StopListening<DeathMaleEvent, Vector3>(deathMaleEventListener);
        EventManager.StopListening<PressInventoryKeyEvent>(pressInventoryKeyEventListener);
        EventManager.StopListening<PressPauseKeyEvent>(pressPauseKeyEventListener);
        EventManager.StopListening<GameOverEvent>(gameOverEventListener);
        EventManager.StopListening<GameOverEvent>(gameWonEventListener);
        EventManager.StopListening<BearGrowlEvent, GameObject>(bearGrowlEventListener);
        EventManager.StopListening<PigOinkEvent, GameObject>(pigOinkEventListener);
        EventManager.StopListening<WolfHowlEvent, GameObject>(wolfHowlEventListener);
        EventManager.StopListening<OldManScreamEvent, GameObject>(oldManScreamEventListener);
    }
    
    void playerLandsEventHandler(Vector3 worldPos, float collisionMagnitude)
    {

        if (eventSound3DPrefab)
        {
            if (collisionMagnitude > 300f)
            {

                EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

                snd.audioSrc.clip = this.playerLandsAudio;

                snd.audioSrc.minDistance = 5f;
                snd.audioSrc.maxDistance = 100f;

                snd.audioSrc.Play();

                // if (collisionMagnitude > 500f)
                // {

                //     EventSound3D snd2 = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

                //     snd2.audioSrc.clip = this.gruntAudio;

                //     snd2.audioSrc.minDistance = 5f;
                //     snd2.audioSrc.maxDistance = 100f;

                //     snd2.audioSrc.Play();
                // }
            }


        }
    }
    
    void playerDrinksPotionEventHandler(GameObject go)
    {
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.drinkingPotionAudio;

            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }
    
    void playerThrowsPotionEventHandler(GameObject go)
    {
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.throwingPotionAudio;

            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }
    
    void playerCollectsPlantEventHandler(GameObject go)
    {
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.collectingPlantAudio;

            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }
    
    void playerCollectsSinglePlantEventHandler(GameObject go)
    {
    
        //Debug.Log("Event triggered. Creating sound prefab at " + go.transform.position);
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.collectingSinglePlantAudio;

            snd.audioSrc.minDistance = 1f;
            snd.audioSrc.maxDistance = 20f;

            snd.audioSrc.Play();
        }
    }
    
    void playerBreaksPotteryEventHandler(GameObject go)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = potteryBreakSounds[Random.Range(0, potteryBreakSounds.Length)];

            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }
    
    void explosionEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {
            
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.explosionAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;
            snd.audioSrc.volume = 0.02f;

            snd.audioSrc.Play();
        }
    }

    void swordEventHandler(GameObject go)
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);
            snd.audioSrc.clip = this.swordAudio;
            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 50f;
            snd.audioSrc.Play();
        }
    }

    // void painFemaleEventHandler(Vector3 worldPos)
    // {
    //     if (eventSound3DPrefab)
    //     {
    //         EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
    //         snd.audioSrc.clip = this.painFemaleAudio;
    //         snd.audioSrc.minDistance = 2f;
    //         snd.audioSrc.maxDistance = 75f;
    //         snd.audioSrc.Play();
    //     }
    // }

    // void painMaleEventHandler(Vector3 worldPos)
    // {
    //     if (eventSound3DPrefab)
    //     {
    //         EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
    //         snd.audioSrc.clip = this.painMaleAudio;
    //         snd.audioSrc.minDistance = 2f;
    //         snd.audioSrc.maxDistance = 75f;
    //         snd.audioSrc.Play();
    //     }
    // }

    // void deathFemaleEventHandler(Vector3 worldPos)
    // {
    //     if (eventSound3DPrefab)
    //     {
    //         EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
    //         snd.audioSrc.clip = this.deathFemaleAudio;
    //         snd.audioSrc.minDistance = 3f;
    //         snd.audioSrc.maxDistance = 100f;
    //         snd.audioSrc.Play();
    //     }
    // }

    void deathMaleEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
            snd.audioSrc.clip = this.deathMaleAudio;
            snd.audioSrc.minDistance = 3f;
            snd.audioSrc.maxDistance = 100f;
            snd.audioSrc.volume = 0.25f;
            snd.audioSrc.Play();
        }
    }

    void pressInventoryKeyEventHandler()
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab);
            snd.audioSrc.clip = this.pressInventoryKeyAudio;
            snd.audioSrc.minDistance = 1f;
            snd.audioSrc.maxDistance = 25f;
            snd.audioSrc.Play();
        }
    }

    void pressPauseKeyEventHandler()
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab);
            snd.audioSrc.clip = this.pressPauseKeyAudio;
            snd.audioSrc.minDistance = 1f;
            snd.audioSrc.maxDistance = 25f;
            snd.audioSrc.Play();
        }
    }
    
    void gameOverEventHandler()
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab);
            snd.audioSrc.clip = this.gameOverAudio;
            snd.audioSrc.volume = 0.8f;
            snd.audioSrc.spatialBlend = 0f;
            snd.audioSrc.Play();
        }
    }
    
    void gameWonEventHandler()
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab);
            snd.audioSrc.clip = this.gameWonAudio;
            snd.audioSrc.volume = 0.8f;
            snd.audioSrc.spatialBlend = 0f;
            snd.audioSrc.Play();
        }
    }
    
    void bearGrowlEventHandler(GameObject go)
    {
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.bearGrowlAudio;

            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }
    
    void pigOinkEventHandler(GameObject go)
    {
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.pigOinkAudio;

            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }
    
    void wolfHowlEventHandler(GameObject go)
    {
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.wolfHowlAudio;

            snd.audioSrc.minDistance = 2f;
            snd.audioSrc.maxDistance = 100f;
            snd.audioSrc.volume = 1f;

            snd.audioSrc.Play();
        }
    }
    
    void oldManScreamEventHandler(GameObject go)
    {
    
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);
            snd.audioSrc.volume = 1f;
            snd.audioSrc.priority = 128;
            snd.audioSrc.spatialBlend = 1f;
            snd.audioSrc.dopplerLevel = 1.04f;
            snd.audioSrc.rolloffMode = AudioRolloffMode.Logarithmic;
            snd.audioSrc.minDistance = 1f;
            snd.audioSrc.maxDistance = 500f;
            
            snd.audioSrc.PlayOneShot(this.oldManScreamAudio);
        }
    }

}
