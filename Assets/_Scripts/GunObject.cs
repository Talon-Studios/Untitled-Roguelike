using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun", fileName = "New Gun")]
public class GunObject : ScriptableObject
{

    public bool isAutomatic = true;
    public float bulletSpeed = 10;
    public float fireRate = 6;
    public float spread = 10;
    public float damage = 10;
    public float reloadTime = 1;
    public float maxMagazine = 15;
    public int projectiles = 1;
    public Bullet bulletPrefab;

}
