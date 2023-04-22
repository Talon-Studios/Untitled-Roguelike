using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{

    [SerializeField] private int xpAmount = 1;
    [SerializeField] private float moveSpeed = 0.5f;

    Transform player;

    void Start() {
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
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed);
            yield return null;
        }

        XPManager.Instance.GainXP(xpAmount);
        Destroy(gameObject);
    }

}
