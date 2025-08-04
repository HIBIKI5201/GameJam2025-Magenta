using UnityEngine;

public class HomingBulletController : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20;
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private float _damage = 1;
    [SerializeField]
    private float _lifeTime = 5f;

    private Transform _owner;
    private Transform _target;

    public void Init(Transform self, Transform target)
    {
        _owner = self;
        _target = target;

        Destroy(gameObject, _lifeTime); // 指定時間後に弾丸を破棄
    }

    void Update()
    {
        if (_target == null) return;

        // 対象への方向ベクトル
        Vector2 direction = (_target.position - transform.position).normalized;

        // 現在の角度と目標角度を計算
        float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 現在の角度
        float currentAngle = transform.eulerAngles.z;

        // 補間して回転
        float angle = Mathf.MoveTowardsAngle(currentAngle, angleToTarget, _rotateSpeed * Time.deltaTime);

        // 回転を適用（Z軸回転のみ）
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // right方向（＝ローカルX軸）に移動
        transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody.TryGetComponent(out Player_Main_System player)) return;

        if (player.transform == _owner)
        {
            return; // 自分自身との衝突は無視
        }

        player.TakeDamage(_damage); // ダメージを与える
        Destroy(gameObject); // 弾丸を破棄
    }
}
