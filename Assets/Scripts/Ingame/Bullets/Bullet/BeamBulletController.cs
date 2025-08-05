using SymphonyFrameWork.System;
using System.Collections;
using UnityEngine;

/// <summary>
/// ビーム弾の挙動を制御します。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
public class BeamBulletController : MonoBehaviour
{
    // --- 定数 ---
    private const float PIVOT_OFFSET_FACTOR = 2f; // スプライトのピボットが中央にある場合のオフセット係数。

    // --- シリアライズされたフィールド ---
    [Header("ビームの縦の当たり判定の大きさ")]
    [SerializeField] private float _verticalRange = 10f;

    [Header("ビームが横に伸びきるまでにかかる時間")]
    [SerializeField, Range(0.1f, 10f)] private float _launchTime = 1f;

    [Header("ビームが発射されてから縮み始めるまでの時間")]
    [SerializeField] private float _firingTime = 2f;

    [Header("ビームの横の最大長")]
    [SerializeField] private float _horizonRange = 200f;

    [Header("ビームが与えるダメージ量")]
    [SerializeField] private float _damage = 1f;

    [SerializeField]
    private AudioClip _chargeSound;
    [SerializeField]
    private AudioClip _shootSound;
    // --- privateフィールド ---
    private Vector3 _initialPosition;
    private Transform _owner;
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// このビーム弾を初期化します。
    /// </summary>
    /// <param name="ownerTransform">このビームを発射したオブジェクトのTransform。</param>
    public void Initialize(Transform ownerTransform)
    {
        _owner = ownerTransform;
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private void Start()
    {
        // 必要なコンポーネントを取得します。
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // 初期位置を保存します。
        _initialPosition = transform.position;

        // ビームの伸縮シーケンスを開始します。
        StartCoroutine(ScaleSequence());
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。他のColliderと接触した時に呼び出されます。
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに衝突した場合の処理。
        if (other.attachedRigidbody?.TryGetComponent(out Player_Main_System player) ?? false)
        {
            // 自分自身の弾であれば、処理を中断します。
            if (player.transform == _owner)
            {
                return;
            }

            // 相手にダメージを与えます。
            player.TakeDamage(_damage);
            // 自身を破棄します。
            Destroy(gameObject);
        }

        // 他の弾に衝突した場合の処理。
        if (other.TryGetComponent(out SurroundBulletController bullet))
        {
            // 相手の弾を破棄します。
            Destroy(bullet.gameObject);
        }
    }

    /// <summary>
    /// ビームの伸縮を制御するコルーチンです。
    /// </summary>
    private IEnumerator ScaleSequence()
    {
        AudioManager.GetAudioSource(AudioGroupTypeEnum.SE.ToString()).PlayOneShot(_chargeSound);
        // 最初にY軸（縦）方向にビームを伸ばします。
        yield return TimeToScale(1f, new Vector3(transform.localScale.x, _verticalRange, 1f));

        AudioManager.GetAudioSource(AudioGroupTypeEnum.SE.ToString()).PlayOneShot(_shootSound);
        // 次にX軸（横）方向にビームを伸ばします。
        yield return TimeToScale(_launchTime, new Vector3(_horizonRange, transform.localScale.y, 1f));
        // 発射状態を一定時間維持します。
        yield return new WaitForSeconds(_firingTime);
        // 最後にY軸（縦）方向を縮めて消滅させます。
        yield return TimeToScale(1f, new Vector3(transform.localScale.x, 0.1f, 1f));

        // 自身を破棄します。
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 指定された時間で指定されたスケールに変化させるコルーチンです。
    /// </summary>
    /// <param name="changeTime">スケール変更にかかる時間。</param>
    /// <param name="endScale">目標のスケール。</param>
    private IEnumerator TimeToScale(float changeTime, Vector3 endScale)
    {
        // スケール変更前の状態を記録します。
        Vector3 startScale = transform.localScale;
        float timer = 0f;

        // 指定された時間になるまでスケールを変化させ続けます。
        while (timer < changeTime)
        {
            // スケール変更前のワールド幅を記録します。
            float previousWidth = _spriteRenderer.bounds.size.x;

            // 経過時間を加算し、進捗割合（0-1）を計算します。
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / changeTime);

            // 線形補間を使ってスムーズにスケールを変化させます。
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // スケール変更後のワールド幅を取得します。
            float currentWidth = _spriteRenderer.bounds.size.x;

            // ワールド空間での幅の増加量を計算します。
            float widthDelta = currentWidth - previousWidth;

            // X軸が伸びた（幅が増加した）場合、ビームの基点がずれないように位置を補正します。
            // 横幅増加に合わせて座標を移動させる処理
            transform.position = _initialPosition + transform.right * (transform.localScale.x / PIVOT_OFFSET_FACTOR);

            yield return null;
        }

        // 最終的なスケールを正確に設定します。
        transform.localScale = endScale;

        // 一定時間後にビーム弾を破棄します。
        Destroy(gameObject, 1.5f);
    }
}