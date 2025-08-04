using System;

/// <summary>
/// レイヤーの種類を定義する列挙型
/// </summary>
[Flags]
public enum LayersEnum : int
{
    /// <summary>
    /// なし
    /// </summary>
    None = 1 << 0,
    /// <summary>
    /// デフォルト
    /// </summary>
    Default = 1 << 1,
    /// <summary>
    /// 透明なエフェクト
    /// </summary>
    TransparentFX = 1 << 2,
    /// <summary>
    /// 水
    /// </summary>
    Water = 1 << 3,
    /// <summary>
    /// UI
    /// </summary>
    UI = 1 << 4,
}