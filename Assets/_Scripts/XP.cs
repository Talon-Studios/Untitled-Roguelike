using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{

    [SerializeField] private int xpAmount = 1;
    [SerializeField] private float moveSpeed = 0.5f;

    Transform player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            StartCoroutine(MoveToPlayer());
        }
    }

    private IEnumerator MoveToPlayer() {
        while (transform.position != player.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        if (!PlayerHealth.Instance.isDead)
        {
            XPManager.Instance.GainXP(xpAmount);
            AudioManager.Instance.PlayRandomPitch(AudioManager.Instance.xp);
            Destroy(gameObject);
        }
    }

}
