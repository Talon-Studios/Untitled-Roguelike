using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealth = 3;

    [Tooltip("How long you are invincible after getting hurt")]
    [SerializeField] private float invincibleTime = 3;
    [SerializeField] private float invincibleFlashSpeed = 0.2f;
    [SerializeField] private GameObject graphics;
    [SerializeField] private Transform heartParent;
    [SerializeField] private Transform heartPrefab;

    [HideInInspector] public int health;

    private bool invincible = false;

    void Start() {
        health = maxHealth;

        UpdateHearts();
    }

    private void GetHurt() {
        health--;
        print("OOF! Health: " + health);
        StartCoroutine(Invincible());
        UpdateHearts();
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
    }

    private void UpdateHearts() {
        Transform[] hearts = heartParent.GetComponentsInChildren<Transform>();
        if (health > hearts.Length)
        {
            for (int i = 0; i < health - hearts.Length + 1; i++) Instantiate(heartPrefab, heartParent);
        } else if (health < hearts.Length) {
            for (int i = 0; i < hearts.Length - health; i++) Destroy(heartParent.GetChild(0).gameObject);
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
