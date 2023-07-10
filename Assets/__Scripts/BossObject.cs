using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss", fileName = "New Boss")]
public class BossObject : ScriptableObject
{

    public GameObject prefab;
    public int level = 5;

}
