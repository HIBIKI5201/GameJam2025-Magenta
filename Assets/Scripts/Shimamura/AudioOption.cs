using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerInput))]

public class AudioOption : MonoBehaviour
{

    PlayerInput _playerInput;

    [SerializeField] Slider[] sliders;
    [SerializeField] Outline[] outlines;

    int selectedIndex = 0;
    float lastInputTime = 0f;
    float inputDelay = 0.2f; // 入力感度調整（連打防止）


    public AudioMixer AudioMixer;
    public Slider BGMSlider;
    public Slider SESlider;
    public Slider MasterSlider;
    public GameObject firstSelect;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        EventSystem.current.SetSelectedGameObject(firstSelect);

        AudioMixer.GetFloat("BGMVolume", out float bgmVolume);//スライダー設定
        BGMSlider.value = bgmVolume;
        AudioMixer.GetFloat("SEVolume", out float seVolume);
        SESlider.value = seVolume;
        AudioMixer.GetFloat("MasterVolume", out float mastervolume);
        MasterSlider.value = mastervolume;
    }

    private void OnEnable()
    {
        _playerInput.actions["Move1"].started += OnAndioOption;

        _playerInput.actions["Move1"].canceled += OnAndioOption;

    }
    private void OnDisable()
    {
        _playerInput.actions["Move1"].started -= OnAndioOption;
            
        _playerInput.actions["Move1"].canceled -= OnAndioOption;
    }

    private void OnAndioOption(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (Time.time - lastInputTime < inputDelay)
            return;

        // 上下キーで選択切替
        if (input.y > 0.5f)
        {
            selectedIndex = (selectedIndex - 1 + sliders.Length) % sliders.Length;
            UpdateOutLine();
            lastInputTime = Time.time;
        }
        else if (input.y < -0.5f)
        {
            selectedIndex = (selectedIndex + 1) % sliders.Length;
            UpdateOutLine();
            lastInputTime = Time.time;
        }
    }

    private void Update()
    {
        if (_playerInput == null) return;

        Vector2 input = _playerInput.actions["Move1"].ReadValue<Vector2>();

        // 左右キーでスライダー値を調整
        if (input.x > 0.5f)
        {
            sliders[selectedIndex].value += Time.deltaTime;
        }
        else if (input.x < -0.5f)
        {
            sliders[selectedIndex].value -= Time.deltaTime;
        }
    }

    private void UpdateOutLine()        
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            outlines[i].enabled = (i == selectedIndex);
        }
    }

    public void SetBGM(float volume)
    {
        AudioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSE(float volume)
    {
        AudioMixer.SetFloat("SEVolume", volume);
    }
    public void SetMaster(float volume)
    {
        AudioMixer.SetFloat("MasterVolume", volume);
    }
}
