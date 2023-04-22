using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserWeapon : MonoBehaviour
{

    [SerializeField] private float damage = 1;

    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private LayerMask boundsLayer;
    [SerializeField] private LayerMask enemyLayer;

    Camera cam;

    #region Singleton
    
    static public LaserWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        cam = Camera.main;

        laserLine.gameObject.SetActive(false);
    }

    public void ActivateWeapon() {
        laserLine.gameObject.SetActive(true);
    }

    void FixedUpdate() {
        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos - transform.position).normalized;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 100, boundsLayer);

        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, ray.point);

        if (laserLine.gameObject.activeInHierarchy)
        {
            RaycastHit2D[] enemiesHit = Physics2D.RaycastAll(transform.position, direction, ray.distance, enemyLayer);
            foreach (RaycastHit2D hit in enemiesHit)
            {
                if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.GetHurt(damage * Time.fixedDeltaTime);
                }
            }
        }
    }

    public void MoreDamage(float damageIncrease) {
        damage += damageIncrease;
    }

}
