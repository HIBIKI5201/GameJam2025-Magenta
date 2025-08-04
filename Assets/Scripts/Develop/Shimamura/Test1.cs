using System.Linq;
using SymphonyFrameWork.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/// <summary>
/// テスト用のシーン遷移を管理するクラス
/// </summary>
public class Test1 : MonoBehaviour
{
    // タイトルシーン名
    [SerializeField] string _TitleScene;
    // オプションシーン名
    [SerializeField] string _OptionScene;
    // ゲームシーン名
    [SerializeField] string _GameScene;

    // 入力アクションマップ
    private InputActionMap _ActionMap;
    
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        _ActionMap = new InputActionMap();
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
      
    }

    /// <summary>
    /// タイトルシーンに遷移する
    /// </summary>
    public void TitleSenen()
    {
        SceneManager.LoadScene(_TitleScene);
    }

    /// <summary>
    /// オプションシーンに遷移する
    /// </summary>
    public void OptionSenen()
    {
        SceneManager.LoadScene(_OptionScene);
    }

    /// <summary>
    /// ゲームシーンに遷移する
    /// </summary>
    public void StratGame()
    {
        SceneManager.LoadScene(_GameScene);
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Unityエディタの場合は再生を停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後はアプリケーションを終了
        Application.Quit();
#endif
    }
}