using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartColorOrbs : MonoBehaviour
{

    [SerializeField] private Color[] colors;
    [SerializeField] private Image colorOrbPrefab;
    [SerializeField] private float radius = 5;
    [SerializeField] private float rotateSpeed = 100;

    void Start() {
        float angle = 0;
        for (int i = 0; i < colors.Length; i++)
        {
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            Vector3 pos = new Vector3(x, y) + transform.position;

            Image colorOrb = Instantiate(colorOrbPrefab, pos, Quaternion.identity, transform);
            colorOrb.color = colors[i];

            angle += 360 / colors.Length;
        }
    }

    void Update() {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

}
