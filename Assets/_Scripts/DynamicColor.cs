using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicColor : MonoBehaviour
{

    void Awake() {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite))
        {
            sprite.color = DynamicColorManager.Instance.color;
        } else if (TryGetComponent<ParticleSystem>(out ParticleSystem particles))
        {
            ParticleSystem.MainModule main =  particles.main;
            main.startColor = DynamicColorManager.Instance.color;
        } else if (TryGetComponent<LineRenderer>(out LineRenderer line))
        {
            line.startColor = DynamicColorManager.Instance.color;
            line.endColor = DynamicColorManager.Instance.color;
        } else if (TryGetComponent<Image>(out Image image))
        {
            image.color = DynamicColorManager.Instance.color;
        } else if (TryGetComponent<RawImage>(out RawImage rawImage))
        {
            rawImage.color = DynamicColorManager.Instance.color;
        }
    }

}
