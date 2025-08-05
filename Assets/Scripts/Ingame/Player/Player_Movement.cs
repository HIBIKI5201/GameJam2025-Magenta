using SymphonyFrameWork.Attribute;
using System;
using UnityEngine;

/// <summary>
/// プレイヤーの移動を管理します。
/// </summary>
public class Player_Movement : MonoBehaviour
{
    // --- privateフィールド ---
    private Func<Vector2> _moveInputFunction;
    private float _currentMoveSpeed;

    // --- シリアライズされたフィールド ---
    [Header("プレイヤーデータ")]
    [SerializeField] private PlayerData _playerData;

    [Header("Rigidbodyコンポーネント")]
    [SerializeField] private Rigidbody _playerRigidbody;

    [Header("移動可能範囲の大きさ")]
    [SerializeField, ReadOnly] private Vector3 _movementArea;

    [Header("移動可能範囲の中心となるTransform")]
    [SerializeField] private Transform _movementAreaTransform;

    [SerializeField]
    private PlayerAvatarManager _character;
    private Transform _target;
    private bool _isFlip;

    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private void Start()
    {
        // 初期移動速度を設定します。
        _currentMoveSpeed = _playerData.PlayerMoveSpeed;
    }

    /// <summary>
    /// 移動入力を提供する関数を設定します。
    /// </summary>
    /// <param name="inputFunction">移動入力を返す関数。</param>
    public void SetMoveInputFunction(Func<Vector2> inputFunction)
    {
        _moveInputFunction = inputFunction;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    /// <summary>
    /// プレイヤーの移動可能範囲を設定します。
    /// </summary>
    /// <param name="areaTransform">移動可能範囲の中心となるTransform。</param>
    /// <param name="areaSize">移動可能範囲の大きさ。</param>
    public void SetMovementBounds(Transform areaTransform, Vector3 areaSize)
    {
        _movementArea = areaSize;
        _movementAreaTransform = areaTransform;
    }

    /// <summary>
    /// 移動速度に倍率を適用します。
    /// </summary>
    /// <param name="scale">適用する倍率。</param>
    public void ApplyMoveSpeedScale(float scale)
    {
        // プレイヤーデータが設定されていない場合は処理を中断します。
        if (_playerData == null) return;

        // 基本移動速度に倍率を適用します。
        _currentMoveSpeed = _playerData.PlayerMoveSpeed * scale;
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。毎フレーム呼び出されます。
    /// </summary>
    private void Update()
    {
        // プレイヤーの移動処理を更新します。
        UpdateMovement();
        UpdateRotate();
    }

    /// <summary>
    /// プレイヤーの移動処理を実行します。
    /// </summary>
    private void UpdateMovement()
    {
        // 入力から移動ベクトルを計算します。
        Vector3 moveVector = new Vector3(_moveInputFunction.Invoke().x, _moveInputFunction.Invoke().y, 0f) * Time.deltaTime;

        // プレイヤーを移動させます。
        transform.Translate(moveVector * _currentMoveSpeed, Space.Self);

        // 移動可能範囲内でプレイヤーのワールド座標を制限します。
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x,
                _movementAreaTransform.position.x - _movementArea.x,
                _movementAreaTransform.position.x + _movementArea.x),
            Mathf.Clamp(transform.position.y,
                _movementAreaTransform.position.y - _movementArea.y,
                _movementAreaTransform.position.y + _movementArea.y),
            transform.position.z // Z座標は変更しません。
        );
    }

    private void UpdateRotate()
    {
        if (_character == null) return;

        Vector3 dir = (_target.position - transform.position).normalized;
        bool isFlip = dir.x <= 0;

        if (isFlip == _isFlip) return;

        _character.FlipX(isFlip);
        _isFlip = isFlip;
    }
}