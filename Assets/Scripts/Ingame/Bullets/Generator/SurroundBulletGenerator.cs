using System;
using UnityEngine;

/// <summary>
/// 周囲に展開する弾を生成するクラスです。
/// </summary>
[Serializable]
public class SurroundBulletGenerator : IBulletGenerator
{
    // --- プロパティ ---
    /// <summary>
    /// この弾ジェネレーター使用時のプレイヤーの移動速度倍率を取得します。
    /// </summary>
    public float MoveSpeedScale => _movementSpeedScale;

    // --- シリアライズされたフィールド ---
    [Header("周囲に展開する弾のプレハブ")]
    [SerializeField] private SurroundBulletController _surroundBulletPrefab;

    [Header("弾の発射間隔")]
    [SerializeField, Min(0.1f)] private float _shootInterval = 1f;

    [Header("一度に生成する弾の数")]
    [SerializeField, Min(3)] private int _numberOfBullets = 3;

    [Header("弾が生成される中心からの距離")]
    [SerializeField, Min(0.1f)] private float _spawnDistance = 0.1f;

    [Header("弾の生存時間")]
    [SerializeField] private float _bulletLifetime = 1.5f;

    [Header("この弾ジェネレーター使用時のプレイヤーの移動速度倍率")]
    [SerializeField] private float _movementSpeedScale = 1f;

    // --- privateフィールド ---
    private Transform _ownerTransform;
    private float _shootTimer;

    /// <summary>
    /// 弾ジェネレーターを初期化します。
    /// </summary>
    /// <param name="ownerTransform">弾を発射するオブジェクトのTransform。</param>
    /// <param name="targetTransform">ホーミングのターゲットとなるオブジェクトのTransform（このジェネレーターでは使用しません）。</param>
    public void Initialize(Transform ownerTransform, Transform targetTransform)
    {
        _ownerTransform = ownerTransform;
    }

    /// <summary>
    /// 弾ジェネレーターの更新処理です。毎フレーム呼び出されます。
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間。</param>
    public void Update(float deltaTime)
    {
        // 発射タイマーを更新します。
        _shootTimer += deltaTime;

        // 発射間隔に達したら弾を発射します。
        if (_shootTimer >= _shootInterval)
        {
            GenerateBullets();
            _shootTimer = 0f; // タイマーをリセットします。
        }
    }

    /// <summary>
    /// 周囲に弾を生成し、初期設定を行います。
    /// </summary>
    private void GenerateBullets()
    {
        // 指定された数の弾を生成します。
        for (int i = 0; i < _numberOfBullets; i++)
        {
            // 弾の角度を計算します。
            float angle = ((2f * Mathf.PI) / _numberOfBullets) * i;
            // 中心からのオフセット位置を計算します。
            Vector3 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _spawnDistance;
            // 弾の生成位置を計算します。
            Vector3 instancePosition = _ownerTransform.position + offset;

            // 弾を生成します。
            SurroundBulletController bullet = GameObject.Instantiate(_surroundBulletPrefab, instancePosition, Quaternion.identity);
            // 弾を初期化します。
            bullet.Initialize(_ownerTransform);

            // 弾の移動方向を設定します。
            Vector3 direction = (instancePosition - _ownerTransform.position).normalized;
            bullet.SetMoveDirection(direction);

            // 弾の生存時間を設定し、自動的に破棄されるようにします。
            GameObject.Destroy(bullet.gameObject, _bulletLifetime);
        }
    }
}