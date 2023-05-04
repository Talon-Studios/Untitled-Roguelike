using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : MonoBehaviour
{

    [SerializeField] private float force = 100;
    [SerializeField] private float invincibleTime = 0.5f;
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private ParticleSystem dashParticles;

    Rigidbody2D playerBody;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;

    #region Singleton
    
    static public DashAbility Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private IEnumerator Dash() {
        FollowCam.Instance.Hitstop(0.1f);
        
        playerHealth.invincible = true;
        playerBody.AddForce(playerBody.velocity.normalized * force, ForceMode2D.Impulse);
        
        dustParticles.Stop();
        dashParticles.Play();

        yield return new WaitForSeconds(invincibleTime);

        playerHealth.invincible = false;

        dustParticles.Play();
        dashParticles.Stop();
    }

    public void Activate() {
        if (Time.timeScale > 0)
        {
            StartCoroutine(Dash());
        }
    }

    public void DashFarther(float forceIncrease, float invincibleTimeIncrease) {
        force += forceIncrease;
        invincibleTime += invincibleTimeIncrease;
    }

}
