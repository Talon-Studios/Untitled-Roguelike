using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{

    [Tooltip("The number of enemies to spawn per second")]
    [SerializeField] private float startingSpawnRate = 1;

    [Tooltip("How much the spawn rate increases per upgrade")]
    [SerializeField] private float spawnRateIncrement;

    public float enemySpeed = 5;
    public float enemyHealthAddon = 0;
    
    [Tooltip("How much enemy speed increases per upgrade")]
    [SerializeField] private float enemySpeedIncrement;
    
    [SerializeField] private EnemyObject[] enemyTypes;
    [SerializeField] private BossObject[] bossTypes;

    private float nextTimeToSpawn;
    private float spawnRate;
    private List<EnemyObject> enemies = new List<EnemyObject>();

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
        CheckEnemyTypes();

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
        float totalChance = 0;
        foreach (EnemyObject enemy in enemies) totalChance += enemy.chance;

        float randomChance = Random.Range(0, totalChance);
        EnemyObject randomEnemy = enemies[0];
        float lastChanceSum = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            float lastChance = i > 0 ? enemies[i - 1].chance : 0;
            if (randomChance >= lastChance && randomChance < enemies[i].chance + lastChanceSum)
            {
                randomEnemy = enemies[i];
                break;
            }

            lastChanceSum += enemies[i].chance;
        }

        GetRandomPosition(out float x, out float y);
        GameObject newEnemy = Instantiate(randomEnemy.prefab, new Vector2(x, y), Quaternion.identity);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.health += enemyScript.health / 100 * enemyHealthAddon;
    }

    public void EnemyBuff() {
        spawnRate += spawnRateIncrement;
        enemySpeed += enemySpeedIncrement;
    }

    public void CheckEnemyTypes() {
        foreach (EnemyObject enemy in enemyTypes)
        {
            if (UpgradeManager.Instance.level >= enemy.level && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }

    public void SpawnBosses() {
        print(UpgradeManager.Instance.level);
        foreach (BossObject boss in bossTypes)
        {
            if (UpgradeManager.Instance.level % boss.level == 0)
            {
                GetRandomPosition(out float x, out float y);
                GameObject newBoss = Instantiate(boss.prefab, new Vector2(x, y), Quaternion.identity);
            }
        }
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
