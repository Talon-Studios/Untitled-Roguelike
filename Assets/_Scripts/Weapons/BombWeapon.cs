using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{

    [SerializeField] private float bombSpin = 10;
    [SerializeField] private float bombStartForceMin = 8;
    [SerializeField] private float bombStartForceMax = 12;
    [SerializeField] private float spawnDelay = 3;
    [SerializeField] private Rigidbody2D bombPrefab;

    #region Singleton
    
    static public BombWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion 

    public void ActivateWeapon() {
        StartCoroutine(SpawnBombRoutine());
    }

    public IEnumerator SpawnBombRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnBomb();
        }
    }

    public void SpawnBomb() {
        Rigidbody2D bombBody = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        float randomStartForce = Random.Range(bombStartForceMin, bombStartForceMax);
        bombBody.AddForce(Random.insideUnitCircle.normalized * randomStartForce, ForceMode2D.Impulse);
        bombBody.AddTorque(bombSpin, ForceMode2D.Impulse);
    }

    public void FireFaster(float percentage) {
        spawnDelay -= spawnDelay / 100 * percentage;
    }

}
