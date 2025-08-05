using SymphonyFrameWork.System;
using UnityEngine;

/// <summary>
/// ホーミング弾の挙動を制御します。
/// </summary>
public class HomingBulletController : MonoBehaviour
{
    // --- シリアライズされたフィールド ---
    [Header("ホーミング弾の回転速度")]
    [SerializeField] private float _rotationSpeed = 20f;

    [Header("ホーミング弾の移動速度")]
    [SerializeField] private float _movementSpeed = 1f;

    [Header("ホーミング弾が与えるダメージ量")]
    [SerializeField] private float _damageAmount = 1f;

    [Header("ホーミング弾の生存時間")]
    [SerializeField] private float _lifetime = 5f;

    [SerializeField]
    private AudioClip _shootSound;
    // --- privateフィールド ---
    private Transform _owner;
    private Transform _target;

    /// <summary>
    /// ホーミング弾を初期化します。
    /// </summary>
    /// <param name="ownerTransform">この弾を発射したオブジェクトのTransform。</param>
    /// <param name="targetTransform">ホーミング対象のオブジェクトのTransform。</param>
    public void Initialize(Transform ownerTransform, Transform targetTransform)
    {
        _owner = ownerTransform;
        _target = targetTransform;

        AudioManager.GetAudioSource(AudioGroupTypeEnum.SE.ToString()).PlayOneShot(_shootSound);

        // 指定時間後に弾を破棄します。
        Destroy(gameObject, _lifetime);
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。毎フレーム呼び出されます。
    /// </summary>
    private void Update()
    {
        // ターゲットが設定されていない場合は処理を中断します。
        if (_target == null) return;

        // ターゲットへの方向ベクトルを計算します。
        Vector2 directionToTarget = (_target.position - transform.position).normalized;

        // 現在の角度とターゲットへの目標角度を計算します。
        float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // 現在のZ軸の回転角度を取得します。
        float currentAngleZ = transform.eulerAngles.z;

        // 目標角度へスムーズに回転させます。
        float interpolatedAngle = Mathf.MoveTowardsAngle(currentAngleZ, angleToTarget, _rotationSpeed * Time.deltaTime);

        // 計算した回転を適用します（Z軸回転のみ）。
        transform.rotation = Quaternion.Euler(0f, 0f, interpolatedAngle);

        // オブジェクトの右方向（ローカルX軸）に移動させます。
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。他のColliderと接触した時に呼び出されます。
    /// </summary>
    /// <param name="other">接触したCollider。</param>
    private void OnTriggerEnter(Collider other)
    {
        // 衝突相手にRigidbodyがアタッチされているか、かつPlayer_Main_Systemコンポーネントを持っているかを確認します。
        if (other.attachedRigidbody == null || !other.attachedRigidbody.TryGetComponent(out Player_Main_System player))
        {
            return;
        }

        // 弾の所有者と衝突したプレイヤーが同じであれば、衝突を無視します。
        if (player.transform == _owner)
        {
            return;
        }

        // プレイヤーにダメージを与えます。
        player.TakeDamage(_damageAmount);
        // 弾を破棄します。
        Destroy(gameObject);
    }
}