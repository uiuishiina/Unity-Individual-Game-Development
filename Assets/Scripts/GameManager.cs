using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("オブジェクト参照")]
    [SerializeField, Tooltip("スピードテキスト")] private TextMeshProUGUI SpeedText;
    [SerializeField, Tooltip("Player_Move_Instance")] private Player_Move Player_Move;

    /// <summary>
    /// 更新関数
    /// </summary>
    private void FixedUpdate()
    {
        TextUpDate();
    }

    /// <summary>
    /// テキストアップデート関数
    /// </summary>
    void TextUpDate()
    {
        var speed = Player_Move.MoveSpeed;
        speed *= (3600 * 0.1f * Time.fixedDeltaTime);
        SpeedText.text = speed.ToString("f0") + " / km";
    }
}
