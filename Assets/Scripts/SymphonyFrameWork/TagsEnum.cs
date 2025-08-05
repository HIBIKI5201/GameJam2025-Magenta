using System;

/// <summary>
/// タグの種類を定義する列挙型です。
/// </summary>
[Flags]
public enum TagsEnum : int
{
    /// <summary>
    /// タグが指定されていない状態を示します。
    /// </summary>
    None = 1 << 0,
    /// <summary>
    /// タグが設定されていないオブジェクトを示します。
    /// </summary>
    Untagged = 1 << 1,
    /// <summary>
    /// リスポーン地点を示します。
    /// </summary>
    Respawn = 1 << 2,
    /// <summary>
    /// ゴール地点を示します。
    /// </summary>
    Finish = 1 << 3,
    /// <summary>
    /// エディタでのみ使用されるオブジェクトを示します。
    /// </summary>
    EditorOnly = 1 << 4,
    /// <summary>
    /// メインカメラを示します。
    /// </summary>
    MainCamera = 1 << 5,
    /// <summary>
    /// プレイヤーオブジェクトを示します。
    /// </summary>
    Player = 1 << 6,
    /// <summary>
    /// ゲームの全体的な制御を行うオブジェクトを示します。
    /// </summary>
    GameController = 1 << 7,
}
