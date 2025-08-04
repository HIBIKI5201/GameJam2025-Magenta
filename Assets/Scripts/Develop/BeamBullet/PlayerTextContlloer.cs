using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのテキストを制御するクラス
/// </summary>
public class PlayerTextContlloer : MonoBehaviour
{
    // 弾ジェネレーター
    IBulletGenerator _bulletGenerator;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // ビーム弾ジェネレーターを生成
        _bulletGenerator = new BeamBulletGenerator();

        // 弾を生成
        _bulletGenerator.Update(1f);
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        
    }
}