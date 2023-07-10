using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGraphics : MonoBehaviour
{

    SpriteRenderer sprite;
    Color originalColor;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    private IEnumerator FlashRoutine(float duration, Color flashColor) {
        sprite.color = flashColor;
        yield return new WaitForSeconds(duration);
        sprite.color = originalColor;
    }

    public void Flash(float duration, Color flashColor) {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine(duration, flashColor));
    }

}
