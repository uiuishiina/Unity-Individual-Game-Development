using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Move : MonoBehaviour
{
    [Header("プレイヤー移動設定")]

    [SerializeField,Tooltip("移動スピード")]private float MoveSpeed = 0;
    [SerializeField, Tooltip("最高速度")] private float MaxSpeed = 10;
    [SerializeField, Tooltip("加速度設定")] private float Acceleration = 0.5f;
    [SerializeField, Tooltip("回転速度設定")] private float Rotation = 0.5f;
    [SerializeField, Tooltip("ドリフト時倍率設定")] private float DriftMag = 1.5f;
    [SerializeField, Tooltip("バック時倍率設定")] private float BackMag = 0.5f;
    [SerializeField] private float bodySmoothTime = 0.15f;

    [Header("その他")]
    [SerializeField, Tooltip("Body")] private GameObject Body;
    [SerializeField, Tooltip("CameraTarget")] private GameObject CameraTarget;

    //  プレイヤー移動設定
    private PlayerInput input;
    private Rigidbody rb;

    private Vector2 input_Vec;
    private float heading;
    private float moveHeading;
    private float bodyVelocity;
    [SerializeField]private bool is_drift = false;

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Start() {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 入力取得関数
    /// </summary>
    private void Update() {

        //  移動キー入力を取得
        input_Vec = input.actions["Move"].ReadValue<Vector2>();
        if (input.actions["Jump"].WasPressedThisFrame()) {
            is_drift = true;
        }
        else if (input.actions["Jump"].WasReleasedThisFrame()) {
            is_drift = false;
        }
    }

    /// <summary>
    /// 移動処理関数
    /// </summary>
    private void FixedUpdate() {

        //  前後移動処理
        MoveSpeed += Acceleration * input_Vec.y * Time.fixedDeltaTime;
        MoveSpeed *= 0.99f;
        MoveSpeed = Mathf.Clamp(MoveSpeed, -MaxSpeed * BackMag, MaxSpeed);

        //  回転処理
        heading += input_Vec.x * Rotation * (MoveSpeed / MaxSpeed);

        // ドリフト時の見た目オフセット
        float driftAngle = 0f;
        if (is_drift) {
            driftAngle = input_Vec.x * 20f;
        }
        float targetBodyHeading = heading + driftAngle;

        heading = Mathf.SmoothDampAngle(heading, targetBodyHeading, ref bodyVelocity, bodySmoothTime);
        Body.transform.rotation = Quaternion.Euler(0, heading, 0);

        // ドリフト中は進行方向の追従を遅らせる
        float followSpeed = is_drift ? 2.0f : DriftMag;
        moveHeading = Mathf.LerpAngle(moveHeading, heading, followSpeed * Time.fixedDeltaTime);

        if (CameraTarget != null) {
            CameraTarget.transform.rotation = Quaternion.Euler(0, moveHeading, 0);
        }

        //  最終的なポジション処理
        Vector3 moveDir = Quaternion.Euler(0, moveHeading, 0) * Vector3.forward;

        rb.linearVelocity = new Vector3(moveDir.x * MoveSpeed, rb.linearVelocity.y, moveDir.z * MoveSpeed);
    }
}
