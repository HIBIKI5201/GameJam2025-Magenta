using System;
using UnityEngine;

/// <summary>
/// ビーム弾を生成するクラス
/// </summary>
[Serializable]
public class BeamBulletGenerator : IBulletGenerator
{
	// プレイヤーのTransform
	Transform _self;
    Transform _target;
    // 生成するビーム弾のプレハブ
    [SerializeField] BeamBulletController _bullet;

    [SerializeField]
    float _interval = 0.1f; // ビーム弾の生成間隔

    float _timer; // タイマー

    public void Init(Transform self, Transform target)
    {
        // プレイヤーのTransformを設定
        _self = self;
        // ターゲットのTransformを設定
        _target = target;
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間</param>
    public void Update(float deltaTime)
	{
        _timer += deltaTime;
        // タイマーが生成間隔を超えたらビーム弾を生成
        if (_timer >= _interval)
        {
            Shoot();
            _timer = 0f; // タイマーをリセット
        }
    }

    private void Shoot()
    {
        // ビーム弾を生成
        BeamBulletController bullet = GameObject.Instantiate(_bullet, _self.position, Quaternion.identity);
        // ターゲットの方向に向ける
        Vector3 direction = (_target.position - _self.position).normalized;
        bullet.transform.right = direction;
    }
}