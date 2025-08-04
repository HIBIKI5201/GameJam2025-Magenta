using UnityEngine;
using UnityEngine.InputSystem;

public class SurroundBulletController : MonoBehaviour
{
    SurroundBulletGenerator _bulletGenerator;
    [SerializeField] GameObject _bulletPrefab;

    void Start()
    {
        _bulletGenerator = new SurroundBulletGenerator(transform, _bulletPrefab);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // 左クリック（仮）
        {
            _bulletGenerator.Shoot();
        }
    }
}
