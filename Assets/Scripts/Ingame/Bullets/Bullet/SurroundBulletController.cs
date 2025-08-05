using SymphonyFrameWork.System;
using UnityEngine;

/// <summary>
/// 周囲に展開する弾の挙動を制御します。
/// </summary>
public class SurroundBulletController : MonoBehaviour
{
    // --- シリアライズされたフィールド ---
    [Header("弾丸の移動速度")]
    [SerializeField] private float _movementSpeed = 10f;

    [Header("弾丸が与えるダメージ量")]
    [SerializeField] private float _damageAmount = 1f;

    [SerializeField]
    private AudioClip _shootSound;
    // --- privateフィールド ---
    private Vector2 _moveDirection = Vector2.up;
    private Transform _owner;

    /// <summary>
    /// 弾丸を初期化します。
    /// </summary>
    /// <param name="ownerTransform">この弾を発射したオブジェクトのTransform。</param>
    public void Initialize(Transform ownerTransform)
    {
        _owner = ownerTransform;

        AudioManager.GetAudioSource(AudioGroupTypeEnum.SE.ToString()).PlayOneShot(_shootSound);
    }

    /// <summary>
    /// 弾丸の移動方向を設定します。
    /// </summary>
    /// <param name="direction">移動方向のベクトル。</param>
    public void SetMoveDirection(Vector2 direction)
    {
        _moveDirection = direction.normalized;
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。毎フレーム呼び出されます。
    /// </summary>
    private void Update()
    {
        // 設定された方向に弾丸を移動させます。
        transform.Translate(_moveDirection * _movementSpeed * Time.deltaTime);
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
    }
}