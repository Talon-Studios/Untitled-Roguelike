using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 30;

    private void GetHurt(float damage) {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die() {
        FollowCam.Instance.ScreenShake(50, Random.insideUnitCircle.normalized);
        FollowCam.Instance.Hitstop(0.1f);
        XPManager.Instance.CreateXP(transform.position);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.TryGetComponent<Bullet>(out Bullet bullet))
        {
            GetHurt(PlayerShooting.Instance.damage);
            Destroy(trigger.gameObject);
        }
    }

}
