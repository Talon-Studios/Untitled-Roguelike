using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 30;
    [SerializeField] private int xpDropAmount = 1;
    [SerializeField] private SpriteGraphics graphics;

    private float hurtScaleSpeed = 8;
    private float hurtScaleMagnitude = 0.5f;

    Rigidbody2D enemyBody;
    FollowEnemy followEnemy;
    Vector2 originalScale;

    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        followEnemy = GetComponent<FollowEnemy>();

        originalScale = graphics.transform.localScale;
    }

    void Update() {
        graphics.transform.localScale = Vector2.MoveTowards(graphics.transform.localScale, originalScale, hurtScaleSpeed * Time.deltaTime);
    }

    public void GetHurt(float damage) {
        graphics.Flash(0.1f, Color.white);
        graphics.transform.localScale = originalScale + (Vector2.one * hurtScaleMagnitude);

        health -= damage; 
        if (health <= 0) Die();
    }

    public void Freeze() {
        followEnemy.speed *= PlayerShooting.Instance.freezingBulletSpeedMultiplier;
    }

    public void Die() {
        FollowCam.Instance.ScreenShake(0.1f, 0.1f);
        ParticleManager.Instance.Play(ParticleManager.Instance.explosion, transform.position);
        AudioManager.Instance.PlayRandomPitch(AudioManager.Instance.explosion, 0.4f, 1.4f);
        
        XPManager.Instance.CreateXP(transform.position, xpDropAmount);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlayRandomPitch(AudioManager.Instance.enemyHurt);
            Vector2 direction = transform.position - PlayerMovement.Instance.transform.position;
            enemyBody.AddForce(direction.normalized * PlayerShooting.Instance.enemyKnockback, ForceMode2D.Impulse);

            if (trigger.TryGetComponent<Bullet>(out Bullet bullet))
            {
                if (bullet.isFreezing)
                {
                    Freeze();
                }

                if (bullet.isPiercing && PlayerShooting.Instance.rangerPiercingDamage)
                {
                    GetHurt(PlayerShooting.Instance.damage * 2);
                } else
                {
                    GetHurt(PlayerShooting.Instance.damage);
                }
            }
        }
    }

}
