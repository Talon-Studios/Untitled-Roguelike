using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    public bool canShoot = true;
    public GunObject gun;
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform gunFlip;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject reloadBar;
    [SerializeField] private Transform reloadMarker;
    [SerializeField] private StartObject startObject;

    [HideInInspector] public bool isAutomatic;
    [HideInInspector] public float bulletSpeed, fireRate, spread, damage, reloadTime, enemyKnockback;
    [HideInInspector] public int projectiles;
    [HideInInspector] public float piercingBulletChance = 0;

    private bool shootInput;
    private float nextTimeToFire;

    private float magazine;
    private bool reloading = false;

    Camera cam;
    Vector3 mousePosition;
    float reloadMarkerTime;
    float gunTargetRotation;

    #region Singleton
    
    static public PlayerShooting Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        cam = Camera.main;
        gun = startObject.gun;

        SetStats();
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
            reloadMarkerTime += Time.deltaTime / reloadTime;
            reloadMarker.localPosition = Vector2.Lerp(-1.5f * Vector2.right, 1.5f * Vector2.right, reloadMarkerTime);
        }

        gunTransform.localRotation = Quaternion.RotateTowards(gunTransform.localRotation, Quaternion.Euler(0, gunTransform.localRotation.y, 0), gun.gunRotationSpeed);
    }

    private void RotateGun() {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mousePosition - pivot.position;
        Utils.DirectionToRotation(direction, out Quaternion rotation);
        pivot.rotation = rotation;

        if (pivot.eulerAngles.z > 180)
        {
            gunFlip.localEulerAngles = new Vector3(0, 0, gunFlip.localEulerAngles.z);
        } else
        {
            gunFlip.localEulerAngles = new Vector3(0, 180, gunFlip.localEulerAngles.z);
        }
    }

    private void Fire() {
        FollowCam.Instance.ScreenShake(gun.screenShakeDuration, gun.screenShakeMagnitude);

        gunTransform.localRotation *= Quaternion.Euler(0, 0, gun.gunKick);

        magazine--;
        if (magazine <= 0) StartCoroutine(Reload());
        if (!isAutomatic) shootInput = false;

        float step = spread / (float)projectiles;
        float halfSpread = spread / 2;

        for (int i = 0; i < projectiles; i++)
        {   
            Quaternion rotation;
            if (projectiles > 1)
            {
                rotation = pivot.rotation * (Quaternion.Euler(0, 0, (i * step) - (halfSpread - (halfSpread / projectiles))));
            } else
            {
                rotation = pivot.rotation * Quaternion.Euler(0, 0, Random.Range(-spread, spread));
            }

            Rigidbody2D bulletBody = Instantiate(gun.bulletPrefab, firePoint.position, rotation).GetComponent<Rigidbody2D>();
            bulletBody.AddRelativeForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    public void PiercingBulletChanceIncrease(float percentage) {
        piercingBulletChance += percentage;
    }

    private IEnumerator Reload() {
        reloading = true;
        reloadBar.SetActive(true);
        reloadMarkerTime = 0;
        reloadMarker.localPosition = Vector2.right * -1.5f;

        yield return new WaitForSeconds(reloadTime);

        magazine = gun.maxMagazine;
        reloading = false;
        reloadBar.SetActive(false);
    }

    private void SetStats() {
        isAutomatic = gun.isAutomatic;
        bulletSpeed = gun.bulletSpeed;
        fireRate = gun.fireRate;
        spread = gun.spread;
        damage = gun.damage;
        reloadTime = gun.reloadTime;
        magazine = gun.maxMagazine;
        enemyKnockback = gun.enemyKnockback;
        projectiles = gun.projectiles;
    }

    void OnShoot(InputValue value) {
        shootInput = value.Get<float>() > 0 ? true : false;
    }

}
