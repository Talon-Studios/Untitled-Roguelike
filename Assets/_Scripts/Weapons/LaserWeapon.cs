using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{

    [SerializeField] private float distance = 100;
    [SerializeField] private float angleChange = 1;
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private LayerMask bulletBoundsLayer;

    private float angle = 0;

    #region Singleton
    
    static public LaserWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
    }

    void Update() {
        Vector2 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, distance, bulletBoundsLayer);

        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, ray.point);

        angle += angleChange * Time.deltaTime;
    }

}
