using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    // 移動可能範囲
    [SerializeField]
    private Vector3 _movement_Area;
    // 移動可能範囲の中心
    [SerializeField]
    private Transform _movement_Area_Tran;
    // プレイヤーのプレハブ
    [SerializeField]
    private Player_Controller _playerPrefab;
    // プレイヤー情報
    [SerializeField]
    private PlayerInfo[] _playerInfos = new PlayerInfo[2];

    // 生成したプレイヤー
    private Player_Controller[] _players;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        // PlayerInputコンポーネントを取得
        if (!TryGetComponent(out PlayerInput input)) return;

        // プレイヤー情報を初期化
        _players = new Player_Controller[_playerInfos.Length];

        // プレイヤーを生成
        for (int i = 0; i < _playerInfos.Length; i++)
        {
            // プレイヤーを生成
            var player = Instantiate(_playerPrefab, _playerInfos[i].SpawnPosition, Quaternion.identity);

            // プレイヤー情報を設定
            _players[i] = player;
            player.GetComponent<Player_Controller>().SetAction(input);
            player.GetComponent<Player_Movement>().SetMovementArea(_movement_Area_Tran.position, _movement_Area);
        }
    }

    /// <summary>
    /// ギズモを描画
    /// </summary>
    private void OnDrawGizmos()
    {
        // 移動可能範囲をワイヤーフレームで表示
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.localPosition, _movement_Area * 2);
    }

    /// <summary>
    /// プレイヤー情報
    /// </summary>
    [Serializable]
    private struct PlayerInfo
    {
        public Player_Controller Player_Controller => _playerPrefab;
        public Vector3 SpawnPosition => _spawnPosition;

        [SerializeField]
        private Vector3 _spawnPosition;
        [SerializeField]
        private Player_Controller _playerPrefab;

    }
}