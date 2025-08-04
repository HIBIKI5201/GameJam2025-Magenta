using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    private int playerIndex = 0;

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            SpawnPlayer();
        }
    }
    public void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefabが設定されていません。インスペクターで設定してください。");
            return;
        }

        // PlayerInput.Instantiate()メソッドを使用してプレイヤーを生成します。
        // playerPrefab: 生成するプレハブ
        // playerIndex: プレイヤーのインデックス（0から始まる）
        // splitScreenIndex: 画面分割のインデックス
        // controlScheme: 使用するコントロールスキームの名前
        // pairWithDevice: プレイヤーに割り当てるデバイス。nullの場合は最初の使用可能なデバイスが割り当てられます。
        PlayerInput newPlayer = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: playerIndex,
            pairWithDevice: null
        );

        Debug.Log($"プレイヤー {playerIndex} をスポーンしました。");
        playerIndex++; // 次のプレイヤーのためにインデックスを更新します。
    }
}
