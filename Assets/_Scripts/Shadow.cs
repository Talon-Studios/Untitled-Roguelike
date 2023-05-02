using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{

    private float offset = 0.2f;
    [SerializeField] private Color shadowColor = Color.black;

    private Vector3 direction = new Vector3(1, -1);
    
    Transform shadow;
    SpriteRenderer sprite;
    RawImage image;
    Camera cam;

    void Start() {
        cam = Camera.main;

        if (TryGetComponent<SpriteRenderer>(out sprite))
        {
            SpriteRenderer shadowSprite = new GameObject("Shadow").AddComponent<SpriteRenderer>();
            shadow = shadowSprite.transform;

            shadowSprite.sprite = sprite.sprite;
            shadowSprite.color = shadowColor;
            shadowSprite.sortingLayerName = "Background";
            shadowSprite.sortingOrder = 1;

            
            shadow.parent = transform;
            shadow.localPosition = direction * offset;
            shadow.localScale = Vector3.one;

            if (sprite.drawMode == SpriteDrawMode.Sliced)
            {
                shadowSprite.drawMode = SpriteDrawMode.Sliced;
                shadowSprite.size = sprite.size;
            }
        } else if (TryGetComponent<RawImage>(out image))
        {
            RawImage shadowImage = new GameObject("Shadow").AddComponent<RawImage>();
            shadow = shadowImage.transform;

            shadowImage.texture = image.texture;
            shadowImage.color = shadowColor;
            shadowImage.SetNativeSize();

            shadow.SetParent(transform.parent, false);
            shadow.SetAsFirstSibling();
        }
    }

    void LateUpdate() {
        if (sprite != null)
        {
            shadow.transform.localPosition = transform.InverseTransformDirection(direction * offset);
        } else if (image != null)
        {
            shadow.transform.localPosition = transform.InverseTransformDirection(cam.WorldToViewportPoint(direction * offset));
        }
    }

}
