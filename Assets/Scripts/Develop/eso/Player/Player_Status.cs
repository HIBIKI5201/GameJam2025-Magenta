using SymphonyFrameWork.Attribute;
using System;
using UnityEngine;

/// <summary>
/// プレイヤーのステータスを管理するクラス
/// </summary>
public class Player_Status : MonoBehaviour
{
    public Action OnDeath; // 死亡時のイベント

    // 現在のHP
    [SerializeField, ReadOnly] int _now_hp;
    // 最大HP
    [SerializeField] int _max_hp;
    // 死亡フラグ
    private bool _is_death;

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
        _now_hp = _max_hp;
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
        // ダメージログを出力
        Debug.Log(damage);
        // HPを減らす
        _now_hp -= damage;

        // HPが0以下になったら死亡
        if (_now_hp <= 0)
        {
            _is_death = true;
            _now_hp = 0;
        }
    }
}