using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    PlayerInput inputsystem;
    Rigidbody rb;
    [Header("プレイヤー設定")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity = 9.8f;

    private void Awake() {
        inputsystem = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        
    }

    private void Update() {

        var move = inputsystem.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocity = new Vector3(move.x * moveSpeed, -gravity * Time.deltaTime, move.y * moveSpeed);
    }
}
