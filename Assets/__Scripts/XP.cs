using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{

    [SerializeField] private int xpAmount = 1;
    [SerializeField] private float moveSpeed = 0.5f;

    Transform player;

    private bool moveToPlayer = false;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (!moveToPlayer) return;

        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if (!PlayerHealth.Instance.isDead && transform.position == player.position)
        {
            XPManager.Instance.GainXP(xpAmount);
            AudioManager.Instance.PlayRandomPitch(AudioManager.Instance.xp);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            moveToPlayer = true;
        }
    }

}
