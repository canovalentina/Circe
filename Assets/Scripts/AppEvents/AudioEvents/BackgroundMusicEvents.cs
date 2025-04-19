using UnityEngine;
using UnityEngine.Events;

public class BackgroundMusicEvents
{
    public class PlayMusicEvent : UnityEvent<AudioClip> { }
    public class StopMusicEvent : UnityEvent { }
    public class PauseMusicEvent : UnityEvent { }
    public class ResumeMusicEvent : UnityEvent { }
    public class RestartMusicEvent : UnityEvent { }
    public class SetVolumeEvent : UnityEvent<float> { }
    
}