using System;
using UnityEngine;

[Serializable]
public class HomingBulletGenerator : IBulletGenerator
{
    public float MoveSpeedScale => _moveSpeed;

    [SerializeField]
    private HomingBulletController _bulletPrefab;
    [SerializeField]
    private float _interval = 0.5f;

    [SerializeField]
    private float _moveSpeed = 1f;

    private Transform _self;
    private Transform _target;

    private float _timer;
    public void Init(Transform self, Transform target)
    {
        _self = self;
        _target = target;
    }

    public void Update(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= _interval)
        {
            _timer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullet = UnityEngine.Object.Instantiate(_bulletPrefab, _self.position, Quaternion.identity);
        bullet.transform.right = (_target.position - _self.position).normalized;
        bullet.Init(_self, _target);
    }
}
