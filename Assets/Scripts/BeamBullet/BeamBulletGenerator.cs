using System.ComponentModel;
using UnityEngine;

public class BeamBulletGenerator : IBulletGenerator
{
	Transform _playerTransform;
	[SerializeField] GameObject _bullet;
	
	public BeamBulletGenerator()
	{
		
	}

	public void Update(float deltaTime)
	{
		//自機の少しずらしたところに生成
		Object.Instantiate(_bullet, _playerTransform);
	}
}
