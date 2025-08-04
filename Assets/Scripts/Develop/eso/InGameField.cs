using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ゲーム内のフィールドを管理するクラス
/// </summary>
public class InGameField : MonoBehaviour
{
    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    private void Update()
    {
        
    }

    /// <summary>
    /// トリガーに接触した際の処理
    /// </summary>
    /// <param name="collision">衝突情報</param>
    private void OnTrigger(Collision collision)
    {
        // 衝突したオブジェクトを原点に移動
        collision.transform.position = Vector3.zero;
    }

    /// <summary>
    /// 衝突が終了した際の処理
    /// </summary>
    /// <param name="collision">衝突情報</param>
    private void OnCollisionExit(Collision collision)
    {
        // 衝突したオブジェクトを原点に移動
        collision.transform.position = Vector3.zero;
    }
}