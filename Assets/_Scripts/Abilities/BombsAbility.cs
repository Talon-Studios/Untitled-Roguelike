using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsAbility : MonoBehaviour
{

    public Bomb bombPrefab;
    [SerializeField] private int bombCount = 3;

    #region Singleton
    
    static public BombsAbility Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void Activate() {
        for (int i = 0; i < bombCount; i++)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }

    public void MoreBombs() {
        bombCount += 1;
    }

}
