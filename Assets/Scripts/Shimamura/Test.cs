using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(PlayerInput))]
public class Test : MonoBehaviour
{
    PlayerInput _p;

    private bool _move1Pressed = false;
    private bool _move2Pressed = false;

    private void Awake()
    {
        _p = GetComponent<PlayerInput>();
       // _inputActions = new InputActionMap();
    }
    private void OnEnable()
    {
        _p.actions["Move1"].started += OnMove1Started;
        _p.actions["Move2"].started += OnMove2Started;

        _p.actions["Move1"].canceled += OnMove1Canceled;
        _p.actions["Move2"].canceled += OnMove2Canceled;

        // _inputActions.Enable();
        // _inputActions.UI.performed += OnStartGame;
    }

    private void OnDisable()
    {
        _p.actions["Move1"].started -= OnMove1Started;
        _p.actions["Move2"].started -= OnMove2Started;

        _p.actions["Move1"].canceled -= OnMove1Canceled;
        _p.actions["Move2"].canceled -= OnMove2Canceled;
    }
     private void OnMove1Started(InputAction.CallbackContext context)
    {
        _move1Pressed = true;
        CheckBothPressed();
    }

    private void OnMove2Started(InputAction.CallbackContext context)
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
        if (_move1Pressed && _move2Pressed)
        {
            Debug.Log("両方押されたのでシーンを移動！");
            //SceneManager.LoadScene("GameScene"); // ← シーン名を変更
        }
    }
   /* private void Update()
    {
        if (_move1Pressed && _move2Pressed)
        {
            Debug.Log("両方押されたのでシーンを移動！");
            // SceneManager.LoadScene("GameScene");
        }
    }*/
}
