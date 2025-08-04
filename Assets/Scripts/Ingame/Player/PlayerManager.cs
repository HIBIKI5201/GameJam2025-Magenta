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
        // プレイヤー情報を初期化
        _players = new Player_Main_System[_playerInfos.Length];

        // プレイヤーを生成
        for (int i = 0; i < _playerInfos.Length; i++)
        {
            var info = _playerInfos[i];

            // プレイヤーを生成
            var player = Instantiate(info.Player_Controller, info.SpawnPosition, Quaternion.identity);

            // プレイヤー情報を設定
            _players[i] = player;
            player.GetComponent<Player_Movement>().SetMovementArea(_movement_Area_Tran, _movement_Area);
            player.transform.SetParent(_movement_Area_Tran);
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
    // プレイヤー情報
    [SerializeField]
    private PlayerInfo[] _playerInfos = new PlayerInfo[2];

    // 生成したプレイヤー
    private Player_Main_System[] _players;
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


    /// <summary>
    /// プレイヤー情報
    /// </summary>
    [Serializable]
    private struct PlayerInfo
    {
        public Player_Main_System Player_Controller => _playerPrefab;
        public Vector3 SpawnPosition => _spawnPosition;

        [SerializeField]
        private Vector3 _spawnPosition;
        [SerializeField]
        private Player_Main_System _playerPrefab;

    }
}