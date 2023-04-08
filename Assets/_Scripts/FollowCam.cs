using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCam : MonoBehaviour
{

    [Tooltip("How fast the camera moves to the player. Set to 0.05 to 0.5 for best effect")]
    [SerializeField] private float smoothing = 0.1f;

    [Tooltip("How much the camera looks where you're aiming")]
    [SerializeField] private float dynamicCamera = 5;
    
    [SerializeField] private Transform player;

    // Screen shake
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1.0f;

    Camera cam;

    #region Singleton
    
    static public FollowCam Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        cam = Camera.main;
    }

    void Update() {
        Vector3 initialPosition = transform.localPosition;

        if (shakeDuration > 0) {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.unscaledDeltaTime * dampingSpeed;
        } else {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    void FixedUpdate() {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10);
        Vector3 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = mousePosition - targetPos;
        
        targetPos += direction.normalized * dynamicCamera;
        targetPos.z = -10;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

    public void ScreenShake(float duration, float magnitude) {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void Hitstop(float duration) {
        StopAllCoroutines();
        StartCoroutine(HitstopRoutine(duration));
    }

    private IEnumerator HitstopRoutine(float duration) {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }

}
