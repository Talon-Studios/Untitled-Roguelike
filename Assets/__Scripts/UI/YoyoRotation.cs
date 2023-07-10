using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoRotation : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 2;
    [SerializeField] private float rotationMagnitude = 2;

    void Update() {
        float rotation = Mathf.Sin(rotationSpeed * Time.unscaledTime) * rotationMagnitude;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

}
