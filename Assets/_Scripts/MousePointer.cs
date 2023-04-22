using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum MouseState
{
    Aiming,
    Interacting
}

public class MousePointer : MonoBehaviour
{

    [Header("States")]
    [SerializeField] private Texture2D aimingState;
    [SerializeField] private Texture2D interactingState;

    private MouseState state;

    RawImage image;
    Camera cam;
    RectTransform canvas;

    #region Singleton
    
    static public MousePointer Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        cam = Camera.main;
        canvas = GameObject.FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        image = GetComponent<RawImage>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update() {
        transform.localPosition = Mouse.current.position.ReadValue() - (canvas.sizeDelta / 2);
    }

    void OnApplicationFocus(bool focus) {
        if (focus)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void SetMouseState(MouseState newState) {
        switch (newState)
        {
            case MouseState.Aiming: { image.texture = aimingState; break; }
            case MouseState.Interacting: { image.texture = interactingState; break; }
        }
    }

}
