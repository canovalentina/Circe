using UnityEngine;
using UnityEngine.Events;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance { get; private set; }
    
    public BackgroundMusic backgroundMusicPrefab;
    private static BackgroundMusic bgm;    
    public AudioClip defaultBackgroundMusic;
  
    private UnityAction<AudioClip> playMusicListener;
    private UnityAction stopMusicListener;
    private UnityAction pauseMusicListener;
    private UnityAction resumeMusicListener;
    private UnityAction restartMusicListener;
    private UnityAction<float> setVolumeListener;
    
    private void Start()
    {
        if (bgm && defaultBackgroundMusic != null)
        {
            playMusicHandler(defaultBackgroundMusic);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Destroying duplicate BackgroundMusicManager.");
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    
        createBackgroundMusicSource();

        playMusicListener = new UnityAction<AudioClip>(playMusicHandler);
        stopMusicListener = new UnityAction(stopMusicHandler);
        pauseMusicListener = new UnityAction(pauseMusicHandler);
        resumeMusicListener = new UnityAction(resumeMusicHandler);
        restartMusicListener = new UnityAction(restartMusicHandler);
        setVolumeListener = new UnityAction<float>(setVolumeHandler);
    }
    
    private void createBackgroundMusicSource() 
    {
        if (bgm == null)
        {
            bgm = FindObjectOfType<BackgroundMusic>();
        }
    
        // Note: Difference with AudioEventManager is that it keeps track of one sound variable
        if (bgm == null)
        {
            if (backgroundMusicPrefab != null)
            {
                bgm = Instantiate(backgroundMusicPrefab);
                DontDestroyOnLoad(bgm.gameObject);
            }
            else
            {
                Debug.LogWarning("Background music prefab is missing.");
            }
        }
        else
        {
            Debug.Log("BackgroundMusic object already exists.");
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening<BackgroundMusicEvents.PlayMusicEvent, AudioClip>(playMusicListener);
        EventManager.StartListening<BackgroundMusicEvents.StopMusicEvent>(stopMusicListener);
        EventManager.StartListening<BackgroundMusicEvents.PauseMusicEvent>(pauseMusicListener);
        EventManager.StartListening<BackgroundMusicEvents.ResumeMusicEvent>(resumeMusicListener);
        EventManager.StartListening<BackgroundMusicEvents.RestartMusicEvent>(restartMusicListener);
        EventManager.StartListening<BackgroundMusicEvents.SetVolumeEvent, float>(setVolumeListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening<BackgroundMusicEvents.PlayMusicEvent, AudioClip>(playMusicListener);
        EventManager.StopListening<BackgroundMusicEvents.StopMusicEvent>(stopMusicListener);
        EventManager.StopListening<BackgroundMusicEvents.PauseMusicEvent>(pauseMusicListener);
        EventManager.StopListening<BackgroundMusicEvents.ResumeMusicEvent>(resumeMusicListener);
        EventManager.StopListening<BackgroundMusicEvents.RestartMusicEvent>(restartMusicListener);
        EventManager.StopListening<BackgroundMusicEvents.SetVolumeEvent, float>(setVolumeListener);
    }

    void playMusicHandler(AudioClip clip)
    {           
        if (bgm && bgm.audioSrc.clip != clip)
        { 
            //print("Background music volume: " + bgm.audioSrc.volume);
            bgm.audioSrc.clip = clip;
            bgm.audioSrc.Play();
        }
    }

    void stopMusicHandler()
    {
        if (bgm)
        {
            bgm.audioSrc.Stop();
        }
    }

    void pauseMusicHandler()
    {
        if (bgm)
        {
            Debug.Log("Pausing background music.");
            bgm.audioSrc.Pause();
        }
        else
        {
            Debug.LogWarning("Bgm is null when trying to pause! It may have been destroyed.");
        }
    }

    void resumeMusicHandler()
    {
        if (bgm && !bgm.audioSrc.isPlaying)
        {
            bgm.audioSrc.UnPause();
        }
    }
    
    void restartMusicHandler()
    {
        if (bgm && defaultBackgroundMusic != null)
        {
            bgm.audioSrc.Stop();
            bgm.audioSrc.clip = defaultBackgroundMusic;
            bgm.audioSrc.time = 0f;
            bgm.audioSrc.Play();
        }
        else
        {
            Debug.LogWarning("Can't restart music: BGM or default clip is missing.");
        }
    }

    void setVolumeHandler(float volume)
    {
        if (bgm)
        {
            bgm.audioSrc.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }  
    
    public float GetCurrentVolume() 
    {
      if (bgm) 
      {
        return bgm.audioSrc.volume;
      }
      
      return 0f;
    }
    
    public void RestartMusic()
    {
        restartMusicHandler();
    }
    
    public void PlayMusic()
    {
        playMusicHandler(defaultBackgroundMusic);
    }
    
    public void StopMusic()
    {
        stopMusicHandler();
    }
 
 }
