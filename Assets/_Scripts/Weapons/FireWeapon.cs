using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireWeapon : MonoBehaviour
{

    [SerializeField] private float spawnDelay = 2;
    [SerializeField] private float startForce = 2;
    [SerializeField] private Rigidbody2D fireballPrefab;

    #region Singleton
    
    static public FireWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void ActivateWeapon() {
        StartCoroutine(SpawnFireballRoutine());
    }

    private IEnumerator SpawnFireballRoutine() {
        while (true)
        {   
            yield return new WaitForSeconds(spawnDelay);
            SpawnFireball();
        }
    }

    private void SpawnFireball() {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = allEnemies.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
        Vector2 direction = closestEnemy.transform.position - transform.position;
        Rigidbody2D fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        fireball.AddForce(direction.normalized * startForce, ForceMode2D.Impulse);
    }

}
