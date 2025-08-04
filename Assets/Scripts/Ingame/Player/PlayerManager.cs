using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    public event Action OnAnyPlayerDead
    {
        add
        {
            foreach (var player in _players)
            {
                player.Ondead += value;
            }
        }
        remove
        {
            foreach (var player in _players)
            {
                player.Ondead -= value;
            }
        }
    }

    public void GeneratePlayer()
    {
        // プレイヤーを生成
        for (int i = 0; i < _players.Length; i++)
        {
            // プレイヤー情報を設定
            var player = _players[i];
            player.GetComponent<Player_Movement>().SetMovementArea(_movement_Area_Tran, _movement_Area);
            player.transform.SetParent(_movement_Area_Tran);
            player.OnHealthChanged += _healthBars[i].SetFillAmount;
        }

        for (int i = 0; i < _players.Length; i++)
        {

            if (_players[i].TryGetComponent(out Player_Main_System playerMainSystem))
            {
                // プレイヤーのメインシステムを初期化
                playerMainSystem.Initialize(
                    _players[(i + 1) % _players.Length]);
            }
            else
            {
                Debug.LogError("Player_Main_System component is missing on the player prefab.");
            }
        }
    }

    public void SetInput()
    {
        // PlayerInputコンポーネントを取得
        if (!TryGetComponent(out _input)) return;

        // 各プレイヤーに入力を設定
        for (int i = 0; i < _players.Length; i++)
        {
            // プレイヤーの入力を設定
            _players[i].StartEntity(_input);
        }
    }

    public void ResetInput()
    {
        if (_input == null) return;

        foreach (var player in _players)
        {
            player.StopEntity();
        }
        _input.actions.Disable(); // 入力アクションを無効化
    }

    // 移動可能範囲
    [SerializeField]
    private Vector3 _movement_Area;
    // 移動可能範囲の中心
    [SerializeField]
    private Transform _movement_Area_Tran;

    [SerializeField]
    private Player_Main_System[] _players;

    [SerializeField]
    private ImageFillAmount[] _healthBars;

    private PlayerInput _input;
    /// <summary>
    /// ギズモを描画
    /// </summary>
    private void OnDrawGizmos()
    {
        // 移動可能範囲をワイヤーフレームで表示
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, _movement_Area * 2);
    }
}