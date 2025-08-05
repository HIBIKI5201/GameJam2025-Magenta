using UnityEngine;
using UnityEngine.UI;

public class Resultseq : MonoBehaviour
{
    [SerializeField] private GameObject _player1Panel;
    [SerializeField] private GameObject _player2Panel;

    private void Start()
    {
        _player1Panel.SetActive(false);
        _player2Panel.SetActive(false);

        IngameEndSequence.PlayerKind winner = IngameEndSequence.WinnerKind;

         GameObject panel = GetWinnerSprite(winner);
        panel.SetActive(true);
    }

    private GameObject GetWinnerSprite(IngameEndSequence.PlayerKind player)
    {
        switch (player)
        {
            case IngameEndSequence.PlayerKind.Player1:
                return _player1Panel;
            case IngameEndSequence.PlayerKind.Player2:
                return _player2Panel;
            default:
                return null;
        }
    }
}
