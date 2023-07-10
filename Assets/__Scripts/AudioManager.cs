using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource randomPitchsource;
    [SerializeField] private AudioSource source;

    [Header("Sounds")]
    public AudioClip xp;
    public AudioClip fire;
    public AudioClip enemyHurt;
    public AudioClip explosion;
    public AudioClip playerHurt;
    public AudioClip hover;
    public AudioClip select;
    public AudioClip playerDie;
    public AudioClip dash;
    public AudioClip locked;
    public AudioClip upgrade;
    public AudioClip activateUpgrade;

    #region Singleton
    
    static public AudioManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void PlayRandomPitch(AudioClip sound, float min = 0.8f, float max = 1.2f) {
        if (sound != null)
        {
            randomPitchsource.pitch = Random.Range(min, max);
            randomPitchsource.PlayOneShot(sound);
        }
    }

    public void Play(AudioClip sound) {
        if (sound != null)
        {
            source.PlayOneShot(sound);
        }
    }

}
