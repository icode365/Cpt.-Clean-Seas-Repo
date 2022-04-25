using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float sfxVolume = 0.1f;
    public AudioSource source;

    public AudioClips[] clips;

    #region----Singleton----

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            
            print("~_instance of AudioManager is NULL!~");
            return null;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public void PlayClip(int clipType)
    {
        foreach (AudioClips clip in clips)
        {
            if (clip.clipType == (AudioClipType)clipType)                
                LeanAudio.play(clip.clip, sfxVolume);
        }
    }
}
