using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class EnemyObject : ScriptableObject
{

    public GameObject prefab;
    public int level = 3;
    public float chance = 30;

}
