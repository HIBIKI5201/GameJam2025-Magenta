using UnityEngine;

public class SurroundBulletController : MonoBehaviour
{
    [Header("弾丸速度")]
    [SerializeField] float _speed = 10f;
    [SerializeField]
    private float _damage = 1;

    Vector2 _direction = Vector2.up;

    private Transform _owner;

    public void Init(Transform transform)
    {
        _owner = transform;
    }
    public void SetDirection(Vector2 dir)
    {
        _direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody.TryGetComponent(out Player_Main_System player)) return;

        if (player.transform == _owner)
        {
            return; // 自分自身との衝突は無視
        }

        player.TakeDamage(_damage); // ダメージを与える
    }
}

