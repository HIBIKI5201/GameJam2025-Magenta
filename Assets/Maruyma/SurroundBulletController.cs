using UnityEngine;
using UnityEngine.InputSystem;

public class SurroundBulletController : MonoBehaviour
{
    SurroundBulletGenerator _bulletGenerator;
    [SerializeField] GameObject _bulletPrefab;
    [Header("発射間隔(秒)")]
    [SerializeField] float _interval = 1f;
    float _timer;
    [Header("一度の弾数")]
    [SerializeField] int _bulletCount = 16;
    [Header("弾丸の中心との距離")]
    [SerializeField] float _dist = 1f;

    void Start()
    {
        _bulletGenerator = new SurroundBulletGenerator(transform, _bulletPrefab, _bulletCount, _dist);
        _timer = _interval; // 最初は弾が出ない
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _bulletGenerator.Shoot();
            _timer = _interval;
        }
    }
}
