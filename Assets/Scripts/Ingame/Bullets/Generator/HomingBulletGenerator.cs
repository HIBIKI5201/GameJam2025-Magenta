using System;
using UnityEngine;

/// <summary>
/// ホーミング弾を生成するクラスです。
/// </summary>
[Serializable]
public class HomingBulletGenerator : IBulletGenerator
{
    public event Action<float> OnIntervalElapsed;

    // --- プロパティ ---
    /// <summary>
    /// この弾ジェネレーター使用時のプレイヤーの移動速度倍率を取得します。
    /// </summary>
    public float MoveSpeedScale => _movementSpeedScale;

    // --- シリアライズされたフィールド ---
    [Header("ホーミング弾のプレハブ")]
    [SerializeField] private HomingBulletController _homingBulletPrefab;

    [Header("弾の発射間隔")]
    [SerializeField] private float _shootInterval = 0.5f;

    [Header("この弾ジェネレーター使用時のプレイヤーの移動速度倍率")]
    [SerializeField] private float _movementSpeedScale = 1f;

    [SerializeField]
    private SelectBulletManager _ui;
    [SerializeField]
    private int _index;

    // --- privateフィールド ---
    private Transform _ownerTransform;
    private Transform _targetTransform;
    private Transform _rootTransform;
    private float _shootTimer;

    /// <summary>
    /// 弾ジェネレーターを初期化します。
    /// </summary>
    /// <param name="ownerTransform">弾を発射するオブジェクトのTransform。</param>
    /// <param name="targetTransform">ホーミングのターゲットとなるオブジェクトのTransform。</param>
    public void Initialize(Transform ownerTransform, Transform targetTransform, Transform root)
    {
        _ownerTransform = ownerTransform;
        _targetTransform = targetTransform;
        _rootTransform = root;

        // UIの初期化
        OnIntervalElapsed += n => _ui[_index].Guage.fillAmount = n;
    }

    public void SetSelected(bool active)
    {
        // UIの選択状態を更新します。
        _ui.SetHighLight(_index, active);

        if (!active)
        {
            _shootTimer = 0f; // 選択解除時にタイマーをリセットします。
            OnIntervalElapsed?.Invoke(1f); // UIのゲージをリセットします。
        }
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
            _shootTimer = 0f;
            GenerateBullet();
        }

        // 発射間隔が経過したことを通知します。
        OnIntervalElapsed?.Invoke(1 - _shootTimer / _shootInterval);
    }

    /// <summary>
    /// ホーミング弾を生成し、初期設定を行います。
    /// </summary>
    private void GenerateBullet()
    {
        // 弾を生成します。
        HomingBulletController bullet = UnityEngine.Object.Instantiate(_homingBulletPrefab, _ownerTransform.position, Quaternion.identity);

        // 弾の向きをターゲットに向けます。
        bullet.transform.right = (_targetTransform.position - _ownerTransform.position).normalized;
        // 弾を初期化します。
        bullet.Initialize(_ownerTransform, _targetTransform);
        // ルートオブジェクトの子として設定します。
        bullet.transform.SetParent(_rootTransform, false);
    }

    private void UpdateUI(float value)
    {
        _ui[_index].Guage.fillAmount = value;
    }

}