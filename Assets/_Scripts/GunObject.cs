using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun", fileName = "New Gun")]
public class GunObject : ScriptableObject
{

    public string weaponName;
    [TextArea] public string weaponDescription;

    public Bullet bulletPrefab;

    [Header("Stats")]

    public bool isAutomatic = true;
    public float bulletSpeed = 40;
    public float fireRate = 3;
    public float spread = 10;
    public float damage = 10;
    public float reloadTime = 1;
    public float maxMagazine = 20;
    public float enemyKnockback = 1;
    public int projectiles = 1;

    [Header("Animation")]

    [Tooltip("How much the gun rotates when fired")]
    public float gunKick = 50;

    [Tooltip("How fast the gun rotates back to the original rotation after firing")]
    public float gunRotationSpeed = 1f;

    public float screenShakeMagnitude = 0.1f;
    public float screenShakeDuration = 0.1f;

    public Texture2D graphics;

}
