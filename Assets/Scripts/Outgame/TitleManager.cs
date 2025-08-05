using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のUIとシーン遷移を管理します。
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class TitleManager : MonoBehaviour
{
    [SerializeField] private TitleUiManager uiManager;
    // --- シリアライズされたフィールド ---
    [Header("ゲームシーンのシーン名")]
    [SerializeField] private string _gameSceneName;

    // --- privateフィールド ---
    private PlayerInput _playerInput;
    private bool _isPlayer1MovePressed = false;
    private bool _isPlayer2MovePressed = false;

    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private void Awake()
    {
        if (uiManager) uiManager.SetFunction(
            () => _isPlayer1MovePressed,
            () => _isPlayer2MovePressed,
            () => SceneLoadUtility.LoadScene(_gameSceneName)
            );
        _playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。コンポーネントが有効になった時に呼び出されます。
    /// </summary>
    private void OnEnable()
    {
        // 入力アクションにコールバックを登録します。
        _playerInput.actions["Move1"].started += OnPlayer1MoveStarted;
        _playerInput.actions["Move2"].started += OnPlayer2MoveStarted;

        _playerInput.actions["Move1"].canceled += OnPlayer1MoveCanceled;
        _playerInput.actions["Move2"].canceled += OnPlayer2MoveCanceled;

        _playerInput.actions["Select1"].started += uiManager.PageChangerAction;
        _playerInput.actions["Select2"].started += uiManager.PageChangerAction;

        _playerInput.actions["Decision"].started += uiManager.OperationPanelChange;
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。コンポーネントが無効になった時に呼び出されます。
    /// </summary>
    private void OnDisable()
    {
        // 入力アクションからコールバックを解除します。
        _playerInput.actions["Move1"].started -= OnPlayer1MoveStarted;
        _playerInput.actions["Move2"].started -= OnPlayer2MoveStarted;

        _playerInput.actions["Move1"].canceled -= OnPlayer1MoveCanceled;
        _playerInput.actions["Move2"].canceled -= OnPlayer2MoveCanceled;

        _playerInput.actions["Select1"].started -= uiManager.PageChangerAction;
        _playerInput.actions["Select2"].started -= uiManager.PageChangerAction;

        _playerInput.actions["Decision"].started -= uiManager.OperationPanelChange;
    }

    /// <summary>
    /// プレイヤー1の移動入力が開始された時に呼び出されます。
    /// </summary>
    /// <param name="context">入力コンテキスト。</param>
    private void OnPlayer1MoveStarted(InputAction.CallbackContext context)
    {
        _isPlayer1MovePressed = true;
        CheckBothPlayersPressed();
    }

    /// <summary>
    /// プレイヤー2の移動入力が開始された時に呼び出されます。
    /// </summary>
    /// <param name="context">入力コンテキスト。</param>
    private void OnPlayer2MoveStarted(InputAction.CallbackContext context)
    {
        _isPlayer2MovePressed = true;
        CheckBothPlayersPressed();
    }

    /// <summary>
    /// プレイヤー1の移動入力が終了した時に呼び出されます。
    /// </summary>
    /// <param name="context">入力コンテキスト。</param>
    private void OnPlayer1MoveCanceled(InputAction.CallbackContext context)
    {
        _isPlayer1MovePressed = false;
    }

    /// <summary>
    /// プレイヤー2の移動入力が終了した時に呼び出されます。
    /// </summary>
    /// <param name="context">入力コンテキスト。</param>
    private void OnPlayer2MoveCanceled(InputAction.CallbackContext context)
    {
        _isPlayer2MovePressed = false;
    }

    /// <summary>
    /// 両方のプレイヤーが同時にボタンを押しているかを確認し、ゲームシーンへ遷移します。
    /// </summary>
    private void CheckBothPlayersPressed()
    {
        // 両プレイヤーが入力している場合、ゲームシーンへ遷移します。
        if (_isPlayer1MovePressed && _isPlayer2MovePressed)
        {
            if (uiManager.GetOperationPanelActive())
            {
                SceneLoadUtility.LoadScene(_gameSceneName);
            }
            else
            {
                //uiManager.OperationPanelChange();
            }
        }
    }

    /// <summary>
    /// ゲームを終了します。
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Unityエディタの場合、再生を停止します。
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルドされたアプリケーションの場合、アプリケーションを終了します。
        Application.Quit();
#endif
    }
}