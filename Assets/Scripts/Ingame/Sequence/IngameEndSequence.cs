using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// インゲーム終了時のシーケンスを管理します。
/// </summary>
public class IngameEndSequence : MonoBehaviour
{
    public static PlayerKind WinnerKind { get; private set; }

    public enum PlayerKind
    {
        Player1,
        Player2
    }

    // --- シリアライズされたフィールド ---
    [Header("リザルトシーンのシーン名")]
    [SerializeField] private string _resultSceneName = "Result";

    [SerializeField]
    private Player_Main_System _player1;
    [SerializeField]
    private Player_Main_System _player2;
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
    private async void OnAnyPlayerDeadHandler(Player_Main_System losePlayer)
    {
        Debug.Log("ゲーム終了。");
        // プレイヤーの入力を無効化します。
        _playerManager.DisablePlayerInput();

        // 勝者のプレイヤーを決定します。
        if (losePlayer == _player1)
        {
            WinnerKind = PlayerKind.Player2;
            Debug.Log("プレイヤー2が勝利しました。");
        }
        else if (losePlayer == _player2)
        {
            WinnerKind = PlayerKind.Player1;
            Debug.Log("プレイヤー1が勝利しました。");
        }
        else
        {
            Debug.LogError("不明なプレイヤーが死亡しました。", this);
            return;
        }

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