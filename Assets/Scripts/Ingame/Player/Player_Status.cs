using SymphonyFrameWork.Attribute;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーのステータスを管理するクラス
/// </summary>
public class Player_Status : MonoBehaviour
{
    public Action<float> OnHealthChanged; // HPの変化を通知するイベント
    public Action OnDeath; // 死亡時のイベント

    // 現在のHP
    [SerializeField, ReadOnly] float _now_hp;
    // 最大HP
    [SerializeField] PlayerData _data;

    // 死亡フラグ
    private bool _is_death;

    private float _invincibilityTimer;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // ステータスを初期化
        StatusInitialize();
    }

    /// <summary>
    /// ステータスを初期化する
    /// </summary>
    private void StatusInitialize()
    {
        _now_hp = _data.MaxHealth;
        _is_death = false;
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void TakeDamage(int damage)
    {
        // 死亡している場合は処理しない
        if(_is_death) return;
        if (Time.time < _invincibilityTimer) return; // 無敵時間中はダメージを受けない

        // ダメージログを出力
        Debug.Log($"{gameObject.name} taked {damage} damage");
        // HPを減らす
        _now_hp -= damage;
        _invincibilityTimer = Time.time + _data.InvincibilityTime; // 無敵時間を設定

        // HPが0以下になったら死亡
        if (_now_hp <= 0)
        {
            _is_death = true;
            _now_hp = 0;
            OnDeath?.Invoke(); // 死亡イベントを発火
        }

        OnHealthChanged?.Invoke(_now_hp / _data.MaxHealth); // HPの変化を通知
    }
}