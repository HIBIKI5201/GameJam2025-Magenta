using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの入力を制御するクラス
/// </summary>
public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    private string _moveInputName; // 移動入力アクション名

    // 移動アクション
    private InputAction _moveAction;

    // 移動入力
    private Vector2 _move_Input;

    /// <summary>
    /// アクションを設定する
    /// </summary>
    /// <param name="input">プレイヤーインプット</param>
    /// <param name="moveInputName">移動入力アクション名</param>
    public void Init(PlayerInput input)
    {
        // 入力がnullの場合は処理しない
        if (input == null) return;
        // アクション名がnullまたは空の場合はエラーログを出力
        if (string.IsNullOrEmpty(_moveInputName))
        {
            Debug.LogError("Move Input Action Name is Null");
            return;
        }

        // アクションアセットを取得
        InputActionAsset actions = input.actions;

        // 移動アクションを取得
        _moveAction = actions[_moveInputName];

        // アクションにコールバックを登録
        _moveAction.performed += SetMove;
        _moveAction.canceled += SetMove;
    }

    /// <summary>
    /// 移動入力を設定する
    /// </summary>
    /// <param name="context">入力コンテキスト</param>
    private void SetMove(InputAction.CallbackContext context)
    {
        _move_Input = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 移動入力を取得する
    /// </summary>
    /// <returns>移動入力</returns>
    public Vector2 GetMove()
    {
        return _move_Input;
    }
}