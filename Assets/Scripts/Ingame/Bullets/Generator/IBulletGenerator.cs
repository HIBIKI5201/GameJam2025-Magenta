using UnityEngine;

/// <summary>
/// 弾を発射するジェネレーターのインターフェース
/// </summary>
interface IBulletGenerator
{
    public float MoveSpeedScale { get; }

    public void Init(Transform self, Transform target);

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間</param>
    public void Update(float deltaTime);
}