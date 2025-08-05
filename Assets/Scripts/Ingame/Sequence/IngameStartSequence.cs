using SymphonyFrameWork.System;
using UnityEngine;

/// <summary>
/// インゲーム開始時のシーケンスを管理します。
/// </summary>
public class IngameStartSequence : MonoBehaviour
{
    // --- シリアライズされたフィールド ---
    [Header("プレイヤーマネージャー")]
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField]
    private AudioClip _bgm;
    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private void Start()
    {
        // ゲームの初期化処理を開始します。
        InitializeGame();
    }

    /// <summary>
    /// ゲームの初期化処理を実行します。
    /// </summary>
    private void InitializeGame()
    {
        // プレイヤーを生成し、初期化します。
        _playerManager.InitializePlayers();
        // プレイヤーの入力を有効にします。
        _playerManager.EnablePlayerInput();

        // IngameEndSequenceコンポーネントを取得し、PlayerManagerを登録します。
        IngameEndSequence endSequence = GetComponent<IngameEndSequence>();
        if (endSequence != null)
        {
            endSequence.SetPlayerManager(_playerManager);
        }
        else
        {
            Debug.LogError("IngameEndSequenceコンポーネントが見つかりません。", this);
        }

        if (_bgm)
        {
            AudioSource source = AudioManager.GetAudioSource(AudioGroupTypeEnum.BGM.ToString());
            source.clip = _bgm;
            source.Play();
        }
    }
}