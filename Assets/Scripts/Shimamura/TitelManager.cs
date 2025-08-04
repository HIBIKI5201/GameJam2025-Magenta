using System.Linq;
using SymphonyFrameWork.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitelManager : MonoBehaviour
{
    [SerializeField] string _TitleScene;
    [SerializeField] string _OptionScene;
    [SerializeField] string _GameScene;

    KeyCode[] _StratKey1 = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    KeyCode[] _StratKey2 = {KeyCode.RightArrow,KeyCode.LeftArrow,KeyCode.UpArrow,KeyCode.DownArrow };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //タイムラインでループすようにする　静止画だとさみしいから

        //SymphonyTween 時間を何秒かかけて動かしたりすることができる
    }

    // Update is called once per frame
    void Update()
    {
        if (_StratKey1.Any(Input.GetKey) && _StratKey2.Any(Input.GetKey))
        {
            Debug.Log("aaa");
        }
    }
    public void TitleSenen()    //タイトルシーン
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void OptionSenen()   //オプションシーン
    {
        SceneManager.LoadScene("OptionScene");
    }
    public void StratGame()     //ゲームシーン    WASDのどれかと十字キーのどれかを押してる状態でスタート
    {
        SceneManager.LoadScene("GameScene");
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
