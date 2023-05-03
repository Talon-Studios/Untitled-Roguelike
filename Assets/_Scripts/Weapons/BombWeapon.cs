using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{

    [SerializeField] private float spawnDelay = 3;
    [SerializeField] private Bomb bombPrefab;

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
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }

    public void FireFaster(float percentage) {
        spawnDelay -= spawnDelay / 100 * percentage;
    }

}
