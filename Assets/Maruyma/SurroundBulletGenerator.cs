using UnityEngine;

public class SurroundBulletGenerator : IBullet
{
    Transform _player1Transform;
    GameObject _bulletPrefab;
    [SerializeField] int _bulletCount = 15; // 一回の弾の数
    float _dist = 1f; // 弾丸と中心の距離

    public SurroundBulletGenerator(Transform player1Transform, GameObject bulletPrefab)
    {
        _player1Transform = player1Transform;
        _bulletPrefab = bulletPrefab;
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

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }
            GameObject.Destroy(bullet, 2f);
        }
    }
    public void Update(float deltaTime)
    {
        
    }
}