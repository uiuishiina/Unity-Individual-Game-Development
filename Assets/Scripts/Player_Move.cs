using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Move : MonoBehaviour
{
    [SerializeField, Header("移動スピード")] private float MoveSpeed = 1;
    [SerializeField, Header("プレイヤーモデル")] private GameObject PlayerModel_;
    PlayerInput playerInput;
    Rigidbody2D rb;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //移動処理
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocity = move * MoveSpeed;

        //移動時の回転処理
        if (move != Vector2.zero) {
            PlayerModel_.transform.rotation = My_Rotate(move);
        }

        //回転処理
        var look = playerInput.actions["Look"].ReadValue<Vector2>();
        if (look != Vector2.zero) {
            PlayerModel_.transform.rotation = My_Rotate(look);
        }
    }

    /// <summary>
    /// 回転角度計算関数
    /// </summary>
    /// <param name="vector">入力ベクトル</param>
    /// <returns></returns>
    Quaternion My_Rotate(Vector2 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90;
        return Quaternion.Euler(0, 0, angle);
    }
}
