using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameEndSequence : MonoBehaviour
{
    [SerializeField]    
    private string ResultSceneName = "Result"; // シーン名を定義

    private PlayerManager _playerManager;

    public void RegisterPlayerManager(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        _playerManager.OnAnyPlayerDead += HandleAnyPlayerDead;
    }

    private async void HandleAnyPlayerDead()
    {
        Debug.Log("Game End");
        _playerManager.ResetInput();

        await Awaitable.WaitForSecondsAsync(2f);

        SceneManager.LoadScene(ResultSceneName);
    }
}
