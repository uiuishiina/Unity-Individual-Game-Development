using UnityEngine;
using UnityEngine.SceneManagement;

public class staticObject : MonoBehaviour
{
    public static staticObject Instance_;

    /// <summary>
    /// 初期作成関数
    /// </summary>
    protected virtual void Awake()
    {
        if (Instance_ != null && Instance_ != this) {
            Destroy(gameObject);
            return;
        }
        Instance_ = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// オブジェクト破棄時処理基底関数
    /// </summary>
    protected virtual void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// シーン開始時基底関数
    /// </summary>
    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
}
