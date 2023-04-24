using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool isPiercing = false;

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet Bounds"))
        {
            Destroy(gameObject);
        } else if (!isPiercing && trigger.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
