using System;

/// <summary>
/// タグの種類を定義する列挙型
/// </summary>
[Flags]
public enum TagsEnum : int
{
    /// <summary>
    /// なし
    /// </summary>
    None = 1 << 0,
    /// <summary>
    /// タグなし
    /// </summary>
    Untagged = 1 << 1,
    /// <summary>
    /// リスポーン
    /// </summary>
    Respawn = 1 << 2,
    /// <summary>
    /// フィニッシュ
    /// </summary>
    Finish = 1 << 3,
    /// <summary>
    /// エディタ専用
    /// </summary>
    EditorOnly = 1 << 4,
    /// <summary>
    /// メインカメラ
    /// </summary>
    MainCamera = 1 << 5,
    /// <summary>
    /// プレイヤー
    /// </summary>
    Player = 1 << 6,
    /// <summary>
    /// ゲームコントローラー
    /// </summary>
    GameController = 1 << 7,
}