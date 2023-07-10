using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseEnemy : MonoBehaviour
{

    [SerializeField] private Transform graphics;
    [SerializeField] private ParticleSystem dustParticles;

    [SerializeField] private float surpriseHealthThreshold = 20;
    [SerializeField] private float spinSpeed = 5;
    [SerializeField] private float startForce = 5;

    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private float rotationSpeedMultiplier = 2;
    [SerializeField] private float spinSpeedMultiplier = 3;

    private bool surprised = false;

    FollowEnemy followEnemy;
    Enemy enemy;
    Rigidbody2D enemyBody;

    void Start() {
        followEnemy = GetComponent<FollowEnemy>();
        enemy = GetComponent<Enemy>();
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        graphics.Rotate(0, 0, spinSpeed * spinSpeedMultiplier * Time.deltaTime);

        if (enemy.health - surpriseHealthThreshold <= 0 && !surprised)
        {
            surprised = true;
            BeSurprised();
        }
    }

    private void BeSurprised() {
        dustParticles.Play();
        enemyBody.AddForce(followEnemy.direction * startForce, ForceMode2D.Impulse);

        followEnemy.speed *= speedMultiplier;
        followEnemy.rotationSmoothing *= rotationSpeedMultiplier;
        spinSpeed *= spinSpeedMultiplier;
    }

}
