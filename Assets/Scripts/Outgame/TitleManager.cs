using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面を管理するクラス
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class TitleManager : MonoBehaviour
{
    // ゲームシーン名
    [SerializeField] string _GameScene;

    // プレイヤーインプット
    PlayerInput _p;
    // プレイヤー1の入力フラグ
    private bool _move1Pressed = false;
    // プレイヤー2の入力フラグ
    private bool _move2Pressed = false;

    /// <summary>
    /// 初期化処理 (Awake)
    /// </summary>
    private void Awake()
    {
        _p = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// 有効になった際の処理
    /// </summary>
    private void OnEnable()
    {
        // 入力アクションにコールバックを登録
        _p.actions["Move1"].started += OnMove1Started;
        _p.actions["Move2"].started += OnMove2Started;

        _p.actions["Move1"].canceled += OnMove1Canceled;
        _p.actions["Move2"].canceled += OnMove2Canceled;
    }

    /// <summary>
    /// 無効になった際の処理
    /// </summary>
    private void OnDisable()
    {
        // 入力アクションからコールバックを解除
        _p.actions["Move1"].started -= OnMove1Started;
        _p.actions["Move2"].started -= OnMove2Started;

        _p.actions["Move1"].canceled -= OnMove1Canceled;
        _p.actions["Move2"].canceled -= OnMove2Canceled;
    }

    /// <summary>
    /// プレイヤー1の入力開始処理
    /// </summary>
    /// <param name="context">入力コンテキスト</param>
    private void OnMove1Started(InputAction.CallbackContext context)
    {
        _move1Pressed = true;
        CheckBothPressed();
    }

    /// <summary>
    /// プレイヤー2の入力開始処理
    /// </summary>
    /// <param name="context">入力コンテキスト</param>
    private void OnMove2Started(InputAction.CallbackContext context)
    {
        _move2Pressed = true;
        CheckBothPressed();
    }

    /// <summary>
    /// プレイヤー1の入力終了処理
    /// </summary>
    /// <param name="context">入力コンテキスト</param>
    private void OnMove1Canceled(InputAction.CallbackContext context)
    {
        _move1Pressed = false;
    }

    /// <summary>
    /// プレイヤー2の入力終了処理
    /// </summary>
    /// <param name="context">入力コンテキスト</param>
    private void OnMove2Canceled(InputAction.CallbackContext context)
    {
        _move2Pressed = false;
    }

    /// <summary>
    /// 両プレイヤーの入力があったか確認する
    /// </summary>
    private void CheckBothPressed()
    {
        // 両プレイヤーが入力している場合
        if (_move1Pressed && _move2Pressed)
        {
            Debug.Log("両方押されたのでシーンを移動！");
            // ゲームシーンに遷移
            SceneManager.LoadScene(_GameScene);
        }
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Unityエディタの場合は再生を停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後はアプリケーションを終了
        Application.Quit();
#endif
    }
}