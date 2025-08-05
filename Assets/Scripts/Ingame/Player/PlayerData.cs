using UnityEngine;

/// <summary>
/// プレイヤーの基本的なステータスを定義するデータアセットです。
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    // --- プロパティ ---
    /// <summary>
    /// プレイヤーの最大体力を取得します。
    /// </summary>
    public float PlayerMaxHealth => _playerMaxHealth;

    /// <summary>
    /// プレイヤーの移動速度を取得します。
    /// </summary>
    public float PlayerMoveSpeed => _playerMoveSpeed;

    /// <summary>
    /// ダメージを受けた後の無敵時間を取得します。
    /// </summary>
    public float InvincibilityTime => _playerInvincibilityTime;

    // --- シリアライズされたフィールド ---
    [Header("体力設定")]
    [SerializeField] private float _playerMaxHealth = 100f;

    [Header("移動設定")]
    [SerializeField] private float _playerMoveSpeed = 5f;

    [Header("無敵時間設定")]
    [SerializeField] private float _playerInvincibilityTime = 0.5f;
}