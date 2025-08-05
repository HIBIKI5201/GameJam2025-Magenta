using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// オーディオ設定を管理するクラス
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class AudioOption : MonoBehaviour
{
    // プレイヤーインプット
    PlayerInput _playerInput;

    // オーディオスライダー
    [SerializeField] Slider[] _sliders;
    // スライダーのアウトライン
    [SerializeField] Outline[] _outlines;

    // 選択中のスライダーのインデックス
    int selectedIndex = 0;
    // 最後の入力時間
    float lastInputTime = 0f;

    // オーディオミキサー
    public AudioMixer AudioMixer;
    // BGMスライダー
    public Slider BGMSlider;
    // SEスライダー
    public Slider SESlider;
    // マスター音量スライダー
    public Slider MasterSlider;
    // 最初に選択するゲームオブジェクト
    public GameObject FirstSelect;

    /// <summary>
    /// 初期化処理 (Awake)
    /// </summary>
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// 初期化処理 (Start)
    /// </summary>
    void Start()
    {
        // 最初に選択するUI要素を設定
        EventSystem.current.SetSelectedGameObject(FirstSelect);

        // 各音量を取得してスライダーに反映
        AudioMixer.GetFloat("BGMVolume", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        AudioMixer.GetFloat("SEVolume", out float seVolume);
        SESlider.value = seVolume;
        AudioMixer.GetFloat("MasterVolume", out float mastervolume);
        MasterSlider.value = mastervolume;
    }

    /// <summary>
    ///有効になった際の処理
    /// </summary>
    private void OnEnable()
    {
        // 入力アクションにコールバックを登録
        _playerInput.actions["Move1"].started += OnAndioOption;
        _playerInput.actions["Move1"].canceled += OnAndioOption;
    }

    /// <summary>
    /// 無効になった際の処理
    /// </summary>
    private void OnDisable()
    {
        // 入力アクションからコールバックを解除
        _playerInput.actions["Move1"].started -= OnAndioOption;
        _playerInput.actions["Move1"].canceled -= OnAndioOption;
    }

    /// <summary>
    /// オーディオ設定の入力処理
    /// </summary>
    /// <param name="context">入力コンテキスト</param>
    private void OnAndioOption(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        // 上入力で前のスライダーを選択
        if (input.y > 0.5f)
        {
            selectedIndex = (selectedIndex - 1 + _sliders.Length) % _sliders.Length;
            UpdateOutLine();
            lastInputTime = Time.time;
        }
        // 下入力で次のスライダーを選択
        else if (input.y < -0.5f)
        {
            selectedIndex = (selectedIndex + 1) % _sliders.Length;
            UpdateOutLine();
            lastInputTime = Time.time;
        }
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    private void Update()
    {
        if (_playerInput == null) return;

        Vector2 input = _playerInput.actions["Move1"].ReadValue<Vector2>();

        // 左右キーでスライダーの値を調整
        if (input.x > 0.5f)
        {
            _sliders[selectedIndex].value += Time.deltaTime;
        }
        else if (input.x < -0.5f)
        {
            _sliders[selectedIndex].value -= Time.deltaTime;
        }
    }

    /// <summary>
    /// アウトラインを更新する
    /// </summary>
    private void UpdateOutLine()
    {
        // 選択中のスライダーのアウトラインのみ表示
        for (int i = 0; i < _outlines.Length; i++)
        {
            _outlines[i].enabled = (i == selectedIndex);
        }
    }

    /// <summary>
    /// BGM音量を設定する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetBGM(float volume)
    {
        AudioMixer.SetFloat("BGMVolume", volume);
    }

    /// <summary>
    /// SE音量を設定する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetSE(float volume)
    {
        AudioMixer.SetFloat("SEVolume", volume);
    }

    /// <summary>
    /// マスター音量を設定する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetMaster(float volume)
    {
        AudioMixer.SetFloat("MasterVolume", volume);
    }
}