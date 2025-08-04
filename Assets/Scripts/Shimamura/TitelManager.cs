using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(PlayerInput))]
public class TitelManager : MonoBehaviour
{

    [SerializeField] string _TitleScene;
    [SerializeField] string _OptionScene;
    [SerializeField] string _GameScene;


    PlayerInput _p;
    private bool _move1Pressed = false;
    private bool _move2Pressed = false;

    private void Awake()
    {
        _p = GetComponent<PlayerInput>();
    }
    private void OnEnable()
    {
        _p.actions["Move1"].started += OnMove1Started;
        _p.actions["Move2"].started += OnMove2Started;

        _p.actions["Move1"].canceled += OnMove1Canceled;
        _p.actions["Move2"].canceled += OnMove2Canceled;
    }

    private void OnDisable()
    {
        _p.actions["Move1"].started -= OnMove1Started;
        _p.actions["Move2"].started -= OnMove2Started;

        _p.actions["Move1"].canceled -= OnMove1Canceled;
        _p.actions["Move2"].canceled -= OnMove2Canceled;
    }
     private void OnMove1Started(InputAction.CallbackContext context)   //プレイヤー1がのボタンを押したらtureになる
    {
        _move1Pressed = true;
        CheckBothPressed();
    }

    private void OnMove2Started(InputAction.CallbackContext context)    //プレイヤー2がのボタンを押したらtureになる
    {
        _move2Pressed = true;
        CheckBothPressed();
    }

    private void OnMove1Canceled(InputAction.CallbackContext context)
    {
        _move1Pressed = false;
    }

    private void OnMove2Canceled(InputAction.CallbackContext context)
    {
        _move2Pressed = false;
    }
    private void CheckBothPressed()
    {
        if (_move1Pressed && _move2Pressed)     //プレイヤー1とプレイヤー2が同時押しした時に
        {
            Debug.Log("両方押されたのでシーンを移動！");
            SceneManager.LoadScene(_GameScene); // ゲームシーン
        }
    }
    public void TitleSenen()    //タイトルシーン
    {
        SceneManager.LoadScene(_TitleScene);
    }
    public void OptionSenen()   //オプションシーン
    {
        SceneManager.LoadScene(_OptionScene);
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
