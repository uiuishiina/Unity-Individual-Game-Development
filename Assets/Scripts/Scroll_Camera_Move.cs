using UnityEngine;

public class Scroll_Camera_Move : MonoBehaviour
{
    private Camera MainCamera;
    [SerializeField, Header("オフセット")] private Vector2 CameraOffset = new Vector2(3,5);
    [SerializeField, Header("追従ターゲット")] private GameObject Target;

    private void Start()
    {
        MainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        var length = Target.transform.position - MainCamera.transform.position;

        var move_vec = new Vector3(ApplyDeadZone(length.x, CameraOffset.x), ApplyDeadZone(length.y, CameraOffset.y), 0);

        if(move_vec != Vector3.zero) {
            transform.Translate(move_vec);
        }
    }

    /// <summary>
    /// 画面移動オフセット計算処理関数
    /// </summary>
    float ApplyDeadZone(float value, float offset)
    {
        //符号を外して計算しやすくする
        float abs = Mathf.Abs(value);

        //オフセット以下なら0
        if (abs <= offset) {
            return 0f;
        }
        
        //オフセット以上なら必要な移動量を返す
        return (abs - offset) * Mathf.Sign(value);
    }
}
