using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float distance = 5;
    [SerializeField] private GameObject fireballPrefab;

    private GameObject[] fireballs = new GameObject[0];

    #region Singleton
    
    static public FireWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Update() {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void AddFireball() {
        float fireballNum = fireballs.Length + 1;
        foreach (GameObject fireball in fireballs) Destroy(fireball);

        for (int i = 0; i < fireballNum; i++)
        {
            Vector2 direction = Quaternion.AngleAxis(i * 360 / fireballNum, Vector3.forward) * Vector3.right;
            Instantiate(fireballPrefab, direction.normalized * distance, Quaternion.identity, transform);
        }
    }

}
