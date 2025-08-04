using System.Linq;
using SymphonyFrameWork.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class TitelManager : MonoBehaviour
{
    [SerializeField] string _TitleScene;
    [SerializeField] string _OptionScene;
    [SerializeField] string _GameScene;

    private InputActionMap _ActionMap;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ActionMap = new InputActionMap();

      //タイムラインでループすようにする　静止画だとさみしいから
      //SymphonyTween 時間を何秒かかけて動かしたりすることができる
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void TitleSenen()    //タイトルシーン
    {
        SceneManager.LoadScene(_TitleScene);
    }
    public void OptionSenen()   //オプションシーン
    {
        SceneManager.LoadScene(_OptionScene);
    }
    public void StratGame()     //ゲームシーン    WASDのどれかと十字キーのどれかを押してる状態でスタート
    {
        SceneManager.LoadScene(_GameScene);
    }


    public void ExitGame()      //ゲーム終了
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後にアプリケーションを終了
        Application.Quit();
#endif
    }
}
