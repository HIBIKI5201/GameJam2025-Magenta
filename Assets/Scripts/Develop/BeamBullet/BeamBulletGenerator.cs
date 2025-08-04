using System.ComponentModel;
using UnityEngine;

/// <summary>
/// ビーム弾を生成するクラス
/// </summary>
public class BeamBulletGenerator : IBulletGenerator
{
	// プレイヤーのTransform
	Transform _playerTransform;
	// 生成するビーム弾のプレハブ
	[SerializeField] GameObject _bullet;
	
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public BeamBulletGenerator()
	{
		
	}

	/// <summary>
	/// 毎フレームの更新処理
	/// </summary>
	/// <param name="deltaTime">前フレームからの経過時間</param>
	public void Update(float deltaTime)
	{
		// プレイヤーの位置にビーム弾を生成
		Object.Instantiate(_bullet, _playerTransform);
	}
}