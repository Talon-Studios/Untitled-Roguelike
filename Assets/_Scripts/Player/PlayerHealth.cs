using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealth = 3;

    [Tooltip("How long you are invincible after getting hurt")]
    [SerializeField] private float invincibleTime = 3;
    [SerializeField] private float invincibleFlashSpeed = 0.2f;
    [SerializeField] private SpriteRenderer graphics;
    [SerializeField] private Transform heartParent;
    [SerializeField] private Transform heartPrefab;

    [Header("UI")]
    [SerializeField] private Texture2D fullHeart;
    [SerializeField] private Texture2D emptyHeart;

    [Header("States")]
    [SerializeField] private Sprite normalPlayer;
    [SerializeField] private Sprite hurtPlayer;

    [HideInInspector] public int health;

    public bool invincible = false;

    RawImage[] hearts;

    #region Singleton
    
    static public PlayerHealth Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        health = maxHealth;

        for (int i = 0; i < health; i++) AddHeartContainer();
    }

    private void GetHurt() {
        health--;

        if (PlayerShooting.Instance.isFurious) PlayerShooting.Instance.StartFury();
        
        FollowCam.Instance.ScreenShake(0.2f, 0.5f);
        FollowCam.Instance.Hitstop(0.2f);
        AudioManager.Instance.Play(AudioManager.Instance.playerHurt);
        
        StartCoroutine(Invincible());
        RemoveHeart();

        if (health <= 0) Die();
    }

    private IEnumerator Invincible() {
        invincible = true;

        StartCoroutine(Flash());
        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
        graphics.sprite = normalPlayer;
        StopAllCoroutines();
    }

    private IEnumerator Flash() {
        while (true)
        {
            graphics.sprite = hurtPlayer;
            yield return new WaitForSeconds(invincibleFlashSpeed);
            graphics.sprite = normalPlayer;
            yield return new WaitForSeconds(invincibleFlashSpeed);
        }
    }

    private void Die() {
        print("YOU DIEEEEED!!!!!");
        SceneManager.LoadScene("Die");
    }

    private void RemoveHeart() {
        hearts[health].texture = emptyHeart;
    }

    private void AddHeart() {
        hearts[health - 1].texture = fullHeart;
    }

    private void AddHeartContainer() {
        Instantiate(heartPrefab, heartParent);
        hearts = heartParent.GetComponentsInChildren<RawImage>();
    }

    public void Heal() {
        if (health < maxHealth)
        {
            health++;
            AddHeart();
        } else
        {
            maxHealth++;
            health++;
            AddHeartContainer();
            AddHeart();
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Enemy") && !invincible)
        {
            trigger.GetComponent<Enemy>().Die();
            GetHurt();
        }
    }

}
