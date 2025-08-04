using UnityEngine;

public class SurroundBulletGenerator : IBullet
{
    Transform _player1Transform;
    GameObject _bulletPrefab;
    int _bulletCount;
    float _dist;

    public SurroundBulletGenerator(Transform player1Transform, GameObject bulletPrefab, int bulletCount, float dist)
    {
        _player1Transform = player1Transform;
        _bulletPrefab = bulletPrefab;
        _bulletCount = bulletCount;
        _dist = dist;
    }
    public void Shoot()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            float angle = ((2 * Mathf.PI) / _bulletCount) * i;
            Vector3 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _dist;
            Vector3 instancePos = _player1Transform.position + offset;

            GameObject bullet = GameObject.Instantiate(_bulletPrefab, instancePos, Quaternion.identity);

            Vector3 direction = (instancePos - _player1Transform.position).normalized;

            SurroundBullet bulletScript = bullet.GetComponent<SurroundBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }
            GameObject.Destroy(bullet, 1.5f);
        }
    }
    public void Update(float deltaTime)
    {
        
    }
}