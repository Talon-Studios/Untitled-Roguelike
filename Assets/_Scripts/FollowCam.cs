using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform player;


    [Tooltip("How fast the camera moves to the player. Set to 0.05 to 0.5 for best effect")]
    [SerializeField] private float smoothing = 0.1f;

    [Tooltip("How much the camera looks where you're aiming")]
    [SerializeField] private float dynamicCamera = 5;
    
    [Tooltip("How fast the screen shake is. Set to 0.05 to 0.5 for best effect")]
    [SerializeField] private float screenShakeSmoothing = 0.1f;

    private Vector3 screenShake;

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
        screenShake = Vector2.Lerp(screenShake, Vector2.zero, screenShakeSmoothing);
    }

    void FixedUpdate() {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10);
        Vector3 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = mousePosition - targetPos;
        
        targetPos += direction.normalized * dynamicCamera;
        targetPos += screenShake;
        targetPos.z = -10;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

    public void ScreenShake(float magnitude, Vector2 direction) {
        screenShake = direction * magnitude;
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
