using System;
using UnityEngine;

[Serializable]
public class SurroundBulletGenerator : IBulletGenerator
{
    private Transform _self;
    private LayerMask _targetLayerMask;

    [SerializeField]
    SurroundBullet _bulletPrefab;

    [SerializeField, Min(0.1f)]
    float _interval = 1f;

    [SerializeField, Min(3)]
    int _bulletCount = 3;

    [SerializeField, Min(0.1f)]
    float _dist = 0.1f;

    [SerializeField]
    private float _lifeTime = 1.5f;

    float _timer;

    public void Init(Transform self, Transform target)
    {
        _self = self;
    }

    public void Shoot()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            float angle = ((2 * Mathf.PI) / _bulletCount) * i;
            Vector3 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _dist;
            Vector3 instancePos = _self.position + offset;

            SurroundBullet bullet = GameObject.Instantiate(_bulletPrefab, instancePos, Quaternion.identity);
            bullet.Init(_self);

            Vector3 direction = (instancePos - _self.position).normalized;
            bullet.SetDirection(direction);
            if (bullet.TryGetComponent(out Rigidbody rb))
            {
                rb.includeLayers = _targetLayerMask;
            }
            GameObject.Destroy(bullet.gameObject, _lifeTime);
        }
    }
    public void Update(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= _interval)
        {
            Shoot();
            _timer = 0f; // リセット
        }
    }
}