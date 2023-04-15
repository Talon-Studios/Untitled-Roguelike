using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 30;
    [SerializeField] private SpriteGraphics graphics;

    Rigidbody2D enemyBody;

    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    private void GetHurt(float damage) {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die() {
        FollowCam.Instance.Hitstop(0.1f);
        FollowCam.Instance.ScreenShake(0.1f, 0.1f);
        
        XPManager.Instance.CreateXP(transform.position);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.TryGetComponent<Bullet>(out Bullet bullet))
        {
            graphics.Flash(0.1f, ColorTheme.Instance.enemyHurt);
            enemyBody.AddForce(trigger.GetComponent<Rigidbody2D>().velocity.normalized * PlayerShooting.Instance.enemyKnockback, ForceMode2D.Impulse);
            GetHurt(PlayerShooting.Instance.damage);
            Destroy(trigger.gameObject);
        }
    }

}
