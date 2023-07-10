using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWeapon : MonoBehaviour
{

    public float speed = 50;
    [SerializeField] private float radius = 2;
    [SerializeField] private Bullet bulletPrefab;

    #region Singleton
    
    static public RotatingWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void ActivateWeapon() {
        AddBullet();
    }

    void FixedUpdate() {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    public void AddBullet() {
        int bulletAmount = transform.childCount + 1;
        foreach (Transform child in transform) Destroy(child.gameObject);

        float angle = 0;
        for (int i = 0; i < bulletAmount; i++)
        {
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            Vector3 pos = new Vector3(x, y) + transform.position;

            Instantiate(bulletPrefab, pos, Quaternion.identity, transform);

            angle += 360 / bulletAmount;
        }
    }

    public void FasterRotation(float percentage) {
        speed += speed / 100 * percentage;
    }

}
