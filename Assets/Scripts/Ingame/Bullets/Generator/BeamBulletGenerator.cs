using System;
using UnityEngine;

/// <summary>
/// ビーム弾を生成するクラスです。
/// </summary>
[Serializable]
public class BeamBulletGenerator : IBulletGenerator
{
    // --- プロパティ ---
    /// <summary>
    /// この弾ジェネレーター使用時のプレイヤーの移動速度倍率を取得します。
    /// </summary>
    public float MoveSpeedScale => _movementSpeedScale;

    // --- シリアライズされたフィールド ---
    [Header("ビーム弾のプレハブ")]
    [SerializeField] private BeamBulletController _beamBulletPrefab;

    [Header("ビーム弾の生成間隔")]
    [SerializeField] private float _shootInterval = 0.1f;

    [Header("この弾ジェネレーター使用時のプレイヤーの移動速度倍率")]
    [SerializeField] private float _movementSpeedScale = 1f;

    // --- privateフィールド ---
    private Transform _ownerTransform;
    private Transform _targetTransform;
    private Transform _rootTransform;
    private float _shootTimer;

    /// <summary>
    /// 弾ジェネレーターを初期化します。
    /// </summary>
    /// <param name="ownerTransform">弾を発射するオブジェクトのTransform。</param>
    /// <param name="targetTransform">ターゲットとなるオブジェクトのTransform。</param>
    public void Initialize(Transform ownerTransform, Transform targetTransform, Transform root)
    {
        _ownerTransform = ownerTransform;
        _targetTransform = targetTransform;
        _rootTransform = root;
    }

    /// <summary>
    /// 弾ジェネレーターの更新処理です。毎フレーム呼び出されます。
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間。</param>
    public void Update(float deltaTime)
    {
        // 発射タイマーを更新します。
        _shootTimer += deltaTime;

        // タイマーが生成間隔を超えたらビーム弾を生成します。
        if (_shootTimer >= _shootInterval)
        {
            GenerateBullet();
            _shootTimer = 0f; // タイマーをリセットします。
        }
    }

    /// <summary>
    /// ビーム弾を生成し、初期設定を行います。
    /// </summary>
    private void GenerateBullet()
    {
        // ビーム弾を生成します。
        BeamBulletController bullet = GameObject.Instantiate(_beamBulletPrefab, _ownerTransform.position, Quaternion.identity);
        
        // ターゲットへの方向を計算し、ビームの向きを設定します。
        Vector3 directionToTarget = (_targetTransform.position - _ownerTransform.position).normalized;
        bullet.transform.right = directionToTarget;
        
        // ビーム弾を初期化します。
        bullet.Initialize(_ownerTransform);
        // ビーム弾をルートの子として設定します。
        bullet.transform.SetParent(_rootTransform, false);
    }
}