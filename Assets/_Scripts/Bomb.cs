using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private float radius = 5;
    [SerializeField] private float delay = 2;
    [SerializeField] private float enemyKnockback = 10;
    [SerializeField] private LayerMask enemyLayer;

    void Start() {
        StartCoroutine(ExplodeRoutine());
    }

    private IEnumerator ExplodeRoutine() {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        ParticleManager.Instance.Play(ParticleManager.Instance.bigExplosion, transform.position);

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach (Collider2D enemy in enemiesHit)
        {
            if (enemy.CompareTag("Enemy"))
            {                
                Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>();
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                enemyBody.AddForce(direction * enemyKnockback, ForceMode2D.Impulse);

                enemyScript.GetHurt(PlayerShooting.Instance.damage);
            }
        }
    }

}
