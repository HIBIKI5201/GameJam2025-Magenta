using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 _movement_Area;
    [SerializeField]
    private Transform _movement_Area_Tran;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private PlayerInfo[] _playerInfos = new PlayerInfo[2];

    private GameObject[] _players = new GameObject[2];
    private void Start()
    {
        if (!TryGetComponent(out PlayerInput input)) return;

        for (int i = 0; i < _players.Length; i++)
        {
            var player = Instantiate(_playerPrefab, _playerInfos[i].SpawnPosition, Quaternion.identity);

            _players[i] = player;
            player.tag = _playerInfos[i].MyTag;
            player.transform.GetChild(0).tag = _playerInfos[i].MyTag;
            player.GetComponent<Player_Controller>().SetAction(input, _playerInfos[i].MoveActionName);
            player.GetComponent<Player_Movement>().SetMovementArea(_movement_Area_Tran.position, _movement_Area);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.localPosition, _movement_Area * 2);
    }
[Serializable]
    private struct PlayerInfo
    {
        public string MoveActionName => _moveActionName;

        [SerializeField]
        private string _moveActionName;

        public Vector3 SpawnPosition => _spawnPosition;

        [SerializeField]
        private Vector3 _spawnPosition;

        public String MyTag => _myTag;

        [SerializeField]
        private String _myTag;
    }
}
