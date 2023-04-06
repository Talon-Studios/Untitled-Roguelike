using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

    static public void DirectionToRotation(Vector2 direction, out Quaternion rotation) {
        rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.identity * direction);
    }

}
