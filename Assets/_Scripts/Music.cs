using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    [SerializeField] private AudioClip selectMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip deathMusic;

    private AudioClip currentClip;

    AudioSource source;

    #region Singleton
    
    static public Music Instance = null;
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    
    #endregion

    void Start() {
        source = GetComponent<AudioSource>();
        ChangeMusic(SceneManager.GetActiveScene().name);
    }

    public void ChangeMusic(string sceneName) {
        AudioClip clip = null;
        switch (sceneName)
        {
            case "Game": clip = gameMusic; break;
            case "Start": clip = selectMusic; break;
            case "Select": clip = selectMusic; break;
            case "Die": clip = deathMusic; break;
        }

        if (clip != null && clip != currentClip)
        {
            currentClip = clip;
            source.Stop();
            source.clip = clip;
            source.Play();
        }
    }

    public void StopMusic() {
        source.Stop();
    }

}
