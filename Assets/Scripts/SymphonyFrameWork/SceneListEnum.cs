/// <summary>
/// シーンのリストを定義する列挙型です。
/// </summary>
public enum SceneListEnum : int
{
    /// <summary>
    /// シーンが指定されていない状態を示します。
    /// </summary>
    None = 0,
    /// <summary>
    /// タイトルシーンを示します。
    /// </summary>
    Title = 1,
    /// <summary>
    /// インゲームシーンを示します。
    /// </summary>
    Ingame = 2,
    /// <summary>
    /// リザルトシーンを示します。
    /// </summary>
    Result = 3,
}