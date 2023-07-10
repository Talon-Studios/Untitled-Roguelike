using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StompAbility : MonoBehaviour
{

    [SerializeField] private float damage = 3;
    [SerializeField] private float range = 5;
    [SerializeField] private float enemyKnockback = 5;
    [SerializeField] private LayerMask enemyLayer;

    PlayerMovement playerMovement;

    #region Singleton
    
    static public StompAbility Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Activate() {
        if (Time.timeScale > 0)
        {
            Stomp();
        }
    }

    private void Stomp() {
        FollowCam.Instance.ScreenShake(0.2f, 0.5f);
        FollowCam.Instance.Hitstop(0.1f);

        Collider2D[] allEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemyCollider in allEnemies)
        {
            if (enemyCollider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.GetHurt(damage);
                
                Vector2 direction = enemy.transform.position - transform.position;
                Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>();
                enemyBody.AddForce(direction.normalized * enemyKnockback, ForceMode2D.Impulse);
            }
        }
    }

    public void BiggerRange() {
        range += 2;
    }

    public void MorePower() {
        damage += 5;
        enemyKnockback += 15;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
