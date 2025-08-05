using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーをスポーンするクラス
/// </summary>
public class PlayerSpawn : MonoBehaviour
{
    // プレイヤーのプレハブ
    [SerializeField]
    private GameObject playerPrefab;

    // プレイヤーのインデックス
    private int playerIndex = 0;

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        // Pキーが押されたらプレイヤーをスポーン
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            SpawnPlayer();
        }
    }

    /// <summary>
    /// プレイヤーをスポーンする
    /// </summary>
    public void SpawnPlayer()
    {
        // プレハブが設定されていない場合はエラーログを出力
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefabが設定されていません。インスペクターで設定してください。");
            return;
        }

        // PlayerInputを使用してプレイヤーを生成
        PlayerInput newPlayer = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: playerIndex,
            pairWithDevice: null
        );

        // スポーンログを出力
        Debug.Log($"プレイヤー {playerIndex} をスポーンしました。");
        // プレイヤーインデックスをインクリメント
        playerIndex++;
    }
}