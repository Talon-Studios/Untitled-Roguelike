using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem explosion;
    public ParticleSystem bigExplosion;

    #region Singleton
    
    static public ParticleManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void Play(ParticleSystem particles, Vector2 position) {
        Destroy(Instantiate(particles.gameObject, position, Quaternion.identity), particles.main.startLifetime.constantMax);
    } 
}
