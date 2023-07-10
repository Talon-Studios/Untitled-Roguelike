using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class CanvasScaleFactorAdjuster : MonoBehaviour
{

    PixelPerfectCamera pixelCam;
    CanvasScaler scaler;

    void Start() {
        scaler = GetComponent<CanvasScaler>();
        pixelCam = Camera.main.GetComponent<PixelPerfectCamera>();
    }

    void LateUpdate() {

    }

    private void AdjustScalingFactor() {
        scaler.scaleFactor = pixelCam.pixelRatio;
    }

}
