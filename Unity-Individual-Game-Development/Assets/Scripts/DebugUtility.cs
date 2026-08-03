using UnityEngine;

public static class DebugUtility
{
    /// <summary>
    /// ログ出力関数
    /// </summary>
    /// <param name="log">ログ</param>
    public static void Log(string log) {
        Debug.Log(log);
    }

    /// <summary>
    /// エラーログ出力関数
    /// </summary>
    /// <param name="log">エラーログ</param>
    public static void ErrorLog(string log) {
        Debug.LogError(log);
    }

    /// <summary>
    /// Null判定関数
    /// </summary>
    /// <typeparam name="T">判定する型(自動変換)</typeparam>
    /// <param name="value">判定する変数</param>
    /// <returns>Null == True</returns>
    public static bool IsNull<T>(T value) {
        return value == null;
    }

    /// <summary>
    /// Nullチェック関数
    /// </summary>
    /// <typeparam name="T">チェックする型(自動変換)</typeparam>
    /// <param name="checkvalue">チェックする変数</param>
    /// <param name="log">Null時出力ログ</param>
    /// <returns>Null == True</returns>
    public static bool NullCheck<T>(T checkvalue,string log)
    {
        if (IsNull<T>(checkvalue)) {
            Log(log);
            return true;
        }
        return false;
    }
}
