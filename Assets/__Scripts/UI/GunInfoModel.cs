using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInfoModel : MonoBehaviour
{

    [SerializeField] private float speed = 5;
    [SerializeField] private float hoverSpeed = 2;
    [SerializeField] private float hoverMagnitude = 2;

    Vector2 originalPos;

    void Start() {
        originalPos = transform.position - (Vector3.up * Mathf.Sin(Time.time * hoverSpeed) * hoverMagnitude);
    }

    void Update() {
        transform.Rotate(0, speed * Time.deltaTime, 0, Space.World);

        transform.position = Vector2.up * Mathf.Sin(Time.time * hoverSpeed) * hoverMagnitude + originalPos;
    }

}
