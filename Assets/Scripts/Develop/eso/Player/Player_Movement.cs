using SymphonyFrameWork.Attribute;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// プレイヤーの移動を管理するクラス
/// </summary>
public class Player_Movement : MonoBehaviour
{
    // 移動入力
    private Func<Vector2> Move_Input;
    // 移動速度
    [SerializeField] private float _move_Speed;
    // Rigidbody
    [SerializeField] private Rigidbody _rigidbody;
    // 移動可能範囲
    [SerializeField, ReadOnly] private Vector3 _movement_Area;
    // 移動可能範囲の中心
    private Vector3 _movement_Pos;

    /// <summary>
    /// アクションを設定する
    /// </summary>
    /// <param name="func">移動入力を返す関数</param>
    public void SetAction(Func<Vector2> func)
    {
        Move_Input = func;
    }

    /// <summary>
    /// 移動可能範囲を設定する
    /// </summary>
    /// <param name="pos">中心座標</param>
    /// <param name="area">範囲</param>
    public void SetMovementArea(Vector3 pos, Vector3 area)
    {
        _movement_Area = area;
        _movement_Pos = pos;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        // 移動処理
        Movement();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Movement()
    {
        // 入力から移動ベクトルを計算
        Vector3 vec = new Vector3(Move_Input.Invoke().x, Move_Input.Invoke().y, 0) * Time.deltaTime;

        // 移動
        transform.Translate(vec * _move_Speed,Space.Self);

        // 移動範囲制限
        transform.localPosition = new Vector2(
            Mathf.Clamp(transform.localPosition.x, -_movement_Area.x, _movement_Area.x),
            Mathf.Clamp(transform.localPosition.y, -_movement_Area.y, _movement_Area.y)
            );
    }
}