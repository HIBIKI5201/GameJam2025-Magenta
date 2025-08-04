using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// テスト用の弾を管理するクラス
/// </summary>
public class TestBullet : MonoBehaviour
{
    // 発射元のタグ
    [SerializeField] private string _shot_Side_Tag;

    /// <summary>
    /// 発射元のタグを設定する
    /// </summary>
    /// <param name="shot_Side_Tag">発射元のタグ</param>
    public void SetShotSideTag(string shot_Side_Tag)
    {
        _shot_Side_Tag = shot_Side_Tag;
    }

    /// <summary>
    /// トリガーに接触した際の処理
    /// </summary>
    /// <param name="other">衝突したコライダー</param>
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー1が自身の弾に当たった場合は処理しない
        if((other.tag == "Player1") && "Bullet1" == tag)
        {
            return;
        }
        // プレイヤー2が自身の弾に当たった場合は処理しない
        if((other.tag == "Player2") && "Bullet2" == tag)
        {
            return;
        }
        // 自身を破棄
        Destroy(this.gameObject);
    }
}