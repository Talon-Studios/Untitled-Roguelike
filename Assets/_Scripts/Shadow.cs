using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    private float offset = 0.2f;
    [SerializeField] private Color shadowColor = Color.black;

    private Vector3 direction = new Vector3(1, -1);
    
    SpriteRenderer graphics;
    SpriteRenderer shadow;

    void Start() {
        graphics = GetComponent<SpriteRenderer>();

        shadow = new GameObject("Shadow").AddComponent<SpriteRenderer>();
        shadow.transform.parent = transform;
        shadow.transform.localPosition = direction * offset;
        shadow.transform.localScale = Vector3.one;
        
        shadow.sprite = graphics.sprite;
        shadow.color = shadowColor;
        shadow.sortingLayerName = "Background";
        shadow.sortingOrder = 1;

        if (graphics.drawMode == SpriteDrawMode.Sliced)
        {
            shadow.drawMode = SpriteDrawMode.Sliced;
            shadow.size = graphics.size;
        }
    }

    void LateUpdate() {
        shadow.transform.localPosition = transform.InverseTransformDirection(direction * offset);
    }

}
