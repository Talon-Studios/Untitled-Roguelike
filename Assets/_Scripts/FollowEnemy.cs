using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{

    [Tooltip("How fast the enemy looks at the player. Set to 0.01 to 0.5 for best effect")]
    [SerializeField] private float rotationSmoothing = 0.05f;
    [SerializeField] private float speed = 5;

    Transform player;
    Rigidbody2D enemyBody;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void LateUpdate() {
        Vector2 direction = player.position - transform.position;
        Utils.DirectionToRotation(direction, out Quaternion targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothing);

        enemyBody.AddRelativeForce(Vector2.up * speed);
    }

}
