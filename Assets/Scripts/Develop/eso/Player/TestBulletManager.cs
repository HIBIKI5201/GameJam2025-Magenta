using UnityEngine;

/// <summary>
/// テスト用の弾を管理するクラス
/// </summary>
public class TestBulletManager : MonoBehaviour
{
    // 弾のプレハブ
    [SerializeField] GameObject Obj;
    // 発射間隔のカウンター
    public float count;
    // 発射間隔
    public float shotmax;

    /// <summary>
    /// 弾を発射する
    /// </summary>
    public void Shot()
    {
        // 発射間隔を超えたら発射
        if (count >= shotmax)
        {
            // カウンターをリセット
            count = 0;
            // 弾を生成
            var obj = Instantiate(Obj, transform.localPosition, transform.localRotation);
            // 弾に初速を与える
            obj.GetComponent<Rigidbody>().linearVelocity = Vector3.right * 20;
            // 発射元のタグを設定
            obj.GetComponent<TestBullet>().SetShotSideTag(tag);
            // プレイヤー1の弾の場合
            if (tag == "Player1")
            {
                obj.tag = "Bullet1";
            }
            // プレイヤー2の弾の場合
            if (tag == "Player2")
            {
                obj.tag = "Bullet2";
            }

        }
        else
        {
            // カウンターを更新
            count += Time.deltaTime;
        }
    }
}