using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet Bounds") || trigger.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
