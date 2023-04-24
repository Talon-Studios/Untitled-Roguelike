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
    [SerializeField] private GameObject graphics;
    [SerializeField] private Transform heartParent;
    [SerializeField] private Transform heartPrefab;

    [SerializeField] private Texture2D fullHeart;
    [SerializeField] private Texture2D emptyHeart;

    [HideInInspector] public int health;

    private bool invincible = false;

    RawImage[] hearts;

    void Start() {
        health = maxHealth;

        for (int i = 0; i < health; i++) AddHeartContainer();
    }

    private void GetHurt() {
        health--;

        print("OOF! Health: " + health);
        StartCoroutine(Invincible());
        RemoveHeart();

        if (health <= 0) Die();
    }

    private IEnumerator Invincible() {
        invincible = true;
        StartCoroutine(Flash());

        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
        graphics.SetActive(true);
        StopAllCoroutines();
    }

    private IEnumerator Flash() {
        while (true)
        {
            graphics.SetActive(false);
            yield return new WaitForSeconds(invincibleFlashSpeed);
            graphics.SetActive(true);
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

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Enemy") && !invincible)
        {
            trigger.GetComponent<Enemy>().Die();
            GetHurt();
        }
    }

}
