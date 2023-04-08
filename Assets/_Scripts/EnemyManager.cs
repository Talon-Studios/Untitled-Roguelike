using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [Tooltip("The number of enemies to spawn per second")]
    [SerializeField] private float startingSpawnRate = 1;

    [Tooltip("How much the spawn rate increases per upgrade")]
    [SerializeField] private float spawnRateIncrement;
    
    [SerializeField] private Enemy[] enemyPrefabs;

    private float nextTimeToSpawn;
    private float spawnRate;

    BoxCollider2D box;
    
    #region Singleton
    
    static public EnemyManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        box = GetComponent<BoxCollider2D>();

        spawnRate = startingSpawnRate;
    }

    void Update() {
        if (Time.time >= nextTimeToSpawn)
        {
            SpawnRandomEnemy();
            nextTimeToSpawn = Time.time + 1 / spawnRate;
        }
    }

    private void SpawnRandomEnemy() {
        GetRandomPosition(out float x, out float y);
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector2(x, y), Quaternion.identity);
    }

    public void FasterSpawnRate() {
        spawnRate += spawnRateIncrement;
    }

    private void GetRandomPosition(out float x, out float y) {
        if (Random.value > 0.5f)
        {
            y = Random.Range(box.bounds.min.y, box.bounds.max.y);

            if (Random.value > 0.5f) x = box.bounds.min.x;    
            else x = box.bounds.max.x;
        } else
        {
            x = Random.Range(box.bounds.min.x, box.bounds.max.x);

            if (Random.value > 0.5f) y = box.bounds.min.y;    
            else y = box.bounds.max.y;
        }
    }

}
