using UnityEngine;

public class IngameStartSequence : MonoBehaviour
{
    [SerializeField]
    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager.GeneratePlayer();
        _playerManager.SetInput();

        var endSequence = GetComponent<IngameEndSequence>();
        endSequence.RegisterPlayerManager(_playerManager);
    }
}
