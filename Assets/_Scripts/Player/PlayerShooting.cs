using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    public bool canShoot = true;
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject reloadBar;
    [SerializeField] private Transform reloadMarker;

    private bool shootInput;
    private float nextTimeToFire;

    [Header("Stats")]
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
    public float spread = 5;
    public float damage = 10;
    public float reloadTime = 2;
    public float maxMagazine = 6;
    public int projectiles = 1;

    private float magazine;
    private bool reloading = false;

    Camera cam;

    #region Singleton
    
    static public PlayerShooting Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        cam = Camera.main;

        magazine = maxMagazine;
    }

    void Update() {
        RotateGun();

        if (shootInput && Time.time >= nextTimeToFire && canShoot && !reloading)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Fire();
        }

        if (reloading)
        {
            reloadMarker.localPosition += (1.5f / 2) * reloadTime * Time.deltaTime * Vector3.right;
        }
    }

    private void RotateGun() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mousePosition - pivot.position;
        Utils.DirectionToRotation(direction, out Quaternion rotation);
        pivot.rotation = rotation;

        if (pivot.eulerAngles.z > 180)
        {
            gun.localEulerAngles = new Vector3(0, 0, gun.localEulerAngles.z);
        } else
        {
            gun.localEulerAngles = new Vector3(0, 180, gun.localEulerAngles.z);
        }
    }

    private void Fire() {
        magazine--;
        if (magazine <= 0)
        {
            StartCoroutine(Reload());
        }

        float step = spread / (float)projectiles;
        float halfSpread = spread / 2;

        for (int i = 0; i < projectiles; i++)
        {
            Quaternion rotation = pivot.rotation * (Quaternion.Euler(0, 0, (i * step) - (halfSpread - (halfSpread / projectiles))));
            Rigidbody2D bulletBody = Instantiate(bulletPrefab, firePoint.position, rotation);
            bulletBody.AddRelativeForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    private IEnumerator Reload() {
        reloading = true;
        reloadBar.SetActive(true);
        reloadMarker.localPosition = Vector2.right * -1.5f;

        yield return new WaitForSeconds(reloadTime);

        magazine = maxMagazine;
        reloading = false;
        reloadBar.SetActive(false);
    }

    void OnShoot(InputValue value) {
        shootInput = value.Get<float>() > 0 ? true : false;
    }

    public float GetDPS() {
        return fireRate * damage;
    }

}
