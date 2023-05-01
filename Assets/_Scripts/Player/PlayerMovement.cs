using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public bool canMove = true;
    public float speed = 10;

    [Header("Animation & Graphics")]
    [SerializeField] private Transform graphics;
    [SerializeField] private float turnSpeed = 400;
    [SerializeField] private float squashAndStretchSpeed = 200;
    [SerializeField] private Vector2 squashAndStretch = new Vector2(-0.2f, 0.2f);
    [SerializeField] private Vector2 dashingSquashAndStretch = new Vector2(-0.5f, 0.5f);

    private Vector2 inputDirection;
    private Quaternion targetRotation;
    private Vector2 targetScale;

    Rigidbody2D playerBody;
    Vector2 originalScale;

    #region Singleton
    
    static public PlayerMovement Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();

        originalScale = graphics.localScale;
        targetScale = originalScale;
    }

    void Update() {
        if (canMove && Time.timeScale > 0)
        {
            playerBody.AddForce(inputDirection * speed);
            graphics.rotation = Quaternion.RotateTowards(graphics.rotation, targetRotation, turnSpeed * Time.deltaTime);
            graphics.localScale = Vector2.MoveTowards(graphics.localScale, targetScale, squashAndStretchSpeed * Time.deltaTime);
        }
    }

    public void DashAnimation() {
        targetScale = originalScale + dashingSquashAndStretch;
    }

    void OnMovement(InputValue value) {
        inputDirection = value.Get<Vector2>();
        if (inputDirection != Vector2.zero)
        {
            Utils.DirectionToRotation(inputDirection, out targetRotation);
            targetScale = originalScale + squashAndStretch;
        } else
        {
            targetScale = originalScale;
        }
    }

}
