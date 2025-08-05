using SymphonyFrameWork.Attribute;
using System;
using UnityEngine;

/// <summary>
/// プレイヤーの体力や状態などのステータスを管理します。
/// </summary>
public class Player_Status : MonoBehaviour
{
    // --- イベント ---
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    // --- プロパティ ---
    /// <summary>
    /// プレイヤーが死亡しているかどうかを取得します。
    /// </summary>
    public bool IsDead => _isDead;

    // --- シリアライズされたフィールド ---
    [Header("プレイヤーのステータス情報")]
    [SerializeField] private PlayerData _playerData;

    // --- privateフィールド ---
    [SerializeField, ReadOnly] private float _currentHealth;
    private bool _isDead;
    private float _invincibilityTimer;

    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private void Start()
    {
        // ステータスを初期化します。
        InitializeStatus();
    }

    /// <summary>
    /// ステータスを初期化します。
    /// </summary>
    private void InitializeStatus()
    {
        // プレイヤーデータを参照してステータスを設定します。
        _currentHealth = _playerData.PlayerMaxHealth;
        _isDead = false;
    }

    /// <summary>
    /// ダメージを受け、体力を減少させます。
    /// </summary>
    /// <param name="damageAmount">受けるダメージの量。</param>
    public void TakeDamage(float damageAmount)
    {
        // 死亡している、または無敵時間中はダメージを受け付けません。
        if (_isDead || Time.time < _invincibilityTimer)
        {
            return;
        }

        // デバッグようにダメージログを出力します。
        Debug.Log($"{gameObject.name} took {damageAmount} damage.");

        // 体力を減少させます。
        _currentHealth -= damageAmount;

        // 無敵時間を設定します。
        _invincibilityTimer = Time.time + _playerData.InvincibilityTime;

        // 体力が0以下になった場合の処理。
        if (_currentHealth <= 0)
        {
            _isDead = true;
            _currentHealth = 0;
            // 死亡イベントを発行します。
            OnDeath?.Invoke();
        }

        // HPバーなどを更新するために、HPの変化を通知します。
        OnHealthChanged?.Invoke(_currentHealth / _playerData.PlayerMaxHealth);
    }
}