using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool isPiercing = false;
    public bool isFreezing = false;
    public float speed = 40;

    [SerializeField] private bool bulletBounds = true;

    Rigidbody2D bulletBody;
    
    void Start() {
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet Bounds") && bulletBounds)
        {
            Destroy(gameObject);
        } else if (!isPiercing && trigger.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
