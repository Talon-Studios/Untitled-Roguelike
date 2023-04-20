using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallWeapon : MonoBehaviour
{

    [SerializeField] private Rigidbody2D ballPrefab;
    [SerializeField] private float startSpeed = 5;

    #region Singleton
    
    static public BallWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void AddBall() {
        Instantiate(ballPrefab, transform.position, Quaternion.identity).AddForce(Random.insideUnitCircle.normalized * startSpeed, ForceMode2D.Impulse);
    }

}
