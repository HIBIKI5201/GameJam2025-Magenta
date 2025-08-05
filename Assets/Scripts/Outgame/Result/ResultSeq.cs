using UnityEngine;
using UnityEngine.UI;

public class Resultseq : MonoBehaviour
{
    [SerializeField] private Text _winnerText;
    [SerializeField] private Image _winnerImage;

    [SerializeField] private Sprite _player1Sprite;
    [SerializeField] private Sprite _player2Sprite;

    private void Start()
    {
        IngameEndSequence.PlayerKind winner = IngameEndSequence.WinnerKind;

        _winnerText.text = GetWinnerText(winner);
        _winnerImage.sprite = GetWinnerSprite(winner);
    }

    private string GetWinnerText(IngameEndSequence.PlayerKind player)
    {
        switch (player)
        {
            case IngameEndSequence.PlayerKind.Player1:
                return "プレイヤー１の勝利！";
            case IngameEndSequence.PlayerKind.Player2:
                return "プレイヤー２の勝利！";
            default:
                return "No Contest";
        }
    }

    private Sprite GetWinnerSprite(IngameEndSequence.PlayerKind player)
    {
        switch (player)
        {
            case IngameEndSequence.PlayerKind.Player1:
                return _player1Sprite;
            case IngameEndSequence.PlayerKind.Player2:
                return _player2Sprite;
            default:
                return null;
        }
    }
}
