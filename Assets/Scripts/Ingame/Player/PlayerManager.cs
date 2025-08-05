using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 複数のプレイヤーの生成、初期化、入力を一元管理します。
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    // --- イベント ---
    public event Action OnAnyPlayerDead;

    // --- シリアライズされたフィールド ---
    [Header("プレイヤーの移動範囲")]
    [SerializeField] private Vector3 _movementArea;
    [Header("プレイヤーの移動範囲の中心となるTransform")]
    [SerializeField] private Transform _movementAreaTransform;

    [Header("管理対象の全プレイヤー")]
    [SerializeField] private Player_Main_System[] _playerSystems;

    [Header("各プレイヤーに対応するHPバー")]
    [SerializeField] private ImageFillAmount[] _healthBars;

    // --- privateフィールド ---
    private PlayerInput _playerInput;

    /// <summary>
    /// 全てのプレイヤーを初期化し、ゲームプレイ可能な状態にします。
    /// </summary>
    public void InitializePlayers()
    {
        // 管理対象のプレイヤーとHPバーの数が一致しているか確認します。
        if (_playerSystems.Length != _healthBars.Length)
        {
            Debug.LogError("プレイヤーの数とHPバーの数が一致していません。インスペクターを確認してください。", this);
            return;
        }

        // 全てのプレイヤーをループして初期設定を行います。
        for (int i = 0; i < _playerSystems.Length; i++)
        {
            Player_Main_System currentPlayer = _playerSystems[i];
            Player_Main_System opponentPlayer = _playerSystems[(i + 1) % _playerSystems.Length];

            // 移動範囲と親オブジェクトを設定します。
            currentPlayer.GetComponent<Player_Movement>().SetMovementBounds(_movementAreaTransform, _movementArea);
            currentPlayer.transform.SetParent(_movementAreaTransform);

            // HPバーとプレイヤーのステータスを紐付けます。
            currentPlayer.PlayerStatus.OnHealthChanged += _healthBars[i].SetFillAmount;

            // 死亡イベントの購読設定を行います。
            SubscribePlayerDeathEvent(currentPlayer);

            // 対戦相手の情報を渡して、メインシステムを初期化します。
            currentPlayer.Initialize(opponentPlayer);
        }
    }

    /// <summary>
    /// プレイヤーの入力を有効にします。
    /// </summary>
    public void EnablePlayerInput()
    {
        // PlayerInputコンポーネントを取得します。
        if (!TryGetComponent(out _playerInput))
        {
            Debug.LogError("PlayerInputコンポーネントが見つかりません。", this);
            return;
        }

        // 各プレイヤーの入力処理を開始させます。
        foreach (var player in _playerSystems)
        {
            player.Activate(_playerInput);
        }
    }

    /// <summary>
    /// プレイヤーの入力を無効にします。
    /// </summary>
    public void DisablePlayerInput()
    {
        if (_playerInput == null) return;

        // 各プレイヤーの入力処理を停止させます。
        foreach (var player in _playerSystems)
        {
            player.Deactivate();
        }
        // 全てのアクションを無効化します。
        _playerInput.actions.Disable();
    }

    /// <summary>
    /// 指定されたプレイヤーの死亡イベントを購読します。
    /// </summary>
    /// <param name="player">対象のプレイヤー。</param>
    private void SubscribePlayerDeathEvent(Player_Main_System player)
    {
        player.PlayerStatus.OnDeath += HandlePlayerDeath;
    }

    /// <summary>
    /// プレイヤーの死亡イベントを処理します。
    /// </summary>
    private void HandlePlayerDeath()
    {
        // いずれかのプレイヤーが死亡したことを外部に通知します。
        OnAnyPlayerDead?.Invoke();
    }

    /// <summary>
    /// Unityエディタでギズモを描画する際に呼び出されます。
    /// </summary>
    private void OnDrawGizmos()
    {
        // 移動可能範囲を視覚的に分かりやすくするためにワイヤーフレームで表示します。
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireCube(_movementAreaTransform.position, _movementArea * 2f);
    }

    /// <summary>
    /// このコンポーネントが破棄される際に呼び出されます。
    /// </summary>
    private void OnDestroy()
    {
        // メモリリークを防ぐため、イベントの購読を解除します。
        if (_playerSystems == null) return;
        foreach (var player in _playerSystems)
        {
            if (player != null)
            {
                player.PlayerStatus.OnDeath -= HandlePlayerDeath;
            }
        }
    }
}