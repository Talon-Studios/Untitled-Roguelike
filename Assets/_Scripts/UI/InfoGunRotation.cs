using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoGunRotation : MonoBehaviour
{

    [SerializeField] private float speed = 5;

    void Update() {
        transform.Rotate(0, speed * Time.deltaTime, 0, Space.World);
    }

}
