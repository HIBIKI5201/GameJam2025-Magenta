using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Resultmanager : MonoBehaviour
{
    [Header("シーン名")]
    [SerializeField] private string _gameSceneName;
    [SerializeField] private string _titleSceneName;

    [Header("UI Image")]
    [SerializeField] private Image _gameImage;
    [SerializeField] private Image _titleImage;

    [Header("ホールドゲージ")]
    [SerializeField] private Image _gameHoldSlider;
    [SerializeField] private Image _titleHoldSlider;
    [SerializeField] private float _requiredHoldTime = 3f;

    PlayerInput _playerInput;
    float _holdTime1 = 0f;
    float _holdTime2 = 0f;
    bool _isHoldingMove1 = false;
    bool _isHoldingMove2 = false;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.actions["Move1"].started += OnMove1Started;
        _playerInput.actions["Move1"].canceled += OnMove1Canceled;

        _playerInput.actions["Move2"].started += OnMove2Started;
        _playerInput.actions["Move2"].canceled += OnMove2Canceled;
    }

    private void OnDisable()
    {
        _playerInput.actions["Move1"].started -= OnMove1Started;
        _playerInput.actions["Move1"].canceled -= OnMove1Canceled;

        _playerInput.actions["Move2"].started -= OnMove2Started;
        _playerInput.actions["Move2"].canceled -= OnMove2Canceled;
    }

    private void Update()
    {
        if (_isHoldingMove1)
        {
            _holdTime1 += Time.deltaTime;
            if (_holdTime1 >= _requiredHoldTime)
            {
                SceneManager.LoadScene(_gameSceneName);
                _isHoldingMove1 = false;
            }
        }
        else
        {
            _holdTime1 = 0f;
        }

        if (_isHoldingMove2)
        {
            _holdTime2 += Time.deltaTime;
            if (_holdTime2 >= _requiredHoldTime)
            {
                SceneManager.LoadScene(_titleSceneName);
                _isHoldingMove2 = false;
            }
        }
        else
        {
            _holdTime2 = 0f;
        }
    }

    private void OnMove1Started(InputAction.CallbackContext context)
    {
        _isHoldingMove1 = true;
    }

    private void OnMove1Canceled(InputAction.CallbackContext context)
    {
        _isHoldingMove1 = false;
        _holdTime1 = 0f;
    }

    private void OnMove2Started(InputAction.CallbackContext context)
    {
        _isHoldingMove2 = true;
    }

    private void OnMove2Canceled(InputAction.CallbackContext context)
    {
        _isHoldingMove2 = false;
        _holdTime2 = 0f;
    }
}
