using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerInput))]

public class AudioOption : MonoBehaviour
{

    PlayerInput _playerInput;

    [SerializeField] Slider[] _sliders;
    [SerializeField] Outline[] _outlines;

    int selectedIndex = 0;
    float lastInputTime = 0f;


    public AudioMixer AudioMixer;
    public Slider BGMSlider;
    public Slider SESlider;
    public Slider MasterSlider;
    public GameObject FirstSelect;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        EventSystem.current.SetSelectedGameObject(FirstSelect); //最初に選択された状態にしている変数firstSelect

        AudioMixer.GetFloat("BGMVolume", out float bgmVolume);//各音量を取得してスライダーに反映する
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

        if (input.y > 0.5f)　　//スライダーの選択を矢印で上下に動かす
        {
            selectedIndex = (selectedIndex - 1 + _sliders.Length) % _sliders.Length;
            UpdateOutLine();
            lastInputTime = Time.time;
        }
        else if (input.y < -0.5f)
        {
            selectedIndex = (selectedIndex + 1) % _sliders.Length;
            UpdateOutLine();
            lastInputTime = Time.time;
        }
    }

    private void Update()
    {
        if (_playerInput == null) return;

        Vector2 input = _playerInput.actions["Move1"].ReadValue<Vector2>();

        
        if (input.x > 0.5f) // 左右キーでスライダー値を調整している
        {
            _sliders[selectedIndex].value += Time.deltaTime;
        }
        else if (input.x < -0.5f)
        {
            _sliders[selectedIndex].value -= Time.deltaTime;
        }
    }

    private void UpdateOutLine()        
    {
        for (int i = 0; i < _outlines.Length; i++)
        {
            _outlines[i].enabled = (i == selectedIndex);
            Debug.Log("aaa");
        }
    }

    public void SetBGM(float volume)        //スライダーとの連携
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
