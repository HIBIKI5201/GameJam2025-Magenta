using UnityEngine;
using UnityEngine.UI;

public class Resultseq : MonoBehaviour
{
    [SerializeField] private Text _winnerText;

    private void Start()
    {
        // IngameEndSequence から勝者を取得
        IngameEndSequence.PlayerKind winner = IngameEndSequence.WinnerKind;

        // 勝者テキストを設定
        _winnerText.text = GetWinnerText(winner);
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
}
