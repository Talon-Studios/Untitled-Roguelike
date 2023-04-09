using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Transform heartParent;
    [SerializeField] private Transform heartPrefab;

    [HideInInspector] public int health;

    void Start() {
        health = maxHealth;

        UpdateHearts();
    }

    private void GetHurt() {
        health--;
        print("OOF! Health: " + health);
        UpdateHearts();
        if (health <= 0) Die();
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
        if (trigger.CompareTag("Enemy"))
        {
            trigger.GetComponent<Enemy>().Die();
            GetHurt();
        }
    }

}
