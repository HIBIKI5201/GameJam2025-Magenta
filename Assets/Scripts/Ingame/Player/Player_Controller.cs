using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの入力（移動など）を処理します。
/// </summary>
public class Player_Controller : MonoBehaviour
{
    // --- シリアライズされたフィールド ---
    [Header("移動入力アクション名")]
    [SerializeField] private string _moveInputName;

    // --- privateフィールド ---
    private InputAction _moveAction;
    private Vector2 _currentMoveInput;

    /// <summary>
    /// このコントローラーを初期化し、入力アクションを設定します。
    /// </summary>
    /// <param name="playerInput">使用するPlayerInputコンポーネント。</param>
    public void Initialize(PlayerInput playerInput)
    {
        // PlayerInputがnullの場合は処理を中断します。
        if (playerInput == null)
        {
            Debug.LogError("PlayerInputが設定されていません。", this);
            return;
        }

        // 移動入力アクション名が設定されていない場合はエラーログを出力します。
        if (string.IsNullOrEmpty(_moveInputName))
        {
            Debug.LogError("移動入力アクション名が設定されていません。", this);
            return;
        }

        // アクションアセットから移動アクションを取得します。
        InputActionAsset actions = playerInput.actions;
        _moveAction = actions[_moveInputName];

        // アクションのPerformedとCanceledイベントにコールバックを登録します。
        _moveAction.performed += HandleMoveInput;
        _moveAction.canceled += HandleMoveInput;

        // アクションを有効化します。
        _moveAction.Enable();
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。コンポーネントが無効になった時に呼び出されます。
    /// </summary>
    private void OnDisable()
    {
        // アクションが有効であれば、コールバックを解除し、アクションを無効化します。
        if (_moveAction != null)
        {
            _moveAction.performed -= HandleMoveInput;
            _moveAction.canceled -= HandleMoveInput;
            _moveAction.Disable();
        }
    }

    /// <summary>
    /// 移動入力アクションのコールバックを処理します。
    /// </summary>
    /// <param name="context">入力コンテキスト。</param>
    private void HandleMoveInput(InputAction.CallbackContext context)
    {
        // 入力値を読み込み、現在の移動入力として保存します。
        _currentMoveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 現在の移動入力を取得します。
    /// </summary>
    /// <returns>現在の移動入力ベクトル。</returns>
    public Vector2 GetMoveInput()
    {
        return _currentMoveInput;
    }
}