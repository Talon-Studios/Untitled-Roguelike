using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FollowMode
{
    LookAt,
    Direction
}

public class FollowEnemy : MonoBehaviour
{

    [SerializeField] private FollowMode followMode;

    [Tooltip("How fast the enemy looks at the player. Set to 0.01 to 0.5 for best effect")]
    public float rotationSmoothing = 0.05f;

    [HideInInspector] public float speed;
    [HideInInspector] public Vector2 direction;

    Transform player;
    Rigidbody2D enemyBody;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyBody = GetComponent<Rigidbody2D>();
        speed = EnemyManager.Instance.enemySpeed;
    }

    void LateUpdate() {
        direction = (player.position - transform.position).normalized;

        if (followMode == FollowMode.LookAt)
        {
            Utils.DirectionToRotation(direction, out Quaternion targetRotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
            enemyBody.AddRelativeForce(Vector2.up * speed * Time.timeScale);
        } else
        {
            enemyBody.AddRelativeForce(direction * speed * Time.timeScale);
        }
    }

}
