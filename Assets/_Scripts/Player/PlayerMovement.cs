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
    [SerializeField] private float tilt = 15;
    [SerializeField] private float tiltSpeed = 1;

    private Vector2 inputDirection;
    private float targetRotation;

    Rigidbody2D playerBody;

    #region Singleton
    
    static public PlayerMovement Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (canMove)
        {
            playerBody.velocity = inputDirection * speed;
            targetRotation = -inputDirection.x * tilt;
            graphics.rotation = Quaternion.RotateTowards(graphics.rotation, Quaternion.Euler(0, 0, targetRotation), tiltSpeed * Time.deltaTime);
        }
    }

    void OnMovement(InputValue value) {
        inputDirection = value.Get<Vector2>();
    }

}
