using UnityEngine;
using UnityEngine.InputSystem;

public class Scroll_Player_Move : MonoBehaviour
{
    [SerializeField, Header("移動スピード")] private float MoveSpeed = 1;
    [SerializeField, Header("ジャンプ力")] private float JumpPower = 1;
    PlayerInput playerInput;
    Rigidbody2D rb;
    bool IsJump = false;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //移動処理
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocityX = move.x * MoveSpeed;

        //ジャンプ処理
        if (!IsJump && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            rb.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
            IsJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsJump && collision.gameObject.tag == "Floor")
        {
            IsJump = false;
        }
    }
}
