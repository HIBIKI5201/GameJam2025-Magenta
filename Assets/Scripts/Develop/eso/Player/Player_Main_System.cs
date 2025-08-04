using System;
using UnityEngine;

/// <summary>
/// プレイヤーのメインシステムを管理するクラス
/// </summary>
public class Player_Main_System : MonoBehaviour
{
    public event Action Ondead
    {
        add => player_Status.OnDeath += value;
        remove => player_Status.OnDeath -= value;
    }

    // プレイヤーコントローラー
    [SerializeField] Player_Controller _player_Controller;
    // プレイヤーの移動
    [SerializeField] Player_Movement _player_Movement;
    // プレイヤーステータス
    [SerializeField] Player_Status player_Status;

    //---テスト---
    // テスト用の弾マネージャー
    [SerializeField] TestBulletManager bullet;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // 移動アクションを設定
        _player_Movement.SetAction(_player_Controller.GetMove);
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        // 弾を発射
        bullet.Shot();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {

    }

    /// <summary>
    /// トリガーに接触した際の処理
    /// </summary>
    /// <param name="other">衝突したコライダー</param>
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー1が自身の弾に当たった場合は処理しない
        if ((tag == "Player1") && other.CompareTag("Bullet1"))
        {
            return;
        }
        // プレイヤー2が自身の弾に当たった場合は処理しない
        if ((tag == "Player2") && other.CompareTag("Bullet2"))
        {
            return;
        }
        // ダメージを受ける
        player_Status.TakeDamage(1);
    }
}