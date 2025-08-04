using System;

/// <summary>
/// レイヤーの種類を定義する列挙型です。
/// </summary>
[Flags]
public enum LayersEnum : int
{
    /// <summary>
    /// レイヤーが指定されていない状態を示します。
    /// </summary>
    None = 1 << 0,
    /// <summary>
    /// デフォルトのレイヤーを示します。
    /// </summary>
    Default = 1 << 1,
    /// <summary>
    /// 透明なエフェクト用のレイヤーを示します。
    /// </summary>
    TransparentFX = 1 << 2,
    /// <summary>
    /// 水用のレイヤーを示します。
    /// </summary>
    Water = 1 << 3,
    /// <summary>
    /// UI要素用のレイヤーを示します。
    /// </summary>
    UI = 1 << 4,
}
