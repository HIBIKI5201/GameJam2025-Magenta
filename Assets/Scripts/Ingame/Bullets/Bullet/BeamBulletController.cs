using System.Collections;
using UnityEngine;


/// <summary>
/// ビーム弾の挙動を制御するクラス
/// </summary>
public class BeamBulletController : MonoBehaviour
{
	[Header("ビームの縦当たり判定")]
	[SerializeField] float _verticalRange = 10f;

	[Header("ビームの横に伸びきる時間")]
	[SerializeField, Range(1f, 10f)] float _launchTime = 1f;

	[Header("ビームが縮むまでの時間")]
	[SerializeField] float _firingTime = 2f;

	[Header("ビームの横の長さ")]
	[SerializeField] float _horizonRange = 200f;

	/// <summary>
	/// 初期化処理
	/// </summary>
	void Start()
    {
		// ビームの伸縮シーケンスを開始
		StartCoroutine(ScaleSequence());
	}

	/// <summary>
	/// ビームの伸縮を制御するコルーチン
	/// </summary>
	IEnumerator ScaleSequence()
	{
		// Y軸にビームを伸ばす
		yield return TimeToScale(1f, new Vector3(transform.localScale.x, _verticalRange, 1f));
		// X軸にビームを伸ばす（中心を動かしながら）
		yield return TimeToScale(_launchTime, new Vector3(_horizonRange, transform.localScale.y, 1f));
		// ビームのY軸縮小を待つ前に firingTime 分待機
		yield return new WaitForSeconds(_firingTime);
		// Y軸を縮める
		yield return TimeToScale(1f, new Vector3(transform.localScale.x, 0.1f, 1f));

		// 自身を破棄
		Destroy(this.gameObject);
	}

	/// <summary>
	/// 指定された時間で指定されたスケールに変化させるコルーチン
	/// </summary>
	/// <param name="changeTime">変化にかかる時間</param>
	/// <param name="endScale">目標のスケール</param>
	IEnumerator TimeToScale(float changeTime, Vector3 endScale)
	{
		//初期の大きさを記憶
		Vector3 startScale = transform.localScale;
		float timer = 0f;

		//while文(その大きさになったら抜ける)
		while (timer < changeTime)
		{
			timer += Time.deltaTime;
			float t = Mathf.Clamp01(timer / changeTime);

			// 線形補間でスケールを変化
			transform.localScale = Vector3.Lerp(startScale, endScale, t);

			//X軸が伸びる場合、Positionも動かさないといけない
			float scaleDeltaX = transform.localScale.x - startScale.x;

			if (scaleDeltaX > 0.1f)
			{
				// 右方向にだけ伸ばす
				transform.position = new Vector3(scaleDeltaX / 16f, 0f, 0f);
			}

			yield return null;
		}

		// 最終的なスケールに設定
		transform.localScale = endScale;
	}
}