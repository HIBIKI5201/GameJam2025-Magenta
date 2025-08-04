using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面のシーケンスを管理します。
/// </summary>
public class ResultSequence : MonoBehaviour
{
    // --- シリアライズされたフィールド ---
    [Header("タイトルシーンのシーン名")]
    [SerializeField] private string _titleSceneName = "Title";

    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private async void Start()
    {
        // 1秒間待機します。
        await Awaitable.WaitForSecondsAsync(1f);

        // タイトルシーンへ遷移します。
        SceneManager.LoadScene(_titleSceneName);
    }
}