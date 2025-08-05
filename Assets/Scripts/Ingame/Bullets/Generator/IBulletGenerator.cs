using System;
using UnityEngine;

/// <summary>
/// 弾を生成するジェネレーターのインターフェースです。
/// </summary>
public interface IBulletGenerator
{
    /// <summary>
    /// この弾ジェネレーター使用時のプレイヤーの移動速度倍率を取得します。
    /// </summary>
    public float MoveSpeedScale { get; }

    /// <summary>
    /// 弾ジェネレーターを初期化します。
    /// </summary>
    /// <param name="ownerTransform">弾を発射するオブジェクトのTransform。</param>
    /// <param name="targetTransform">ターゲットとなるオブジェクトのTransform。</param>
    public void Initialize(Transform ownerTransform, Transform targetTransform, Transform root);

    public void SetSelected(bool active);

    /// <summary>
    /// 弾ジェネレーターの更新処理です。毎フレーム呼び出されます。
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間。</param>
    public void Update(float deltaTime);
}