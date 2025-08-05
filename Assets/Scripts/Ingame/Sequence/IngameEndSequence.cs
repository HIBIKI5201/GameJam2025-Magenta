using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// インゲーム終了時のシーケンスを管理します。
/// </summary>
public class IngameEndSequence : MonoBehaviour
{
    // --- シリアライズされたフィールド ---
    [Header("リザルトシーンのシーン名")]
    [SerializeField] private string _resultSceneName = "Result";

    // --- privateフィールド ---
    private PlayerManager _playerManager;

    /// <summary>
    /// PlayerManagerを設定し、プレイヤーの死亡イベントを購読します。
    /// </summary>
    /// <param name="playerManager">設定するPlayerManagerのインスタンス。</param>
    public void SetPlayerManager(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        // 任意のプレイヤーが死亡した際のイベントを購読します。
        _playerManager.OnAnyPlayerDead += OnAnyPlayerDeadHandler;
    }

    /// <summary>
    /// 任意のプレイヤーが死亡した際に呼び出されるイベントハンドラです。
    /// </summary>
    private async void OnAnyPlayerDeadHandler()
    {
        Debug.Log("ゲーム終了。");
        // プレイヤーの入力を無効化します。
        _playerManager.DisablePlayerInput();

        // 2秒間待機します。
        await Awaitable.WaitForSecondsAsync(2f);

        // リザルトシーンへ遷移します。
        SceneManager.LoadScene(_resultSceneName);
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。このコンポーネントが破棄される際に呼び出されます。
    /// </summary>
    private void OnDestroy()
    {
        // メモリリークを防ぐため、イベントの購読を解除します。
        if (_playerManager != null)
        {
            _playerManager.OnAnyPlayerDead -= OnAnyPlayerDeadHandler;
        }
    }
}